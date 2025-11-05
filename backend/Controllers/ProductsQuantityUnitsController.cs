using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class ProductsQuantityUnitsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsQuantityUnitsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsQuantityUnits
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.ProductsQuantityUnits);
        }

        // GET: odata/ProductsQuantityUnits(1)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.ProductsQuantityUnits.FirstOrDefault(e => e.ProductQuantityUnitId == key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsQuantityUnit entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity.CreatedAt = DateTime.UtcNow;
            _context.ProductsQuantityUnits.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH
        [HttpPatch]
        public async Task<IActionResult> Patch([FromRoute] long key, [FromBody] Delta<ProductsQuantityUnit> patch)
        {
            var entity = await _context.ProductsQuantityUnits.FindAsync(key);
            if (entity == null)
                return NotFound();

            try
            {
                patch.TrySetPropertyValue(nameof(ProductsQuantityUnit.Products), null);

                patch.Patch(entity);

                await _context.SaveChangesAsync();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd aktualizacji jednostki: {ex.Message}");
                return StatusCode(500, "Błąd aktualizacji jednostki: " + ex.Message);
            }
        }

        // DELETE
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long key)
        {
            var entity = await _context.ProductsQuantityUnits.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.ProductsQuantityUnits.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
