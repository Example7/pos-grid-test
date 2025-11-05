using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesFinancialDocumentsVatSummarysController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesFinancialDocumentsVatSummarysController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesFinancialDocumentsVatSummary
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OutcomesFinancialDocumentsVatSummary>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OutcomesFinancialDocumentsVatSummary(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OutcomesFinancialDocumentsVatSummary>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OutcomesFinancialDocumentsVatSummary
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocumentsVatSummary entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OutcomesFinancialDocumentsVatSummary>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesFinancialDocumentsVatSummary(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesFinancialDocumentsVatSummary> patch)
        {
            var entity = await _context.Set<OutcomesFinancialDocumentsVatSummary>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesFinancialDocumentsVatSummary(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OutcomesFinancialDocumentsVatSummary>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OutcomesFinancialDocumentsVatSummary>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
