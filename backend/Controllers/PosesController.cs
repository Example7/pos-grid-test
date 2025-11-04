using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Deltas;
using DevExpress.Models.Generated;
using DevExpress.Data;

namespace DevExpress.Controllers
{
    public class PosesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public PosesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Poses
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Poses);
        }

        // GET: odata/Poses(5)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var entity = _context.Poses.FirstOrDefault(p => p.PosId == key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Poses
        public async Task<IActionResult> Post([FromBody] Pose pose)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Poses.Add(pose);
            await _context.SaveChangesAsync();
            return Created(pose);
        }

        // PATCH: odata/Poses(5)
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<Pose> patch)
        {
            var entity = await _context.Poses.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Poses(5)
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Poses.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Poses.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}