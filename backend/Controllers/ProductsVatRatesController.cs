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
        public IActionResult Get()
        {
            return Ok(_context.ProductsVatRates);
        }

        // GET: odata/ProductsVatRates(1)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.ProductsVatRates
                .FirstOrDefault(x => x.ProductVatRateId == key);

            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/ProductsVatRates
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] ProductsVatRate entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity.CreatedAt = DateTime.UtcNow;

            _context.ProductsVatRates.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/ProductsVatRates(1)
        [HttpPatch]
        public async Task<IActionResult> Patch([FromRoute] long key, [FromBody] Delta<ProductsVatRate> patch)
        {
            var entity = await _context.ProductsVatRates.FindAsync(key);
            if (entity == null)
                return NotFound();

            try
            {
                // nie próbuj patchować relacji Products
                patch.TrySetPropertyValue(nameof(ProductsVatRate.Products), null);

                patch.Patch(entity);

                await _context.SaveChangesAsync();
                return Ok(entity);
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Błąd aktualizacji stawki VAT: {ex.Message}");
                return StatusCode(500, "Błąd aktualizacji stawki VAT: " + ex.Message);
            }
        }

        // DELETE: odata/ProductsVatRates(1)
        [HttpDelete]
        public async Task<IActionResult> Delete([FromRoute] long key)
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
