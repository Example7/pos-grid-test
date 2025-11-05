using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductsQuantityUnitsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsQuantityUnitsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsQuantityUnit
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<ProductsQuantityUnit>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/ProductsQuantityUnit(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<ProductsQuantityUnit>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/ProductsQuantityUnit
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsQuantityUnit entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<ProductsQuantityUnit>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsQuantityUnit(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<ProductsQuantityUnit> patch)
        {
            var entity = await _context.Set<ProductsQuantityUnit>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsQuantityUnit(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<ProductsQuantityUnit>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<ProductsQuantityUnit>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
