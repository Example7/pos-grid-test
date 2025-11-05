using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class UserContextsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public UserContextsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/UserContext
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<UserContext>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/UserContext(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<UserContext>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/UserContext
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] UserContext entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<UserContext>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/UserContext(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<UserContext> patch)
        {
            var entity = await _context.Set<UserContext>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/UserContext(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<UserContext>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<UserContext>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
