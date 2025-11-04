using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OutcomesFinancialDocumentsStatus
{
    [Key]
    public long OutcomeFinancialDocumentStatusId { get; set; }

    public string OutcomeFinancialDocumentStatusName { get; set; } = null!;

    public virtual ICollection<OutcomesFinancialDocument> OutcomesFinancialDocuments { get; set; } = new List<OutcomesFinancialDocument>();
}
