using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Outcome
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Outcome>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Outcome(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Outcome>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Outcome
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Outcome entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Outcome>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Outcome(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Outcome> patch)
        {
            var entity = await _context.Set<Outcome>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Outcome(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Outcome>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Outcome>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
