using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class StoresController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public StoresController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Store
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Store>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Store(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<Store>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Store
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Store entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Store>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Store(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<Store> patch)
        {
            var entity = await _context.Set<Store>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Store(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<Store>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Store>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
