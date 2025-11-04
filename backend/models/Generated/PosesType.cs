using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class PosesType
{
    [Key]
    public long PosTypeId { get; set; }

    public string PosTypeName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Pose> Poses { get; set; } = new List<Pose>();
}
