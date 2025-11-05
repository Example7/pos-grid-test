using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class SetsItemsProductsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public SetsItemsProductsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/SetsItemsProduct
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<SetsItemsProduct>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/SetsItemsProduct(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<SetsItemsProduct>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/SetsItemsProduct
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SetsItemsProduct entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<SetsItemsProduct>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/SetsItemsProduct(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<SetsItemsProduct> patch)
        {
            var entity = await _context.Set<SetsItemsProduct>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/SetsItemsProduct(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<SetsItemsProduct>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<SetsItemsProduct>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
