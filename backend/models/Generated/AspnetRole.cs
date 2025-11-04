using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetRole
{
    public Guid Applicationid { get; set; }

    public Guid Roleid { get; set; }

    public string Rolename { get; set; } = null!;

    public string Loweredrolename { get; set; } = null!;

    public string? Description { get; set; }

    public virtual AspnetApplication Application { get; set; } = null!;

    public virtual ICollection<AspnetUser> Users { get; set; } = new List<AspnetUser>();
}
