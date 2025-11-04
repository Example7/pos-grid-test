using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class ProductsCategories1Controller : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsCategories1Controller(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsCategories1
        [EnableQuery]
        public IActionResult Get() => Ok(_context.ProductsCategories1s);

        // GET: odata/ProductsCategories1(1)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var item = _context.ProductsCategories1s.FirstOrDefault(c => c.ProductCategory1Id == key);
            return item == null ? NotFound() : Ok(item);
        }

        // POST: odata/ProductsCategories1
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsCategories1 entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(entity.Category1Name))
                return BadRequest("Pole 'Category1Name' jest wymagane.");

            entity.CreatedAt = DateTime.UtcNow;

            _context.ProductsCategories1s.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsCategories1(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<ProductsCategories1> patch)
        {
            var entity = await _context.ProductsCategories1s.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsCategories1(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.ProductsCategories1s.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.ProductsCategories1s.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
