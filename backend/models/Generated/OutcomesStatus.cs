using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OutcomesStatus
{
    [Key]
    public long OutcomeStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();
}
