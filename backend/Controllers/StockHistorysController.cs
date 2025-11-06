using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class StockHistorysController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public StockHistorysController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/StockHistory
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<StockHistory>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/StockHistory(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<StockHistory>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/StockHistory
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StockHistory entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<StockHistory>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/StockHistory(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<StockHistory> patch)
        {
            var entity = await _context.Set<StockHistory>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/StockHistory(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<StockHistory>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<StockHistory>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
