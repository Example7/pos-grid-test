using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class StocksController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public StocksController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Stock
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Stock>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Stock(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Stock>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Stock
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Stock entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Stock>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Stock(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Stock> patch)
        {
            var entity = await _context.Set<Stock>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Stock(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Stock>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Stock>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
