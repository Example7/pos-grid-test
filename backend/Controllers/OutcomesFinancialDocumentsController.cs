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

        // GET: odata/OutcomesFinancialDocument
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OutcomesFinancialDocument>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OutcomesFinancialDocument(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OutcomesFinancialDocument>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OutcomesFinancialDocument
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocument entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OutcomesFinancialDocument>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesFinancialDocument(key)
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

        // DELETE: odata/OutcomesFinancialDocument(key)
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
