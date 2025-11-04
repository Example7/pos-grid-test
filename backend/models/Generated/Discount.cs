using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Discount
{
    public long DiscountId { get; set; }

    public string DiscountName { get; set; } = null!;

    public string DiscountType { get; set; } = null!;

    public decimal DiscountValue { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<DiscountsProduct> DiscountsProducts { get; set; } = new List<DiscountsProduct>();

    public virtual ICollection<DiscountsStore> DiscountsStores { get; set; } = new List<DiscountsStore>();

    public virtual ICollection<OrdersItem> OrdersItems { get; set; } = new List<OrdersItem>();
}
