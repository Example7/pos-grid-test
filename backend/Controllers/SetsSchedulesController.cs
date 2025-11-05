using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class SetsSchedulesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public SetsSchedulesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/SetsSchedule
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<SetsSchedule>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/SetsSchedule(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<SetsSchedule>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/SetsSchedule
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SetsSchedule entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<SetsSchedule>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/SetsSchedule(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<SetsSchedule> patch)
        {
            var entity = await _context.Set<SetsSchedule>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/SetsSchedule(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<SetsSchedule>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<SetsSchedule>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
