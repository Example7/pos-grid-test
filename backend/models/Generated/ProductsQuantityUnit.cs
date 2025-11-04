using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class ProductsQuantityUnit
{
    [Key]
    public long ProductQuantityUnitId { get; set; }

    public string UnitName { get; set; } = null!;

    public string? UnitSymbol { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
