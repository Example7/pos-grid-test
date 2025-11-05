using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductsVatRatesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsVatRatesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsVatRate
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<ProductsVatRate>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/ProductsVatRate(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<ProductsVatRate>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/ProductsVatRate
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsVatRate entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<ProductsVatRate>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsVatRate(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<ProductsVatRate> patch)
        {
            var entity = await _context.Set<ProductsVatRate>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsVatRate(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<ProductsVatRate>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<ProductsVatRate>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
