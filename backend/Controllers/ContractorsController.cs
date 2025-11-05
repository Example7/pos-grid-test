using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ContractorsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ContractorsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Contractor
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Contractor>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Contractor(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Contractor>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Contractor
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contractor entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Contractor>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Contractor(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Contractor> patch)
        {
            var entity = await _context.Set<Contractor>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Contractor(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Contractor>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Contractor>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
