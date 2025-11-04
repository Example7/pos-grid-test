using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class Pose
{
    [Key]
    public long PosId { get; set; }

    public string PosName { get; set; } = null!;

    public long? PosTypeId { get; set; }

    public long? StoreId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();

    public virtual PosesType? PosType { get; set; }

    public virtual Store? Store { get; set; }
}
