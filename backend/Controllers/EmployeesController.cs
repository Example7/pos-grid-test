using DevExpress.Data;
using DevExpress.Models.Generated;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class EmployeesController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public EmployeesController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/Employees
        [EnableQuery]
        public IActionResult Get() => Ok(_context.Employees);

        // GET: odata/Employees(1)
        [EnableQuery]
        public IActionResult Get(Guid key)
        {
            var employee = _context.Employees.FirstOrDefault(e => e.EmployeeId == key);
            return employee == null ? NotFound() : Ok(employee);
        }

        // POST: odata/Employees
        [HttpPost]
        public async Task<IActionResult> Post([FromBody] Employee employee)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(employee.EmployeeName))
                return BadRequest("Pole 'EmployeeName' jest wymagane.");

            // Automatyczne daty
            employee.CreatedAt = DateTime.UtcNow;

            _context.Employees.Add(employee);
            await _context.SaveChangesAsync();
            return Created(employee);
        }

        // PATCH: odata/Employees(1)
        [HttpPatch]
        public async Task<IActionResult> Patch(Guid key, [FromBody] Delta<Employee> patch)
        {
            var employee = await _context.Employees.FindAsync(key);
            if (employee == null)
                return NotFound();

            patch.Patch(employee);

            await _context.SaveChangesAsync();
            return Ok(employee);
        }

        // DELETE: odata/Employees(1)
        [HttpDelete]
        public async Task<IActionResult> Delete(Guid key)
        {
            var employee = await _context.Employees.FindAsync(key);
            if (employee == null)
                return NotFound();

            _context.Employees.Remove(employee);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
