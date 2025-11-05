using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetPersonalizationallusersController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetPersonalizationallusersController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetPersonalizationalluser
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetPersonalizationalluser>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetPersonalizationalluser(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetPersonalizationalluser>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetPersonalizationalluser
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetPersonalizationalluser entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetPersonalizationalluser>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetPersonalizationalluser(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetPersonalizationalluser> patch)
        {
            var entity = await _context.Set<AspnetPersonalizationalluser>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetPersonalizationalluser(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetPersonalizationalluser>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetPersonalizationalluser>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
