using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class OutcomesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public OutcomesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Outcomes
        [EnableQuery]
        public IActionResult Get() =>
            Ok(_context.Outcomes
                .Include(o => o.Contractor)
                .Include(o => o.Store)
                .Include(o => o.Pos)
                .Include(o => o.OutcomeCreatedByEmployee)
                .Include(o => o.OutcomesItems)
            );

        // GET: odata/Outcomes(key)
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var entity = _context.Outcomes
                .Include(o => o.OutcomesItems)
                .FirstOrDefault(o => o.OutcomeId == key);

            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Outcomes
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Outcome entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            entity.OutcomeId = Guid.NewGuid();
            entity.CreatedAt = DateTime.UtcNow;

            // automatyczne sumowanie pozycji, jeœli istniej¹
            await RecalculateTotalsAsync(entity);

            _context.Outcomes.Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Outcomes(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Outcome> patch)
        {
            var entity = await _context.Outcomes
                .Include(o => o.OutcomesItems)
                .FirstOrDefaultAsync(o => o.OutcomeId == key);

            if (entity == null)
                return NotFound();

            patch.Patch(entity);

            await RecalculateTotalsAsync(entity);

            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Outcomes(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Outcomes.FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Outcomes.Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        // Pomocnicza metoda do przeliczania sum wartoœci
        private async Task RecalculateTotalsAsync(Outcome outcome)
        {
            if (outcome.OutcomeId == Guid.Empty)
                return;

            // pobierz pozycje powi¹zane z tym dokumentem
            var items = await _context.OutcomesItems
                .Where(i => i.OutcomeId == outcome.OutcomeId)
                .ToListAsync();

            if (items.Any())
            {
                outcome.TotalCostValue = items.Sum(i => i.CostValue ?? 0);
                outcome.TotalNetValue = items.Sum(i => i.NetValue ?? 0);
                outcome.TotalGrossValue = items.Sum(i => i.GrossValue ?? 0);
            }
        }
    }
}
