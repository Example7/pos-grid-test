using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OrdersRealizationsStatus
{
    [Key]
    public long OrderRealizationStatusId { get; set; }

    public string? OrderRealizationStatusLogo { get; set; }

    public string OrderRealizationStatusName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
