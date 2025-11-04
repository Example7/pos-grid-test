using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Deltas;
using DevExpress.Models.Generated;
using DevExpress.Data;

namespace DevExpress.Controllers
{
    public class OrdersPaymentsStatusesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersPaymentsStatusesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersPaymentsStatuses
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.OrdersPaymentsStatuses);
        }

        // GET: odata/OrdersPaymentsStatuses(5)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var entity = _context.OrdersPaymentsStatuses
                .FirstOrDefault(s => s.OrderPaymentStatusId == key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersPaymentsStatuses
        public async Task<IActionResult> Post([FromBody] OrdersPaymentsStatus status)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.OrdersPaymentsStatuses.Add(status);
            await _context.SaveChangesAsync();
            return Created(status);
        }

        // PATCH: odata/OrdersPaymentsStatuses(5)
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<OrdersPaymentsStatus> patch)
        {
            var entity = await _context.OrdersPaymentsStatuses.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersPaymentsStatuses(5)
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.OrdersPaymentsStatuses.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.OrdersPaymentsStatuses.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
