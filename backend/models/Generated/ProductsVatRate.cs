using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class ProductsVatRate
{
    [Key]
    public long ProductVatRateId { get; set; }

    public string VatRateName { get; set; } = null!;

    public decimal VatRateValue { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<IncomesItem> IncomesItems { get; set; } = new List<IncomesItem>();

    public virtual ICollection<OrdersItem> OrdersItems { get; set; } = new List<OrdersItem>();

    public virtual ICollection<OrdersVatSummary> OrdersVatSummaries { get; set; } = new List<OrdersVatSummary>();

    public virtual ICollection<OutcomesFinancialDocumentsItem> OutcomesFinancialDocumentsItems { get; set; } = new List<OutcomesFinancialDocumentsItem>();

    public virtual ICollection<OutcomesFinancialDocumentsVatSummary> OutcomesFinancialDocumentsVatSummaries { get; set; } = new List<OutcomesFinancialDocumentsVatSummary>();

    public virtual ICollection<OutcomesItem> OutcomesItems { get; set; } = new List<OutcomesItem>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
