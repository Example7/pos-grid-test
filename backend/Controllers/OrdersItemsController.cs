using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersItem
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersItem>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersItem(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersItem>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersItem>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersItem(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersItem> patch)
        {
            var entity = await _context.Set<OrdersItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersItem(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersItem>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
