using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Inorder
{
    public long InorderId { get; set; }

    public string InorderNumber { get; set; } = null!;

    public long? StoreId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<InordersItem> InordersItems { get; set; } = new List<InordersItem>();

    public virtual Store? Store { get; set; }
}
