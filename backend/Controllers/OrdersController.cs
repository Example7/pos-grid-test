using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class OrdersController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersController(SupabaseDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get() => Ok(_context.Orders);

        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var order = _context.Orders.FirstOrDefault(o => o.OrderId == key);
            return order == null ? NotFound() : Ok(order);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order order)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            order.CreatedAt = DateTime.UtcNow;
            order.UpdatedAt = DateTime.UtcNow;

            _context.Orders.Add(order);
            await _context.SaveChangesAsync();
            return Created(order);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Order> patch)
        {
            var order = await _context.Orders.FindAsync(key);
            if (order == null) return NotFound();

            patch.Patch(order);
            order.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            return Ok(order);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var order = await _context.Orders.FindAsync(key);
            if (order == null) return NotFound();

            _context.Orders.Remove(order);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
