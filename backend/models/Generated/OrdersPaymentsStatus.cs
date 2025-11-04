using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OrdersPaymentsStatus
{
    [Key]
    public long OrderPaymentStatusId { get; set; }

    public string? OrderPaymentStatusLogo { get; set; }

    public string OrderPaymentStatusName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
