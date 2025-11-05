using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class IncomesStatussController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public IncomesStatussController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/IncomesStatus
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<IncomesStatus>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/IncomesStatus(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<IncomesStatus>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/IncomesStatus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IncomesStatus entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<IncomesStatus>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/IncomesStatus(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<IncomesStatus> patch)
        {
            var entity = await _context.Set<IncomesStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/IncomesStatus(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<IncomesStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<IncomesStatus>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
