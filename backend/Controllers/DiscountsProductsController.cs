using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class DiscountsProductsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public DiscountsProductsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/DiscountsProduct
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<DiscountsProduct>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/DiscountsProduct(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<DiscountsProduct>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/DiscountsProduct
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] DiscountsProduct entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<DiscountsProduct>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/DiscountsProduct(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<DiscountsProduct> patch)
        {
            var entity = await _context.Set<DiscountsProduct>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/DiscountsProduct(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<DiscountsProduct>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<DiscountsProduct>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
