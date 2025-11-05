using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductSetsItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductSetsItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductSetsItem
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<ProductSetsItem>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/ProductSetsItem(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<ProductSetsItem>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/ProductSetsItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductSetsItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<ProductSetsItem>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductSetsItem(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<ProductSetsItem> patch)
        {
            var entity = await _context.Set<ProductSetsItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductSetsItem(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<ProductSetsItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<ProductSetsItem>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
