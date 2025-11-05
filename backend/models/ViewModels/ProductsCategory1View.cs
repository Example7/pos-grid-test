using System;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.ViewModels
{
    public class ProductsCategory1View
    {
        [Key]
        public long ProductCategory1Id { get; set; }
        public string Category1Name { get; set; } = string.Empty;
        public DateTime CreatedAt { get; set; }
        public int ProductsCount { get; set; }
        public int SubcategoriesCount { get; set; }
    }
}
