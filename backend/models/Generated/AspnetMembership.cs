using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class AspnetMembership
{
    public Guid Applicationid { get; set; }

    public Guid Userid { get; set; }

    public string Password { get; set; } = null!;

    public int Passwordformat { get; set; }

    public string Passwordsalt { get; set; } = null!;

    public string? Mobilepin { get; set; }

    public string? Email { get; set; }

    public string? Loweredemail { get; set; }

    public string? Passwordquestion { get; set; }

    public string? Passwordanswer { get; set; }

    public bool Isapproved { get; set; }

    public bool Islockedout { get; set; }

    public DateTime Createdate { get; set; }

    public DateTime? Lastlogindate { get; set; }

    public DateTime? Lastpasswordchangeddate { get; set; }

    public DateTime? Lastlockoutdate { get; set; }

    public int Failedpasswordattemptcount { get; set; }

    public DateTime? Failedpasswordattemptwindowstart { get; set; }

    public int Failedpasswordanswerattemptcount { get; set; }

    public DateTime? Failedpasswordanswerattemptwindowstart { get; set; }

    public string? Comment { get; set; }

    public virtual AspnetApplication Application { get; set; } = null!;

    public virtual AspnetUser User { get; set; } = null!;
}
