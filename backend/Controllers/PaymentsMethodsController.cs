using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.AspNetCore.OData.Deltas;
using DevExpress.Models.Generated;
using DevExpress.Data;

namespace DevExpress.Controllers
{
    public class PaymentsMethodsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public PaymentsMethodsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/PaymentsMethods
        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.PaymentsMethods);
        }

        // GET: odata/PaymentsMethods(5)
        [EnableQuery]
        public IActionResult Get(long key)
        {
            var entity = _context.PaymentsMethods.FirstOrDefault(p => p.PaymentMethodId == key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/PaymentsMethods
        public async Task<IActionResult> Post([FromBody] PaymentsMethod method)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.PaymentsMethods.Add(method);
            await _context.SaveChangesAsync();
            return Created(method);
        }

        // PATCH: odata/PaymentsMethods(5)
        public async Task<IActionResult> Patch(long key, [FromBody] Delta<PaymentsMethod> patch)
        {
            var entity = await _context.PaymentsMethods.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/PaymentsMethods(5)
        public async Task<IActionResult> Delete(long key)
        {
            var entity = await _context.PaymentsMethods.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.PaymentsMethods.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
