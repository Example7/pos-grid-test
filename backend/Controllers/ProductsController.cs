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
        public IActionResult Get() => Ok(_context.Products
            .Include(p => p.ProductCategory1)
            .Include(p => p.ProductCategory2)
            .Include(p => p.ProductQuantityUnit)
            .Include(p => p.ProductVatRate)
            .Include(p => p.Supplier)
        );

        // GET: odata/Products(1)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var product = _context.Products
                .Include(p => p.ProductCategory1)
                .Include(p => p.ProductCategory2)
                .Include(p => p.ProductQuantityUnit)
                .Include(p => p.ProductVatRate)
                .Include(p => p.Supplier)
                .FirstOrDefault(p => p.ProductId == key);

            return product == null ? NotFound() : Ok(product);
        }

        // POST: odata/Products
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(product.ProductName))
                return BadRequest("Pole 'ProductName' jest wymagane.");

            if (product.ProductPrice < 0)
                return BadRequest("Cena produktu nie mo¿e byæ ujemna.");

            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Created(product);
        }

        // PATCH: odata/Products(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<Product> patch)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null)
                return NotFound();

            patch.Patch(product);
            product.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(product);
        }

        // DELETE: odata/Products(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null)
                return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
