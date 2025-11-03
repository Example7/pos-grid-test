using DevExpress.Data;
using DevExpress.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ShopsController : ODataController
    {
        private readonly AppDbContext _context;
        public ShopsController(AppDbContext context) => _context = context;

        [EnableQuery]
        public IActionResult Get() => Ok(_context.Shops);

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var item = _context.Shops.FirstOrDefault(p => p.Id == key);
            return item == null ? NotFound() : Ok(item);
        }

        public async Task<IActionResult> Post([FromBody] Shop item)
        {
            item.CreatedAt = DateTime.UtcNow;
            item.UpdatedAt = DateTime.UtcNow;
            _context.Shops.Add(item);
            await _context.SaveChangesAsync();
            return Created(item);
        }

        public async Task<IActionResult> Patch(int key, [FromBody] Delta<Shop> patch)
        {
            var item = await _context.Shops.FindAsync(key);
            if (item == null) return NotFound();

            patch.Patch(item);
            item.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(item);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var item = await _context.Shops.FindAsync(key);
            if (item == null) return NotFound();

            _context.Shops.Remove(item);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
