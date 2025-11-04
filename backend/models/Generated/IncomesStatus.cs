using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class IncomesStatus
{
    public long IncomeStatusId { get; set; }

    public string StatusName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();
}
