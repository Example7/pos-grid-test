using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersPaymentsStatussController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersPaymentsStatussController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersPaymentsStatus
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersPaymentsStatus>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersPaymentsStatus(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersPaymentsStatus>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersPaymentsStatus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersPaymentsStatus entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersPaymentsStatus>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersPaymentsStatus(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersPaymentsStatus> patch)
        {
            var entity = await _context.Set<OrdersPaymentsStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersPaymentsStatus(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersPaymentsStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersPaymentsStatus>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
