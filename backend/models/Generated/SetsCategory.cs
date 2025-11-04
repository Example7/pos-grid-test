using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class SetsCategory
{
    public long SetCategorieId { get; set; }

    public string SetCategorieName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Set> Sets { get; set; } = new List<Set>();
}
