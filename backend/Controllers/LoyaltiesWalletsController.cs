using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class LoyaltiesWalletsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public LoyaltiesWalletsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/LoyaltiesWallet
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<LoyaltiesWallet>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/LoyaltiesWallet(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<LoyaltiesWallet>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/LoyaltiesWallet
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] LoyaltiesWallet entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<LoyaltiesWallet>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/LoyaltiesWallet(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<LoyaltiesWallet> patch)
        {
            var entity = await _context.Set<LoyaltiesWallet>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/LoyaltiesWallet(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<LoyaltiesWallet>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<LoyaltiesWallet>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
