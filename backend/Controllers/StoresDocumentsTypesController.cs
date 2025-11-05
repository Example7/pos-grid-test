using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class StoresDocumentsTypesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public StoresDocumentsTypesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/StoresDocumentsType
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<StoresDocumentsType>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/StoresDocumentsType(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<StoresDocumentsType>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/StoresDocumentsType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StoresDocumentsType entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<StoresDocumentsType>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/StoresDocumentsType(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<StoresDocumentsType> patch)
        {
            var entity = await _context.Set<StoresDocumentsType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/StoresDocumentsType(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<StoresDocumentsType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<StoresDocumentsType>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
