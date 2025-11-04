using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class StoresOrdersType
{
    public long StoreOrderTypeId { get; set; }

    public string StoreOrderTypeName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
