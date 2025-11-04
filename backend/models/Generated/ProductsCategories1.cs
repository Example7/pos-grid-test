using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class ProductsCategories1
{
    [Key]
    public long ProductCategory1Id { get; set; }

    public string Category1Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();

    public virtual ICollection<ProductsCategories2> ProductsCategories2s { get; set; } = new List<ProductsCategories2>();
}
