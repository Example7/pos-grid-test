using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class PosesTypesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public PosesTypesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/PosesTypes
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.PosesTypes);
        }

        // GET: odata/PosesTypes(5)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var entity = _context.PosesTypes.FirstOrDefault(e => e.PosTypeId == key);
            if (entity == null)
                return NotFound();

            return Ok(entity);
        }

        // POST: odata/PosesTypes
        public async Task<IActionResult> Post([FromBody] PosesType posType)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.PosesTypes.Add(posType);
            await _context.SaveChangesAsync();
            return Created(posType);
        }

        // PATCH: odata/PosesTypes(5)
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<PosesType> patch)
        {
            var entity = await _context.PosesTypes.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/PosesTypes(5)
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.PosesTypes.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.PosesTypes.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
