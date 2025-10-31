using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevExpress.Models
{
    [Table("products")]
    public class Product
    {
        [Column("id")]
        public int Id { get; set; }

        [Column("name")]
        public string Name { get; set; } = string.Empty;

        [Column("category")]
        public string Category { get; set; } = string.Empty;

        [Column("price")]
        public decimal Price { get; set; }

        [Column("stock")]
        public int Stock { get; set; }

        [Column("active")]
        public bool Active { get; set; } = true;

        [Column("sku")]
        public string? Sku { get; set; }

        [Column("brand")]
        public string? Brand { get; set; }

        [Column("supplier")]
        public string? Supplier { get; set; }

        [Column("warehouse")]
        public string? Warehouse { get; set; }

        [Column("discount")]
        public decimal? Discount { get; set; }

        [Column("rating")]
        public decimal? Rating { get; set; }

        [Column("color")]
        public string? Color { get; set; }

        [Column("size")]
        public string? Size { get; set; }

        [Column("description")]
        public string? Description { get; set; }

        [Column("created_at")]
        public DateTime? CreatedAt { get; set; }

        [Column("updated_at")]
        public DateTime? UpdatedAt { get; set; }
    }
}
