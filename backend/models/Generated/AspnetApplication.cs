using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetApplication
{
    public string Applicationname { get; set; } = null!;

    public string Loweredapplicationname { get; set; } = null!;

    public Guid Applicationid { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<AspnetMembership> AspnetMemberships { get; set; } = new List<AspnetMembership>();

    public virtual ICollection<AspnetPath> AspnetPaths { get; set; } = new List<AspnetPath>();

    public virtual ICollection<AspnetRole> AspnetRoles { get; set; } = new List<AspnetRole>();

    public virtual ICollection<AspnetUser> AspnetUsers { get; set; } = new List<AspnetUser>();
}
