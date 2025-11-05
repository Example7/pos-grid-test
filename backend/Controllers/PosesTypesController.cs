using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class PosesTypesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public PosesTypesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/PosesType
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<PosesType>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/PosesType(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<PosesType>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/PosesType
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PosesType entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<PosesType>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/PosesType(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<PosesType> patch)
        {
            var entity = await _context.Set<PosesType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/PosesType(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<PosesType>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<PosesType>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
