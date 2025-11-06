using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductsRecipesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsRecipesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsRecipe
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<ProductsRecipe>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/ProductsRecipe(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<ProductsRecipe>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/ProductsRecipe
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsRecipe entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<ProductsRecipe>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsRecipe(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<ProductsRecipe> patch)
        {
            var entity = await _context.Set<ProductsRecipe>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsRecipe(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<ProductsRecipe>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<ProductsRecipe>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
