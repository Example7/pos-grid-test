using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Products
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Products.AsQueryable();
            return Ok(query);
        }

        // GET: odata/Products(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] int key)
        {
            var entity = _context.Products.FirstOrDefault(p => p.ProductId == key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Products.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Products(key)
        [HttpPatch]
        public async Task<IActionResult> Patch([FromRoute] int key, [FromBody] Delta<Product> patch)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == key);
            if (entity == null)
                return NotFound();

            try
            {
                patch.TrySetPropertyValue(nameof(Product.ProductsBarcodes), null);
                patch.TrySetPropertyValue(nameof(Product.ProductsRecipes), null);
                patch.TrySetPropertyValue(nameof(Product.OrdersItems), null);

                patch.Patch(entity);
                entity.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine("Błąd podczas aktualizacji produktu: " + ex.Message);
                return StatusCode(500, "Błąd aktualizacji produktu: " + ex.Message);
            }
        }

        // DELETE: odata/Products(key)
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] int key)
        {
            var entity = await _context.Products.FirstOrDefaultAsync(p => p.ProductId == key);
            if (entity == null)
                return NotFound();

            _context.Products.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
