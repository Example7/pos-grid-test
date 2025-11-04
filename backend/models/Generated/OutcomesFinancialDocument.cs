using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

/// <summary>
/// Dokumenty finansowe sprzedaży (paragony, faktury)
/// </summary>
public partial class OutcomesFinancialDocument
{
    public int StoreId { get; set; }

    public int PosId { get; set; }

    /// <summary>
    /// Data dokumentu finansowego
    /// </summary>
    public DateOnly? FinancialDocumentDate { get; set; }

    public int? YearId { get; set; }

    /// <summary>
    /// Status dokumentu finansowego
    /// </summary>
    public long? FinancialDocumentStatusId { get; set; }

    /// <summary>
    /// Typ dokumentu finansowego
    /// </summary>
    public long FinancialDocumentTypeId { get; set; }

    /// <summary>
    /// Numer dokumentu finansowego
    /// </summary>
    public string? FinancialDocumentNumber { get; set; }

    /// <summary>
    /// NIP klienta
    /// </summary>
    public string? CustomerNip { get; set; }

    public DateTime? CreatedAt { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public Guid? OrderId { get; set; }

    /// <summary>
    /// Nazwa klienta
    /// </summary>
    public string? CustomerName { get; set; }

    [Key]
    public Guid OutcomeFinancialDocumentId { get; set; }

    public Guid? OutcomeId { get; set; }

    /// <summary>
    /// ID kontrahenta/klienta
    /// </summary>
    public Guid? ContractorId { get; set; }

    /// <summary>
    /// Suma wartości kosztowych wszystkich pozycji
    /// </summary>
    public decimal? TotalCostValue { get; set; }

    /// <summary>
    /// Suma wartości netto wszystkich pozycji
    /// </summary>
    public decimal? TotalNetValue { get; set; }

    /// <summary>
    /// Suma wartości brutto wszystkich pozycji
    /// </summary>
    public decimal? TotalGrossValue { get; set; }

    public virtual Contractor? Contractor { get; set; }

    public virtual OutcomesFinancialDocumentsStatus? FinancialDocumentStatus { get; set; }

    public virtual FinancialDocumentsType FinancialDocumentType { get; set; } = null!;

    public virtual Order? Order { get; set; }

    public virtual Outcome? Outcome { get; set; }

    public virtual ICollection<OutcomesFinancialDocumentsItem> OutcomesFinancialDocumentsItems { get; set; } = new List<OutcomesFinancialDocumentsItem>();

    public virtual ICollection<OutcomesFinancialDocumentsVatSummary> OutcomesFinancialDocumentsVatSummaries { get; set; } = new List<OutcomesFinancialDocumentsVatSummary>();
}
