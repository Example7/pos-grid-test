using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class FinancialDocumentsTypesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public FinancialDocumentsTypesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/FinancialDocumentsType
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<FinancialDocumentsType>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/FinancialDocumentsType(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<FinancialDocumentsType>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/FinancialDocumentsType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] FinancialDocumentsType entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<FinancialDocumentsType>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/FinancialDocumentsType(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<FinancialDocumentsType> patch)
        {
            var entity = await _context.Set<FinancialDocumentsType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/FinancialDocumentsType(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<FinancialDocumentsType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<FinancialDocumentsType>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
