using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class OutcomesFinancialDocumentsStatusesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesFinancialDocumentsStatusesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesFinancialDocumentsStatuses
        [EnableQuery]
        public IActionResult Get() => Ok(_context.OutcomesFinancialDocumentsStatuses);

        // GET: odata/OutcomesFinancialDocumentsStatuses(key)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var entity = _context.OutcomesFinancialDocumentsStatuses
                .FirstOrDefault(e => e.OutcomeFinancialDocumentStatusId == key);

            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OutcomesFinancialDocumentsStatuses
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocumentsStatus entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.OutcomesFinancialDocumentsStatuses.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesFinancialDocumentsStatuses(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<OutcomesFinancialDocumentsStatus> patch)
        {
            var entity = await _context.OutcomesFinancialDocumentsStatuses.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesFinancialDocumentsStatuses(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.OutcomesFinancialDocumentsStatuses.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.OutcomesFinancialDocumentsStatuses.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
