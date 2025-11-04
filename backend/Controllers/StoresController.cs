using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class StoresController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public StoresController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Stores
        [EnableQuery]
        public IActionResult Get() => Ok(_context.Stores);

        // GET: odata/Stores(1)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var store = _context.Stores.FirstOrDefault(s => s.StoreId == key);
            return store == null ? NotFound() : Ok(store);
        }

        // POST: odata/Stores
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Store store)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(store.StoreName))
                return BadRequest("Pole 'StoreName' jest wymagane.");

            store.CreatedAt = DateTime.UtcNow;

            _context.Stores.Add(store);
            await _context.SaveChangesAsync();
            return Created(store);
        }

        // PATCH: odata/Stores(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<Store> patch)
        {
            var store = await _context.Stores.FindAsync(key);
            if (store == null)
                return NotFound();

            patch.Patch(store);

            await _context.SaveChangesAsync();
            return Ok(store);
        }

        // DELETE: odata/Stores(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var store = await _context.Stores.FindAsync(key);
            if (store == null)
                return NotFound();

            _context.Stores.Remove(store);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
