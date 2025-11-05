using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class DiscountsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public DiscountsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Discount
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Discount>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Discount(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Discount>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Discount
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Discount entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Discount>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Discount(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Discount> patch)
        {
            var entity = await _context.Set<Discount>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Discount(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Discount>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Discount>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
