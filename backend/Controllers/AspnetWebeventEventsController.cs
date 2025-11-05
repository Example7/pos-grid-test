using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetWebeventEventsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetWebeventEventsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetWebeventEvent
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetWebeventEvent>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetWebeventEvent(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetWebeventEvent>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetWebeventEvent
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetWebeventEvent entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetWebeventEvent>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetWebeventEvent(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetWebeventEvent> patch)
        {
            var entity = await _context.Set<AspnetWebeventEvent>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetWebeventEvent(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetWebeventEvent>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetWebeventEvent>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
