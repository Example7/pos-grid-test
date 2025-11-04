using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class ContractorsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ContractorsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Contractors
        [EnableQuery]
        public IActionResult Get() => Ok(_context.Contractors);

        // GET: odata/Contractors(1)
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var contractor = _context.Contractors.FirstOrDefault(c => c.ContractorId == key);
            return contractor == null ? NotFound() : Ok(contractor);
        }

        // POST: odata/Contractors
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Contractor contractor)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            // Przyk³adowa walidacja: nazwa kontrahenta nie mo¿e byæ pusta
            if (string.IsNullOrWhiteSpace(contractor.ContractorName))
                return BadRequest("Pole 'ContractorName' jest wymagane.");

            contractor.CreatedAt = DateTime.UtcNow;

            _context.Contractors.Add(contractor);
            await _context.SaveChangesAsync();
            return Created(contractor);
        }

        // PATCH: odata/Contractors(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Contractor> patch)
        {
            var contractor = await _context.Contractors.FindAsync(key);
            if (contractor == null)
                return NotFound();

            patch.Patch(contractor);

            await _context.SaveChangesAsync();
            return Ok(contractor);
        }

        // DELETE: odata/Contractors(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var contractor = await _context.Contractors.FindAsync(key);
            if (contractor == null)
                return NotFound();

            _context.Contractors.Remove(contractor);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}