using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Store
{
    public long StoreId { get; set; }

    public string StoreName { get; set; } = null!;

    public string? StoreAddress { get; set; }

    public string? StoreCountryId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<DiscountsStore> DiscountsStores { get; set; } = new List<DiscountsStore>();

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Inorder> Inorders { get; set; } = new List<Inorder>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();

    public virtual ICollection<Pose> Poses { get; set; } = new List<Pose>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
