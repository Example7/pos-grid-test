using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class CountrysController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public CountrysController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Country
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Country>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Country(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Country>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Country
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Country entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Country>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Country(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Country> patch)
        {
            var entity = await _context.Set<Country>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Country(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Country>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Country>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
