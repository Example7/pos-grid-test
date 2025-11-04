using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class SetsItemsProduct
{
    public long SetItemProductId { get; set; }

    public long SetItemId { get; set; }

    public long ProductId { get; set; }

    public bool? SetItemProductIsActive { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OrdersItem> OrdersItems { get; set; } = new List<OrdersItem>();

    public virtual Product Product { get; set; } = null!;

    public virtual SetsItem SetItem { get; set; } = null!;
}
