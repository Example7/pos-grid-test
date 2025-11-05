using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductsRecipesItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsRecipesItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsRecipesItem
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<ProductsRecipesItem>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/ProductsRecipesItem(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<ProductsRecipesItem>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/ProductsRecipesItem
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsRecipesItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<ProductsRecipesItem>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsRecipesItem(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<ProductsRecipesItem> patch)
        {
            var entity = await _context.Set<ProductsRecipesItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsRecipesItem(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<ProductsRecipesItem>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<ProductsRecipesItem>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
