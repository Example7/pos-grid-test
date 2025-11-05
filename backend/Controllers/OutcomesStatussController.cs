using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesStatussController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesStatussController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesStatus
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OutcomesStatus>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OutcomesStatus(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OutcomesStatus>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OutcomesStatus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesStatus entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OutcomesStatus>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesStatus(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesStatus> patch)
        {
            var entity = await _context.Set<OutcomesStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesStatus(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OutcomesStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OutcomesStatus>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
