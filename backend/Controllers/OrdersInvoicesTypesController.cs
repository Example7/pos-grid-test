using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersInvoicesTypesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersInvoicesTypesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersInvoicesType
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersInvoicesType>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersInvoicesType(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersInvoicesType>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersInvoicesType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersInvoicesType entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersInvoicesType>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersInvoicesType(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersInvoicesType> patch)
        {
            var entity = await _context.Set<OrdersInvoicesType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersInvoicesType(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersInvoicesType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersInvoicesType>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
