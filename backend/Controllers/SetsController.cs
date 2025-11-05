using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class SetsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public SetsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Set
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Set>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Set(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Set>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Set
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Set entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Set>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Set(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Set> patch)
        {
            var entity = await _context.Set<Set>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Set(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Set>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Set>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
