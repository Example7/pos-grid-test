using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class ProductSetsItem
{
    public long? ProductId { get; set; }

    public long ProductSetId { get; set; }

    public string SetName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Product? Product { get; set; }
}
