using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetUsersController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetUsersController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetUser
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetUser>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetUser(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetUser>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetUser
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetUser entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetUser>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetUser(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetUser> patch)
        {
            var entity = await _context.Set<AspnetUser>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetUser(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetUser>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetUser>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
