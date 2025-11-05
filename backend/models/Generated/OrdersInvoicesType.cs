using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OrdersInvoicesType
{
    [Key]
    public long OrderInvoiceTypeId { get; set; }

    public string InvoiceTypeName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<OrdersInvoice> OrdersInvoices { get; set; } = new List<OrdersInvoice>();
}
