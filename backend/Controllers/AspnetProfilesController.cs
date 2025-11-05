using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetProfilesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetProfilesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetProfile
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetProfile>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetProfile(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetProfile>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetProfile
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetProfile entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetProfile>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetProfile(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetProfile> patch)
        {
            var entity = await _context.Set<AspnetProfile>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetProfile(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetProfile>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetProfile>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
