using DevExpress.Data;
using DevExpress.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class DevicesController : ODataController
    {
        private readonly AppDbContext _context;
        public DevicesController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Devices);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var device = _context.Devices.FirstOrDefault(d => d.Id == key);
            return device == null ? NotFound() : Ok(device);
        }

        public async Task<IActionResult> Post([FromBody] Device device)
        {
            device.CreatedAt = DateTime.UtcNow;
            device.UpdatedAt = DateTime.UtcNow;
            _context.Devices.Add(device);
            await _context.SaveChangesAsync();
            return Created(device);
        }

        public async Task<IActionResult> Patch(int key, [FromBody] Delta<Device> patch)
        {
            var device = await _context.Devices.FindAsync(key);
            if (device == null) return NotFound();

            patch.Patch(device);
            device.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(device);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var device = await _context.Devices.FindAsync(key);
            if (device == null) return NotFound();

            _context.Devices.Remove(device);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
