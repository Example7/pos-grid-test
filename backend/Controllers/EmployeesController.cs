using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class EmployeesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public EmployeesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Employee
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.Set<Employee>().AsQueryable();

            return Ok(query);
        }

        // GET: odata/Employee(key)
        [EnableQuery]
        public IActionResult Get([FromRoute] Guid key)
        {
            var entity = _context.Set<Employee>().Find(key);
            return entity == null ? NotFound() : Ok(entity);
        }

        // POST: odata/Employee
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee entity)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            _context.Set<Employee>().Add(entity);
            await _context.SaveChangesAsync();
            return Created(entity);
        }

        // PATCH: odata/Employee(key)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Employee> patch)
        {
            var entity = await _context.Set<Employee>().FindAsync(key);
            if (entity == null)
                return NotFound();

            patch.Patch(entity);
            await _context.SaveChangesAsync();
            return Ok(entity);
        }

        // DELETE: odata/Employee(key)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var entity = await _context.Set<Employee>().FindAsync(key);
            if (entity == null)
                return NotFound();

            _context.Set<Employee>().Remove(entity);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
