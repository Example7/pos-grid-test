using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetApplicationsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetApplicationsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetApplication
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetApplication>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetApplication(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetApplication>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetApplication
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetApplication entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetApplication>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetApplication(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetApplication> patch)
        {
            var entity = await _context.Set<AspnetApplication>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetApplication(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetApplication>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetApplication>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
