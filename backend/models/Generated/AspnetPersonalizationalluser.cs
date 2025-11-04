using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetPersonalizationalluser
{
    public Guid Pathid { get; set; }

    public byte[] Pagesettings { get; set; } = null!;

    public DateTime Lastupdateddate { get; set; }

    public virtual AspnetPath Path { get; set; } = null!;
}
