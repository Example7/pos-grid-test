using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Income
{
    public string IncomeNumber { get; set; } = null!;

    public long? StoreId { get; set; }

    public long? PosId { get; set; }

    public long IncomeStatusId { get; set; }

    public Guid? IncomeCreatedByEmployeeId { get; set; }

    public long? YearId { get; set; }

    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// Type of income document (PZ, MM+, RI+)
    /// </summary>
    public string? StoreDocumentTypeId { get; set; }

    public Guid ContractorId { get; set; }

    public Guid IncomeId { get; set; }

    public decimal? TotalCostValue { get; set; }

    public decimal? TotalGrossValue { get; set; }

    /// <summary>
    /// Suma wartości netto wszystkich pozycji - wyliczane jako SUM(incomes_items.net_value)
    /// </summary>
    public decimal? TotalNetValue { get; set; }

    public virtual Contractor Contractor { get; set; } = null!;

    public virtual Employee? IncomeCreatedByEmployee { get; set; }

    public virtual IncomesStatus IncomeStatus { get; set; } = null!;

    public virtual ICollection<IncomesItem> IncomesItems { get; set; } = new List<IncomesItem>();

    public virtual Pose? Pos { get; set; }

    public virtual Store? Store { get; set; }

    public virtual StoresDocumentsType? StoreDocumentType { get; set; }

    public virtual Year? Year { get; set; }
}
