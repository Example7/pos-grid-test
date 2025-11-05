using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OrdersRealizationsTypesStatusesPathsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OrdersRealizationsTypesStatusesPathsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OrdersRealizationsTypesStatusesPath
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OrdersRealizationsTypesStatusesPath>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OrdersRealizationsTypesStatusesPath(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OrdersRealizationsTypesStatusesPath>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersRealizationsTypesStatusesPath
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersRealizationsTypesStatusesPath entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OrdersRealizationsTypesStatusesPath>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersRealizationsTypesStatusesPath(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersRealizationsTypesStatusesPath> patch)
        {
            var entity = await _context.Set<OrdersRealizationsTypesStatusesPath>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersRealizationsTypesStatusesPath(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OrdersRealizationsTypesStatusesPath>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OrdersRealizationsTypesStatusesPath>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
