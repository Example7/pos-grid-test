using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductsBarcodesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsBarcodesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsBarcode
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<ProductsBarcode>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/ProductsBarcode(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<ProductsBarcode>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/ProductsBarcode
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsBarcode entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<ProductsBarcode>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsBarcode(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<ProductsBarcode> patch)
        {
            var entity = await _context.Set<ProductsBarcode>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsBarcode(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<ProductsBarcode>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<ProductsBarcode>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
