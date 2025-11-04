using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Set
{
    public long SetId { get; set; }

    public long? SetCategorieId { get; set; }

    public string SetName { get; set; } = null!;

    public DateTime SetActiveFrom { get; set; }

    public DateTime SetActiveTo { get; set; }

    public bool? SetIsDisplayed { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual SetsCategory? SetCategorie { get; set; }

    public virtual ICollection<SetsItem> SetsItems { get; set; } = new List<SetsItem>();

    public virtual ICollection<SetsSchedule> SetsSchedules { get; set; } = new List<SetsSchedule>();

    public virtual ICollection<ShopsSet> ShopsSets { get; set; } = new List<ShopsSet>();
}
