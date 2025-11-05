using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class InordersController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public InordersController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Inorder
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Inorder>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Inorder(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Inorder>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Inorder
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Inorder entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Inorder>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Inorder(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Inorder> patch)
        {
            var entity = await _context.Set<Inorder>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Inorder(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Inorder>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Inorder>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
