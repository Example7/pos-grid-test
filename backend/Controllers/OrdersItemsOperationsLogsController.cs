using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersItemsOperationsLogsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersItemsOperationsLogsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersItemsOperationsLog
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersItemsOperationsLog>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersItemsOperationsLog(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersItemsOperationsLog>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersItemsOperationsLog
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersItemsOperationsLog entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersItemsOperationsLog>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersItemsOperationsLog(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersItemsOperationsLog> patch)
        {
            var entity = await _context.Set<OrdersItemsOperationsLog>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersItemsOperationsLog(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersItemsOperationsLog>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersItemsOperationsLog>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
