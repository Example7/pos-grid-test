using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetPersonalizationperuser
{
    public Guid Id { get; set; }

    public Guid Pathid { get; set; }

    public Guid Userid { get; set; }

    public byte[] Pagesettings { get; set; } = null!;

    public DateTime Lastupdateddate { get; set; }

    public virtual AspnetPath Path { get; set; } = null!;

    public virtual AspnetUser User { get; set; } = null!;
}
