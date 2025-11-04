using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class OrdersItemsOperationsLog
{
    public long OrderItemOperationLogId { get; set; }

    public long OrderItemId { get; set; }

    public Guid? OrderItemOperationEmployeeId { get; set; }

    public long? OldOrderItemStatusId { get; set; }

    public long? NewOrderItemStatusId { get; set; }

    public string OperationType { get; set; } = null!;

    public string? OperationDescription { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Employee? OrderItemOperationEmployee { get; set; }
}
