using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class ProductsVatRatesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsVatRatesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsVatRates
        [EnableQuery]
        public IActionResult Get() => Ok(_context.ProductsVatRates);

        // GET: odata/ProductsVatRates(1)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var item = _context.ProductsVatRates.FirstOrDefault(c => c.ProductVatRateId == key);
            return item == null ? NotFound() : Ok(item);
        }

        // POST: odata/ProductsVatRates
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsVatRate entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (entity.VatRateValue <= 0)
                return BadRequest("Wartoœæ stawki VAT musi byæ wiêksza od zera.");

            entity.CreatedAt = DateTime.UtcNow;

            _context.ProductsVatRates.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsVatRates(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<ProductsVatRate> patch)
        {
            var entity = await _context.ProductsVatRates.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/ProductsVatRates(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.ProductsVatRates.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.ProductsVatRates.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
