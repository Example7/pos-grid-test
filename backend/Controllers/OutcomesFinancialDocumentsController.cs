using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Deltas;
using DevExpress.Data;
using DevExpress.Models.Generated;

namespace DevExpress.Controllers
{
    public class OutcomesFinancialDocumentsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesFinancialDocumentsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesFinancialDocuments
        [EnableQuery]
        public IActionResult Get() => Ok(_context.OutcomesFinancialDocuments);

        // GET: odata/OutcomesFinancialDocuments(<GUID>)
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var doc = _context.OutcomesFinancialDocuments
                .FirstOrDefault(d => d.OutcomeFinancialDocumentId == key);
            return doc == null ? NotFound() : Ok(doc);
        }

        // POST: odata/OutcomesFinancialDocuments
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocument doc)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.OutcomesFinancialDocuments.Add(doc);
            await _context.SaveChangesAsync();
            return Created(doc);
        }

        // PATCH: odata/OutcomesFinancialDocuments(<GUID>)
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesFinancialDocument> patch)
        {
            var doc = await _context.OutcomesFinancialDocuments.FindAsync(key);
            if (doc == null) return NotFound();

            patch.Patch(doc);
            await _context.SaveChangesAsync();
            return Ok(doc);
        }

        // DELETE: odata/OutcomesFinancialDocuments(<GUID>)
        public async Task<IActionResult> Delete(Guid key)
        {
            var doc = await _context.OutcomesFinancialDocuments.FindAsync(key);
            if (doc == null) return NotFound();

            _context.OutcomesFinancialDocuments.Remove(doc);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
