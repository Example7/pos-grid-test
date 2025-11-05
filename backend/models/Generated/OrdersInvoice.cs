using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OrdersInvoice
{
    [Key]
    public long OrderInvoiceId { get; set; }

    public long? OrderInvoiceTypeId { get; set; }

    public string InvoiceNumber { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public Guid? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual OrdersInvoicesType? OrderInvoiceType { get; set; }
}
