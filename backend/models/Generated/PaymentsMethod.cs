using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class PaymentsMethod
{
    [Key]
    public long PaymentMethodId { get; set; }

    public string PaymentMethodName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();
}
