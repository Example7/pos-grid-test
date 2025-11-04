using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class OrdersRealizationsTypesStatusesPath
{
    public long OrderRealizationTypeStatusPathId { get; set; }

    public long? OrderRealizationTypeId { get; set; }

    public long? OrderStatusId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual OrdersRealizationsType? OrderRealizationType { get; set; }
}
