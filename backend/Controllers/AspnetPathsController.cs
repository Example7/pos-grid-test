using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetPathsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetPathsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetPath
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetPath>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetPath(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetPath>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetPath
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetPath entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetPath>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetPath(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetPath> patch)
        {
            var entity = await _context.Set<AspnetPath>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetPath(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetPath>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetPath>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
