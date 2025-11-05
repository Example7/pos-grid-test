using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class StoresDocumentsTypesCategory
{
    [Key]
    public string StoreDocumentTypeCategoryId { get; set; } = null!;

    public string CategoryName { get; set; } = null!;
}
