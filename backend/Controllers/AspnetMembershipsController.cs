using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetMembershipsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetMembershipsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetMembership
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetMembership>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetMembership(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetMembership>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetMembership
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetMembership entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetMembership>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetMembership(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetMembership> patch)
        {
            var entity = await _context.Set<AspnetMembership>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetMembership(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetMembership>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetMembership>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
