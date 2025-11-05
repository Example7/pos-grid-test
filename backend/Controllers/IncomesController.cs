using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class IncomesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public IncomesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Income
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Income>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Income(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Income>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Income
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Income entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Income>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Income(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Income> patch)
        {
            var entity = await _context.Set<Income>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Income(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Income>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Income>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
