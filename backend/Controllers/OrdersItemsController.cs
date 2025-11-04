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

        // GET: odata/OrdersItems
        [EnableQuery]
        public IActionResult Get() =>
            Ok(_context.OrdersItems
                .Include(o => o.Product)
                .Include(o => o.Discount)
                .Include(o => o.Order)
                .Include(o => o.OrderItemProductVatRate)
                .Include(o => o.OrderItemCreatedByEmploye)
            );

        // GET: odata/OrdersItems(key)
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var entity = _context.OrdersItems
                .Include(o => o.Product)
                .Include(o => o.Discount)
                .Include(o => o.OrderItemProductVatRate)
                .FirstOrDefault(o => o.OrderItemId == key);

            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OrdersItems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OrdersItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity.OrderItemId = Guid.NewGuid();
            entity.OrderItemCreatedAt = DateTime.UtcNow;

            CalculateValues(entity);

            _context.OrdersItems.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OrdersItems(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OrdersItem> patch)
        {
            var entity = await _context.OrdersItems.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            entity.UpdatedAt = DateTime.UtcNow;

            CalculateValues(entity);

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OrdersItems(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.OrdersItems.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.OrdersItems.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Pomocnicze przeliczanie wartoœci pozycji
        private void CalculateValues(OrdersItem entity)
        {
            if (entity.OrderItemGrossPrice.HasValue && entity.OrderItemQuantity > 0 && entity.OrderItemVatRateRatio.HasValue)
            {
                var grossValue = entity.OrderItemGrossPrice.Value * entity.OrderItemQuantity;
                entity.OrderItemGrossValue = Math.Round(grossValue, 2);

                // Net = Gross / (1 + VAT%)
                entity.OrderItemNetValue = Math.Round(
                    grossValue / (1 + (entity.OrderItemVatRateRatio.Value / 100)), 2);

                entity.OrderItemVatValue = Math.Round(
                    entity.OrderItemGrossValue.Value - entity.OrderItemNetValue.Value, 2);
            }
        }
    }
}
