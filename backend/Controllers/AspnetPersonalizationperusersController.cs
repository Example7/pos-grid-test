using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetPersonalizationperusersController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetPersonalizationperusersController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetPersonalizationperuser
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetPersonalizationperuser>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetPersonalizationperuser(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetPersonalizationperuser>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetPersonalizationperuser
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetPersonalizationperuser entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetPersonalizationperuser>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetPersonalizationperuser(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetPersonalizationperuser> patch)
        {
            var entity = await _context.Set<AspnetPersonalizationperuser>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetPersonalizationperuser(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetPersonalizationperuser>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetPersonalizationperuser>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
