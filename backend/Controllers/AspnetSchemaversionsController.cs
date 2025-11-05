using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class AspnetSchemaversionsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public AspnetSchemaversionsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/AspnetSchemaversion
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<AspnetSchemaversion>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/AspnetSchemaversion(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<AspnetSchemaversion>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/AspnetSchemaversion
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] AspnetSchemaversion entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<AspnetSchemaversion>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/AspnetSchemaversion(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<AspnetSchemaversion> patch)
        {
            var entity = await _context.Set<AspnetSchemaversion>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/AspnetSchemaversion(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<AspnetSchemaversion>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<AspnetSchemaversion>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
