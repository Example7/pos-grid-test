using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersVatSummarysController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersVatSummarysController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersVatSummary
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersVatSummary>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersVatSummary(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersVatSummary>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersVatSummary
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersVatSummary entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersVatSummary>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersVatSummary(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersVatSummary> patch)
        {
            var entity = await _context.Set<OrdersVatSummary>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersVatSummary(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersVatSummary>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersVatSummary>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
