using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Outcome
{
    public string OutcomeNumber { get; set; } = null!;

    public long? StoreId { get; set; }

    public long? PosId { get; set; }

    public long? OutcomeStatusId { get; set; }

    public long? YearId { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? OrderId { get; set; }

    public Guid OutcomeId { get; set; }

    public string? StoreDocumentTypeId { get; set; }

    public decimal? TotalCostValue { get; set; }

    public Guid? OutcomeCreatedByEmployeeId { get; set; }

    /// <summary>
    /// Suma wartości brutto wszystkich pozycji - wyliczane jako SUM(outcomes_items.gross_value)
    /// </summary>
    public decimal? TotalGrossValue { get; set; }

    public Guid? ContractorId { get; set; }

    /// <summary>
    /// Suma wartości netto wszystkich pozycji - wyliczane jako SUM(outcomes_items.net_value)
    /// </summary>
    public decimal? TotalNetValue { get; set; }

    public virtual Contractor? Contractor { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Employee? OutcomeCreatedByEmployee { get; set; }

    public virtual OutcomesStatus? OutcomeStatus { get; set; }

    public virtual ICollection<OutcomesFinancialDocument> OutcomesFinancialDocuments { get; set; } = new List<OutcomesFinancialDocument>();

    public virtual ICollection<OutcomesItem> OutcomesItems { get; set; } = new List<OutcomesItem>();

    public virtual Pose? Pos { get; set; }

    public virtual Store? Store { get; set; }

    public virtual StoresDocumentsType? StoreDocumentType { get; set; }

    public virtual Year? Year { get; set; }
}
