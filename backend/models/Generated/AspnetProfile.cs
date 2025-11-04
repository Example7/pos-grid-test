using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetProfile
{
    public Guid Userid { get; set; }

    public string Propertynames { get; set; } = null!;

    public string Propertyvaluesstring { get; set; } = null!;

    public byte[]? Propertyvaluesbinary { get; set; }

    public DateTime Lastupdateddate { get; set; }

    public virtual AspnetUser User { get; set; } = null!;
}
