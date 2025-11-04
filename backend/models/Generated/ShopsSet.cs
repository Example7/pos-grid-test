using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class ShopsSet
{
    public long ShopSetId { get; set; }

    public long ShopId { get; set; }

    public long SetId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Set Set { get; set; } = null!;

    public virtual Shop Shop { get; set; } = null!;
}
