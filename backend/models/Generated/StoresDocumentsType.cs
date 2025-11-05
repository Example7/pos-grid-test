using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class StoresDocumentsType
{
    [Key]
    public string StoreDocumentTypeId { get; set; } = null!;

    public string StoreDocumentTypeName { get; set; } = null!;

    public string? StoreDocumentTypeDescription { get; set; }

    public string? StoreDocumentTypeCategoryId { get; set; }

    public string? StoreOrderTypeId { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();
}
