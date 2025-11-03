using DevExpress.Data;
using DevExpress.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class UsersController : ODataController
    {
        private readonly AppDbContext _context;
        public UsersController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Users);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var user = _context.Users.FirstOrDefault(u => u.Id == key);
            return user == null ? NotFound() : Ok(user);
        }

        public async Task<IActionResult> Post([FromBody] User user)
        {
            user.CreatedAt = DateTime.Now;
            user.UpdatedAt = DateTime.Now;
            _context.Users.Add(user);
            await _context.SaveChangesAsync();
            return Created(user);
        }

        public async Task<IActionResult> Patch(int key, [FromBody] Delta<User> patch)
        {
            var user = await _context.Users.FindAsync(key);
            if (user == null) return NotFound();

            patch.Patch(user);
            user.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return Updated(user);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var user = await _context.Users.FindAsync(key);
            if (user == null) return NotFound();

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
