using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Product
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Product>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Product(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Product>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Product
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Product entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Product>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Product(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Product> patch)
        {
            var entity = await _context.Set<Product>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Product(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Product>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Product>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
