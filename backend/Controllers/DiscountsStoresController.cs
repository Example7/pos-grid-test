using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class DiscountsStoresController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public DiscountsStoresController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/DiscountsStore
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<DiscountsStore>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/DiscountsStore(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<DiscountsStore>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/DiscountsStore
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DiscountsStore entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<DiscountsStore>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/DiscountsStore(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<DiscountsStore> patch)
        {
            var entity = await _context.Set<DiscountsStore>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/DiscountsStore(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<DiscountsStore>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<DiscountsStore>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
