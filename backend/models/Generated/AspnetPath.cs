using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetPath
{
    public Guid Applicationid { get; set; }

    public Guid Pathid { get; set; }

    public string Path { get; set; } = null!;

    public string Loweredpath { get; set; } = null!;

    public virtual AspnetApplication Application { get; set; } = null!;

    public virtual AspnetPersonalizationalluser? AspnetPersonalizationalluser { get; set; }

    public virtual ICollection<AspnetPersonalizationperuser> AspnetPersonalizationperusers { get; set; } = new List<AspnetPersonalizationperuser>();
}
