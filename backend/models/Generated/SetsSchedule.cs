using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class SetsSchedule
{
    public long SetScheduleId { get; set; }

    public long SetId { get; set; }

    public string? SetScheduleDaysOfWeek { get; set; }

    public TimeOnly? SetScheduleHoursFrom { get; set; }

    public TimeOnly? SetScheduleHoursTo { get; set; }

    public DateTime? CreatedAt { get; set; }

    public virtual Set Set { get; set; } = null!;
}
