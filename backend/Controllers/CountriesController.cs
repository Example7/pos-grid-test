using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class CountriesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public CountriesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Countries
        [EnableQuery]
        public IActionResult Get() => Ok(_context.Countries);

        // GET: odata/Countries(1)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var country = _context.Countries.FirstOrDefault(c => c.CountryId == key);
            return country == null ? NotFound() : Ok(country);
        }

        // POST: odata/Countries
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Country entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(entity.CountryName))
                return BadRequest("Pole 'CountryName' jest wymagane.");

            entity.CreatedAt = DateTime.UtcNow;

            _context.Countries.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Countries(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<Country> patch)
        {
            var entity = await _context.Countries.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Countries(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Countries.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Countries.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
