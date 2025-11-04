using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class ShopsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ShopsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Shops
        [EnableQuery]
        public IActionResult Get() => Ok(_context.Shops);

        // GET: odata/Shops(1)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var shop = _context.Shops.FirstOrDefault(s => s.Id == key);
            return shop == null ? NotFound() : Ok(shop);
        }

        // POST: odata/Shops
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Shop shop)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(shop.ShopName))
                return BadRequest("Pole 'ShopName' jest wymagane.");

            shop.CreatedAt = DateTime.UtcNow;

            _context.Shops.Add(shop);
            await _context.SaveChangesAsync();
            return Created(shop);
        }

        // PATCH: odata/Shops(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<Shop> patch)
        {
            var shop = await _context.Shops.FindAsync(key);
            if (shop == null)
                return NotFound();

            patch.Patch(shop);

            await _context.SaveChangesAsync();
            return Ok(shop);
        }

        // DELETE: odata/Shops(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var shop = await _context.Shops.FindAsync(key);
            if (shop == null)
                return NotFound();

            _context.Shops.Remove(shop);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}