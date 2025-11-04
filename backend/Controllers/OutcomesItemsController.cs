using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class OutcomesItemsController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesItemsController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/OutcomesItems
        [EnableQuery]
        public IActionResult Get() => Ok(_context.OutcomesItems);

        // GET: odata/OutcomesItems(key)
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var item = _context.OutcomesItems.FirstOrDefault(x => x.OutcomeItemId == key);
            return item == null ? NotFound() : Ok(item);
        }

        // POST: odata/OutcomesItems
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] OutcomesItem entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity.OutcomeItemId = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;
            entity.UpdatedAt = DateTime.UtcNow;

            // automatyczne obliczenia, jeœli dane dostêpne
            if (entity.Quantity > 0)
            {
                entity.CostValue = Math.Round(entity.CostPrice * entity.Quantity, 2);
                if (entity.NetPrice.HasValue)
                    entity.NetValue = Math.Round(entity.NetPrice.Value * entity.Quantity, 2);
                if (entity.GrossPrice.HasValue)
                    entity.GrossValue = Math.Round(entity.GrossPrice.Value * entity.Quantity, 2);
            }

            _context.OutcomesItems.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/OutcomesItems(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<OutcomesItem> patch)
        {
            var entity = await _context.OutcomesItems.FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            entity.UpdatedAt = DateTime.UtcNow;

            // przelicz wartoœci, jeœli Quantity lub ceny siê zmieni³y
            if (entity.Quantity > 0)
            {
                entity.CostValue = Math.Round(entity.CostPrice * entity.Quantity, 2);
                if (entity.NetPrice.HasValue)
                    entity.NetValue = Math.Round(entity.NetPrice.Value * entity.Quantity, 2);
                if (entity.GrossPrice.HasValue)
                    entity.GrossValue = Math.Round(entity.GrossPrice.Value * entity.Quantity, 2);
            }

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/OutcomesItems(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.OutcomesItems.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.OutcomesItems.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
