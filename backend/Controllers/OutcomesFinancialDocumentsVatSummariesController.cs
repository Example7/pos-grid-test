using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesFinancialDocumentsVatSummariesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesFinancialDocumentsVatSummariesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesFinancialDocumentsVatSummaries
        [EnableQuery]
        public IActionResult Get() =>
            Ok(_context.OutcomesFinancialDocumentsVatSummaries
                .Include(v => v.ProductVatRate)
                .Include(v => v.OutcomeFinancialDocument)
            );

        // GET: odata/OutcomesFinancialDocumentsVatSummaries(key)
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var entity = _context.OutcomesFinancialDocumentsVatSummaries
                .Include(v => v.ProductVatRate)
                .FirstOrDefault(v => v.OutcomeFinancialDocumentVatSummaryId == key);

            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OutcomesFinancialDocumentsVatSummaries
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocumentsVatSummary entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity.OutcomeFinancialDocumentVatSummaryId = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;

            CalculateVatValue(entity);

            _context.OutcomesFinancialDocumentsVatSummaries.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesFinancialDocumentsVatSummaries(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesFinancialDocumentsVatSummary> patch)
        {
            var entity = await _context.OutcomesFinancialDocumentsVatSummaries.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);

            entity.UpdatedAt = DateTime.UtcNow;
            CalculateVatValue(entity);

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesFinancialDocumentsVatSummaries(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.OutcomesFinancialDocumentsVatSummaries.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.OutcomesFinancialDocumentsVatSummaries.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Pomocnicza metoda do przeliczenia VAT
        private void CalculateVatValue(OutcomesFinancialDocumentsVatSummary entity)
        {
            if (entity.FinancialDocumentSummaryGrossValue.HasValue &&
                entity.FinancialDocumentSummaryNetValue.HasValue)
            {
                entity.FinancialDocumentSummaryVatValue =
                    Math.Round(entity.FinancialDocumentSummaryGrossValue.Value -
                               entity.FinancialDocumentSummaryNetValue.Value, 2);
            }
            else
            {
                entity.FinancialDocumentSummaryVatValue = null;
            }
        }
    }
}
