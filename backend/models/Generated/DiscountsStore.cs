using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class DiscountsStore
{
    public long DiscountStoreId { get; set; }

    public long DiscountId { get; set; }

    public long StoreId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Discount Discount { get; set; } = null!;

    public virtual Store Store { get; set; } = null!;
}
