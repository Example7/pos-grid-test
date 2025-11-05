using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetRolesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetRolesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetRole
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetRole>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetRole(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetRole>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetRole
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetRole entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetRole>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetRole(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetRole> patch)
        {
            var entity = await _context.Set<AspnetRole>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetRole(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetRole>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetRole>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
