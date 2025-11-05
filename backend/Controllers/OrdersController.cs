using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Order
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Order>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Order(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Order>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Order
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Order entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Order>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Order(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Order> patch)
        {
            var entity = await _context.Set<Order>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Order(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Order>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Order>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
