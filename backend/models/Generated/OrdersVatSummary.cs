using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class OrdersVatSummary
{
    public long OrderVatSummaryId { get; set; }

    public long? ProductVatRateId { get; set; }

    public decimal VatAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual ProductsVatRate? ProductVatRate { get; set; }
}
