using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesFinancialDocumentsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesFinancialDocumentsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // LISTA: odata/OutcomesFinancialDocuments
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.OutcomesFinancialDocuments
                .Include(d => d.OutcomesFinancialDocumentsItems)
                    .ThenInclude(i => i.Product)
                .Include(d => d.OutcomesFinancialDocumentsVatSummaries)
                .Include(d => d.FinancialDocumentType)
                .Include(d => d.FinancialDocumentStatus)
                .Include(d => d.Contractor)
                .AsNoTracking();

            return Ok(query);
        }

        // POJEDYNCZY: odata/OutcomesFinancialDocuments(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var query = _context.OutcomesFinancialDocuments
                .Where(d => d.OutcomeFinancialDocumentId == key)
                .Include(d => d.OutcomesFinancialDocumentsItems)
                    .ThenInclude(i => i.Product)
                .Include(d => d.OutcomesFinancialDocumentsVatSummaries)
                .Include(d => d.FinancialDocumentType)
                .Include(d => d.FinancialDocumentStatus)
                .Include(d => d.Contractor)
                .AsNoTracking();

            return Ok(query);
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocument entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OutcomesFinancialDocument>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesFinancialDocument> patch)
        {
            var entity = await _context.Set<OutcomesFinancialDocument>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OutcomesFinancialDocument>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OutcomesFinancialDocument>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
