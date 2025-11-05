using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class StoresDocumentsTypesCategorysController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public StoresDocumentsTypesCategorysController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/StoresDocumentsTypesCategory
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<StoresDocumentsTypesCategory>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/StoresDocumentsTypesCategory(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<StoresDocumentsTypesCategory>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/StoresDocumentsTypesCategory
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StoresDocumentsTypesCategory entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<StoresDocumentsTypesCategory>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/StoresDocumentsTypesCategory(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<StoresDocumentsTypesCategory> patch)
        {
            var entity = await _context.Set<StoresDocumentsTypesCategory>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/StoresDocumentsTypesCategory(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<StoresDocumentsTypesCategory>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<StoresDocumentsTypesCategory>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
