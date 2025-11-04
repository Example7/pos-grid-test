using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class ProductsCategories2Controller : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsCategories2Controller(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsCategories2
        [EnableQuery]
        public IActionResult Get() => Ok(_context.ProductsCategories2s);

        // GET: odata/ProductsCategories2(1)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var item = _context.ProductsCategories2s.FirstOrDefault(c => c.ProductCategory2Id == key);
            return item == null ? NotFound() : Ok(item);
        }

        // POST: odata/ProductsCategories2
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsCategories2 entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(entity.Category2Name))
                return BadRequest("Pole 'Category2Name' jest wymagane.");

            entity.CreatedAt = DateTime.UtcNow;

            _context.ProductsCategories2s.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsCategories2(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<ProductsCategories2> patch)
        {
            var entity = await _context.ProductsCategories2s.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsCategories2(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.ProductsCategories2s.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.ProductsCategories2s.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
