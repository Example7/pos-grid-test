using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class SetsItem
{
    public long SetItemId { get; set; }

    public long SetId { get; set; }

    public string SetItemName { get; set; } = null!;

    public decimal? SetItemPrice { get; set; }

    public decimal? SetItemDiscountPercentRatio { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Set Set { get; set; } = null!;

    public virtual ICollection<SetsItemsProduct> SetsItemsProducts { get; set; } = new List<SetsItemsProduct>();
}
