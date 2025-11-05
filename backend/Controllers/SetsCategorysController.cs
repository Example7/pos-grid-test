using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class SetsCategorysController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public SetsCategorysController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/SetsCategory
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<SetsCategory>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/SetsCategory(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<SetsCategory>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/SetsCategory
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] SetsCategory entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<SetsCategory>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/SetsCategory(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<SetsCategory> patch)
        {
            var entity = await _context.Set<SetsCategory>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/SetsCategory(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<SetsCategory>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<SetsCategory>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
