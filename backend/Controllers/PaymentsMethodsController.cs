using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class PaymentsMethodsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public PaymentsMethodsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/PaymentsMethod
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<PaymentsMethod>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/PaymentsMethod(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var entity = _context.Set<PaymentsMethod>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/PaymentsMethod
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] PaymentsMethod entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<PaymentsMethod>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/PaymentsMethod(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<PaymentsMethod> patch)
        {
            var entity = await _context.Set<PaymentsMethod>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/PaymentsMethod(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.Set<PaymentsMethod>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<PaymentsMethod>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
