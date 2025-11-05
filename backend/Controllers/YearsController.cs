using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class YearsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public YearsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Year
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Year>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Year(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Year>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Year
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Year entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Year>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Year(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Year> patch)
        {
            var entity = await _context.Set<Year>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Year(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Year>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Year>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
