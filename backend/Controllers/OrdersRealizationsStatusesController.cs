using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Deltas;
using DevExpress.Models.Generated;
using DevExpress.Data;

namespace DevExpress.Controllers
{
    public class OrdersRealizationsStatusesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersRealizationsStatusesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersRealizationsStatuses
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.OrdersRealizationsStatuses);
        }

        // GET: odata/OrdersRealizationsStatuses(5)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var entity = _context.OrdersRealizationsStatuses
                .FirstOrDefault(s => s.OrderRealizationStatusId == key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersRealizationsStatuses
        public async Task<IActionResult> Post([FromBody] OrdersRealizationsStatus status)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.OrdersRealizationsStatuses.Add(status);
            await _context.SaveChangesAsync();
            return Created(status);
        }

        // PATCH: odata/OrdersRealizationsStatuses(5)
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<OrdersRealizationsStatus> patch)
        {
            var entity = await _context.OrdersRealizationsStatuses.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersRealizationsStatuses(5)
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.OrdersRealizationsStatuses.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.OrdersRealizationsStatuses.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
