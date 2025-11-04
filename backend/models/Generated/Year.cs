using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Year
{
    public long YearId { get; set; }

    public int YearNumber { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();
}
