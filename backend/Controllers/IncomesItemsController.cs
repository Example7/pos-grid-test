using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class IncomesItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public IncomesItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/IncomesItem
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<IncomesItem>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/IncomesItem(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<IncomesItem>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/IncomesItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] IncomesItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<IncomesItem>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/IncomesItem(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<IncomesItem> patch)
        {
            var entity = await _context.Set<IncomesItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/IncomesItem(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<IncomesItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<IncomesItem>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
