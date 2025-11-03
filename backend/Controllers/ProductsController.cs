using DevExpress.Data;
using DevExpress.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Deltas;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;
using Microsoft.EntityFrameworkCore;

namespace DevExpress.Controllers
{
    public class ProductsController : ODataController
    {
        private readonly AppDbContext _context;

        public ProductsController(AppDbContext context)
        {
            _context = context;
        }

        [EnableQuery]
        public IActionResult Get()
        {
            return Ok(_context.Products);
        }

        [EnableQuery]
        public IActionResult Get(int key)
        {
            var product = _context.Products.FirstOrDefault(p => p.Id == key);
            return product == null ? NotFound() : Ok(product);
        }

        public async Task<IActionResult> Post([FromBody] Product product)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Pole 'Name' jest wymagane.");

            if (product.Price <= 0)
                return BadRequest("Cena musi być większa niż 0.");

            product.CreatedAt = DateTime.UtcNow;
            product.UpdatedAt = DateTime.UtcNow;

            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Created(product);
        }

        public async Task<IActionResult> Patch(int key, [FromBody] Delta<Product> patch)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null) return NotFound();

            patch.Patch(product);

            if (string.IsNullOrWhiteSpace(product.Name))
                return BadRequest("Pole 'Name' jest wymagane.");

            if (product.Price <= 0)
                return BadRequest("Cena musi być większa niż 0.");

            product.UpdatedAt = DateTime.UtcNow;
            await _context.SaveChangesAsync();
            return Ok(product);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        [HttpPost]
        [Route("api/CheckNameUnique")]
        public IActionResult CheckNameUnique([FromBody] ProductNameCheckDto dto)
        {
            if (dto == null || string.IsNullOrWhiteSpace(dto.Name))
                return BadRequest(new { isUnique = true });

            var exists = _context.Products
                .Any(p => p.Name == dto.Name && p.Id != dto.Id);

            return Ok(new { isUnique = !exists });
        }
    }

    public class ProductNameCheckDto
    {
        public int? Id { get; set; }
        public string Name { get; set; }
    }
}
