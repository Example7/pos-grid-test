using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersRealizationsStatussController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersRealizationsStatussController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersRealizationsStatus
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersRealizationsStatus>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersRealizationsStatus(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersRealizationsStatus>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersRealizationsStatus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersRealizationsStatus entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersRealizationsStatus>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersRealizationsStatus(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersRealizationsStatus> patch)
        {
            var entity = await _context.Set<OrdersRealizationsStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersRealizationsStatus(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersRealizationsStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersRealizationsStatus>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
