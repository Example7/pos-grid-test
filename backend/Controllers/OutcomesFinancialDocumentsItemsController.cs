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

        // GET: odata/OutcomesFinancialDocumentsItem
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<OutcomesFinancialDocumentsItem>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/OutcomesFinancialDocumentsItem(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<OutcomesFinancialDocumentsItem>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/OutcomesFinancialDocumentsItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesFinancialDocumentsItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<OutcomesFinancialDocumentsItem>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesFinancialDocumentsItem(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesFinancialDocumentsItem> patch)
        {
            var entity = await _context.Set<OutcomesFinancialDocumentsItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesFinancialDocumentsItem(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<OutcomesFinancialDocumentsItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<OutcomesFinancialDocumentsItem>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
