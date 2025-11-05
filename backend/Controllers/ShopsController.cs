using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ShopsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ShopsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Shop
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Shop>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Shop(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Shop>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Shop
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Shop entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Shop>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Shop(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Shop> patch)
        {
            var entity = await _context.Set<Shop>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Shop(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Shop>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Shop>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
