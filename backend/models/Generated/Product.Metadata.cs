using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace DevExpress.Models.Generated
{
    [ModelMetadataType(typeof(ProductMetadata))]
    public partial class Product
    {
    }

    public class ProductMetadata
    {
        [Required]
        [StringLength(100, MinimumLength = 3)]
        public string ProductName { get; set; } = null!;

        [StringLength(500)]
        public string? ProductDescription { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cena nie mo¿e byæ ujemna")]
        public decimal ProductPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cena oryginalna nie mo¿e byæ ujemna")]
        public decimal? OriginalPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Cena po zni¿ce nie mo¿e byæ ujemna")]
        public decimal? DiscountedPrice { get; set; }

        [Range(0, double.MaxValue, ErrorMessage = "Minimalny stan nie mo¿e byæ ujemny")]
        public decimal? MinStockLevel { get; set; }
    }
}
