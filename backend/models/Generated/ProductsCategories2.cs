using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class ProductsCategories2
{
    [Key]
    public long ProductCategory2Id { get; set; }

    public long? ProductCategory1Id { get; set; }

    public string Category2Name { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ProductsCategories1? ProductCategory1 { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
