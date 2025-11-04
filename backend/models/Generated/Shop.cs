using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Shop
{
    public long Id { get; set; }

    public string ShopName { get; set; } = null!;

    public Guid? ManagerId { get; set; }

    public bool IsActive { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Employee? Manager { get; set; }

    public virtual ICollection<ShopsSet> ShopsSets { get; set; } = new List<ShopsSet>();
}
