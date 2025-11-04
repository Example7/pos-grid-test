using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class OrdersInvoicesType
{
    public long OrderInvoiceTypeId { get; set; }

    public string InvoiceTypeName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<OrdersInvoice> OrdersInvoices { get; set; } = new List<OrdersInvoice>();
}
