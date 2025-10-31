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
            product.CreatedAt = DateTime.Now;
            product.UpdatedAt = DateTime.Now;
            _context.Products.Add(product);
            await _context.SaveChangesAsync();
            return Created(product);
        }

        public async Task<IActionResult> Patch(int key, [FromBody] Delta<Product> patch)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null) return NotFound();

            patch.Patch(product);
            product.UpdatedAt = DateTime.Now;
            await _context.SaveChangesAsync();
            return Updated(product);
        }

        public async Task<IActionResult> Delete(int key)
        {
            var product = await _context.Products.FindAsync(key);
            if (product == null) return NotFound();

            _context.Products.Remove(product);
            await _context.SaveChangesAsync();
            return NoContent();
        }
    }
}
