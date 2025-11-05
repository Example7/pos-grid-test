using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ShopsSetsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ShopsSetsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ShopsSet
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<ShopsSet>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/ShopsSet(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<ShopsSet>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/ShopsSet
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ShopsSet entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<ShopsSet>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ShopsSet(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<ShopsSet> patch)
        {
            var entity = await _context.Set<ShopsSet>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ShopsSet(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<ShopsSet>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<ShopsSet>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
