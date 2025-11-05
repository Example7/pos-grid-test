using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesItem
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OutcomesItem>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OutcomesItem(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OutcomesItem>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OutcomesItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OutcomesItem>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesItem(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesItem> patch)
        {
            var entity = await _context.Set<OutcomesItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesItem(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OutcomesItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OutcomesItem>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
