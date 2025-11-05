using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesFinancialDocumentsStatussController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesFinancialDocumentsStatussController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesFinancialDocumentsStatus
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OutcomesFinancialDocumentsStatus>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OutcomesFinancialDocumentsStatus(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OutcomesFinancialDocumentsStatus>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OutcomesFinancialDocumentsStatus
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocumentsStatus entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OutcomesFinancialDocumentsStatus>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesFinancialDocumentsStatus(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesFinancialDocumentsStatus> patch)
        {
            var entity = await _context.Set<OutcomesFinancialDocumentsStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesFinancialDocumentsStatus(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OutcomesFinancialDocumentsStatus>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OutcomesFinancialDocumentsStatus>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
