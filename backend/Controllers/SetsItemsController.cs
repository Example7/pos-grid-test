using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class SetsItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public SetsItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/SetsItem
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<SetsItem>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/SetsItem(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<SetsItem>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/SetsItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SetsItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<SetsItem>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/SetsItem(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<SetsItem> patch)
        {
            var entity = await _context.Set<SetsItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/SetsItem(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<SetsItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<SetsItem>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
