using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class InordersItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public InordersItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/InordersItem
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<InordersItem>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/InordersItem(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<InordersItem>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/InordersItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] InordersItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<InordersItem>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/InordersItem(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<InordersItem> patch)
        {
            var entity = await _context.Set<InordersItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/InordersItem(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<InordersItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<InordersItem>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
