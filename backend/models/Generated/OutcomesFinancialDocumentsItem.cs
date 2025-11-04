using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

/// <summary>
/// Pozycje dokumentów finansowych sprzedaży
/// </summary>
public partial class OutcomesFinancialDocumentsItem
{
    [Key]
    public long ProductId { get; set; }

    public decimal Quantity { get; set; }

    /// <summary>
    /// Cena kosztowa netto jednostkowa - dla celów raportowych (marża)
    /// </summary>
    public decimal? CostPrice { get; set; }

    public decimal GrossPrice { get; set; }

    public long ProductVatRateId { get; set; }

    public decimal VatRateValue { get; set; }

    public Guid CreatedByEmployeeId { get; set; }

    public DateTime? CreatedAt { get; set; }

    public Guid? OutcomeFinancialDocumentId { get; set; }

    public Guid OutcomeFinancialDocumentItemId { get; set; }

    public Guid? OutcomeItemId { get; set; }

    /// <summary>
    /// Wartość kosztowa pozycji - automatycznie obliczana jako ROUND(cost_price * quantity, 2)
    /// </summary>
    public decimal? CostValue { get; set; }

    /// <summary>
    /// Wartość brutto pozycji - automatycznie obliczana jako ROUND(quantity * gross_price, 2)
    /// </summary>
    public decimal? GrossValue { get; set; }

    /// <summary>
    /// Wartość netto pozycji - automatycznie obliczana jako ROUND((quantity * gross_price) / (1 + vat_rate_value/100), 2)
    /// </summary>
    public decimal? NetValue { get; set; }

    /// <summary>
    /// Wartość VAT pozycji - automatycznie wyliczana z gross_price, quantity i przechowywanego vat_rate_value
    /// </summary>
    public decimal? VatValue { get; set; }

    public virtual Employee CreatedByEmployee { get; set; } = null!;

    public virtual OutcomesFinancialDocument? OutcomeFinancialDocument { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductsVatRate ProductVatRate { get; set; } = null!;
}
