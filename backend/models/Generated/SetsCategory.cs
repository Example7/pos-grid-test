using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class SetsCategory
{
    [Key]
    public long SetCategorieId { get; set; }

    public string SetCategorieName { get; set; } = null!;

    public DateTime? CreatedAt { get; set; }

    public virtual ICollection<Set> Sets { get; set; } = new List<Set>();
}
