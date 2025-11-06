using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

/// <summary>
/// Podsumowania VAT dla dokumentów finansowych sprzedaży
/// </summary>
public partial class OutcomesFinancialDocumentsVatSummary
{
    public long ProductVatRateId { get; set; }

    public decimal? FinancialDocumentSummaryGrossValue { get; set; }

    public decimal? FinancialDocumentSummaryNetValue { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? OutcomeFinancialDocumentId { get; set; }

    [Key]
    public Guid OutcomeFinancialDocumentVatSummaryId { get; set; }

    /// <summary>
    /// Wartość stawki VAT (wstawiana z products_vat_rates przy tworzeniu rekordu) - przechowywana historycznie
    /// </summary>
    public decimal VatRateValue { get; set; }

    /// <summary>
    /// Wartość VAT - automatycznie wyliczana jako gross_value - net_value na podstawie przechowywanego vat_rate_value
    /// </summary>
    public decimal? FinancialDocumentSummaryVatValue { get; set; }

    public virtual OutcomesFinancialDocument? OutcomeFinancialDocument { get; set; }

    public virtual ProductsVatRate ProductVatRate { get; set; } = null!;
}
