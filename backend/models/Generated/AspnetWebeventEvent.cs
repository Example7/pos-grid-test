using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetWebeventEvent
{
    public Guid Eventid { get; set; }

    public DateTime Eventtimeutc { get; set; }

    public DateTime Eventtime { get; set; }

    public string Eventtype { get; set; } = null!;

    public long Eventsequence { get; set; }

    public long Eventoccurrence { get; set; }

    public int Eventcode { get; set; }

    public int Eventdetailcode { get; set; }

    public string? Message { get; set; }

    public string? Applicationpath { get; set; }

    public string? Applicationvirtualpath { get; set; }

    public string Machinename { get; set; } = null!;

    public string? Requesturl { get; set; }

    public string? Exceptiontype { get; set; }

    public string? Details { get; set; }
}
