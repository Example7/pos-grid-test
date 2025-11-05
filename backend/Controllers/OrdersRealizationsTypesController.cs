using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersRealizationsTypesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersRealizationsTypesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersRealizationsType
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersRealizationsType>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersRealizationsType(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersRealizationsType>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersRealizationsType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersRealizationsType entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersRealizationsType>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersRealizationsType(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersRealizationsType> patch)
        {
            var entity = await _context.Set<OrdersRealizationsType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersRealizationsType(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersRealizationsType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersRealizationsType>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
