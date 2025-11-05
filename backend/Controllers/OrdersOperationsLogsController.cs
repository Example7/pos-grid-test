using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersOperationsLogsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersOperationsLogsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersOperationsLog
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersOperationsLog>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersOperationsLog(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersOperationsLog>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersOperationsLog
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersOperationsLog entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersOperationsLog>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersOperationsLog(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersOperationsLog> patch)
        {
            var entity = await _context.Set<OrdersOperationsLog>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersOperationsLog(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersOperationsLog>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersOperationsLog>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
