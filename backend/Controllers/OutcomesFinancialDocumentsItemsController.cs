using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesFinancialDocumentsItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesFinancialDocumentsItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesFinancialDocumentsItems
        [EnableQuery]
        public IActionResult Get() =>
            Ok(_context.OutcomesFinancialDocumentsItems
                .Include(i => i.Product)
                .Include(i => i.ProductVatRate)
                .Include(i => i.CreatedByEmployee)
            );

        // GET: odata/OutcomesFinancialDocumentsItems(key)
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var item = _context.OutcomesFinancialDocumentsItems
                .Include(i => i.Product)
                .Include(i => i.ProductVatRate)
                .Include(i => i.CreatedByEmployee)
                .FirstOrDefault(i => i.OutcomeFinancialDocumentItemId == key);

            return item == null ? NotFound() : Ok(item);
        }

        // POST: odata/OutcomesFinancialDocumentsItems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocumentsItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity.OutcomeFinancialDocumentItemId = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;

            CalculateValues(entity);

            _context.OutcomesFinancialDocumentsItems.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesFinancialDocumentsItems(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesFinancialDocumentsItem> patch)
        {
            var entity = await _context.OutcomesFinancialDocumentsItems.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);

            CalculateValues(entity);

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesFinancialDocumentsItems(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.OutcomesFinancialDocumentsItems.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.OutcomesFinancialDocumentsItems.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Pomocnicza metoda do przeliczeñ wartoœci
        private void CalculateValues(OutcomesFinancialDocumentsItem entity)
        {
            if (entity.Quantity <= 0 || entity.GrossPrice <= 0)
                return;

            var vatRate = entity.VatRateValue / 100m;

            // Koszt
            if (entity.CostPrice.HasValue)
                entity.CostValue = Math.Round(entity.CostPrice.Value * entity.Quantity, 2);

            // Wartoœæ brutto
            entity.GrossValue = Math.Round(entity.Quantity * entity.GrossPrice, 2);

            // Wartoœæ netto
            entity.NetValue = Math.Round(entity.GrossValue.Value / (1 + vatRate), 2);

            // Wartoœæ VAT
            entity.VatValue = Math.Round(entity.GrossValue.Value - entity.NetValue.Value, 2);
        }
    }
}
