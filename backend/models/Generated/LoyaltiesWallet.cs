using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class LoyaltiesWallet
{
    public long LoyaltyWalletId { get; set; }

    public string WalletName { get; set; } = null!;

    public decimal? PointsBalance { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
