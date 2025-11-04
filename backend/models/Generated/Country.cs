using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Country
{
    public long CountryId { get; set; }

    public string CountryName { get; set; } = null!;

    public string? CountryCode { get; set; }

    public DateTime CreatedAt { get; set; }
}
