using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class PosesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public PosesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Pose
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Pose>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Pose(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<Pose>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Pose
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Pose entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Pose>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Pose(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<Pose> patch)
        {
            var entity = await _context.Set<Pose>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Pose(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<Pose>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Pose>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
