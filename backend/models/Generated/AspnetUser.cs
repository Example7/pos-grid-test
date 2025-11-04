using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetUser
{
    public Guid Applicationid { get; set; }

    public Guid Userid { get; set; }

    public Guid? EmplyeeId { get; set; }

    public string? Userlogo { get; set; }

    public string Username { get; set; } = null!;

    public string Loweredusername { get; set; } = null!;

    public string? Mobilealias { get; set; }

    public bool Isanonymous { get; set; }

    public DateTime Lastactivitydate { get; set; }

    public virtual AspnetApplication Application { get; set; } = null!;

    public virtual ICollection<AspnetMembership> AspnetMemberships { get; set; } = new List<AspnetMembership>();

    public virtual ICollection<AspnetPersonalizationperuser> AspnetPersonalizationperusers { get; set; } = new List<AspnetPersonalizationperuser>();

    public virtual AspnetProfile? AspnetProfile { get; set; }

    public virtual ICollection<AspnetRole> Roles { get; set; } = new List<AspnetRole>();
}
