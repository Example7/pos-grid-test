using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Deltas;
using DevExpress.Models.Generated;
using DevExpress.Data;

namespace DevExpress.Controllers
{
    public class OrdersRealizationsTypesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersRealizationsTypesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersRealizationsTypes
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.OrdersRealizationsTypes);
        }

        // GET: odata/OrdersRealizationsTypes(5)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var entity = _context.OrdersRealizationsTypes
                .FirstOrDefault(o => o.OrderRealizationTypeId == key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersRealizationsTypes
        public async Task<IActionResult> Post([FromBody] OrdersRealizationsType type)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.OrdersRealizationsTypes.Add(type);
            await _context.SaveChangesAsync();
            return Created(type);
        }

        // PATCH: odata/OrdersRealizationsTypes(5)
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<OrdersRealizationsType> patch)
        {
            var entity = await _context.OrdersRealizationsTypes.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersRealizationsTypes(5)
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.OrdersRealizationsTypes.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.OrdersRealizationsTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
