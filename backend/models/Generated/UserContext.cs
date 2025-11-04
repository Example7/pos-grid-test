using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class UserContext
{
    public long Id { get; set; }

    public Guid? UserId { get; set; }

    public string? ContextData { get; set; }

    public DateTime CreatedAt { get; set; }
}
