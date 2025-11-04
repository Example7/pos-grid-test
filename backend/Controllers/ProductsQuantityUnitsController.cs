using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class ProductsQuantityUnitsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsQuantityUnitsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsQuantityUnits
        [EnableQuery]
        public IActionResult Get() => Ok(_context.ProductsQuantityUnits);

        // GET: odata/ProductsQuantityUnits(1)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var item = _context.ProductsQuantityUnits.FirstOrDefault(c => c.ProductQuantityUnitId == key);
            return item == null ? NotFound() : Ok(item);
        }

        // POST: odata/ProductsQuantityUnits
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsQuantityUnit entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(entity.UnitName))
                return BadRequest("Pole 'ProductQuantityUnitName' jest wymagane.");

            entity.CreatedAt = DateTime.UtcNow;

            _context.ProductsQuantityUnits.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsQuantityUnits(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<ProductsQuantityUnit> patch)
        {
            var entity = await _context.ProductsQuantityUnits.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsQuantityUnits(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.ProductsQuantityUnits.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.ProductsQuantityUnits.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
