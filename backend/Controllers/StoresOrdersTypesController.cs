using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class StoresOrdersTypesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public StoresOrdersTypesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/StoresOrdersType
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<StoresOrdersType>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/StoresOrdersType(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<StoresOrdersType>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/StoresOrdersType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] StoresOrdersType entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<StoresOrdersType>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/StoresOrdersType(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<StoresOrdersType> patch)
        {
            var entity = await _context.Set<StoresOrdersType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/StoresOrdersType(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<StoresOrdersType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<StoresOrdersType>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
