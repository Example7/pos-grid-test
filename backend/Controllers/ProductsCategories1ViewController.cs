using DevExpress.Data;
using DevExpress.Models.Generated;
using DevExpress.Models.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.OData.Query;
using Microsoft.AspNetCore.OData.Routing.Controllers;

namespace DevExpress.Controllers
{
    public class ProductsCategories1ViewController : ODataController
    {
        private readonly SupabaseDbContext _context;

        public ProductsCategories1ViewController(SupabaseDbContext context)
        {
            _context = context;
        }

        // GET: odata/ProductsCategories1View
        [EnableQuery]
        public IActionResult Get()
        {
            var query = _context.ProductsCategories1s
                .Select(c => new ProductsCategory1View
                {
                    ProductCategory1Id = c.ProductCategory1Id,
                    Category1Name = c.Category1Name,
                    CreatedAt = c.CreatedAt,
                    ProductsCount = c.Products.Count(),
                    SubcategoriesCount = c.ProductsCategories2s.Count()
                });

            return Ok(query);
        }

        // GET: odata/ProductsCategories1View(1)
        [EnableQuery]
        public IActionResult Get([FromRoute] long key)
        {
            var item = _context.ProductsCategories1s
                .Where(c => c.ProductCategory1Id == key)
                .Select(c => new ProductsCategory1View
                {
                    ProductCategory1Id = c.ProductCategory1Id,
                    Category1Name = c.Category1Name,
                    CreatedAt = c.CreatedAt,
                    ProductsCount = c.Products.Count(),
                    SubcategoriesCount = c.ProductsCategories2s.Count()
                })
                .FirstOrDefault();

            return item == null ? NotFound() : Ok(item);
        }
    }
}
