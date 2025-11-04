using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class FinancialDocumentsType
{
    public long FinancialDocumentTypeId { get; set; }

    public string TypeCode { get; set; } = null!;

    public string TypeName { get; set; } = null!;

    public string? Description { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<OutcomesFinancialDocument> OutcomesFinancialDocuments { get; set; } = new List<OutcomesFinancialDocument>();
}
