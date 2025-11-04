using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class OrdersOperationsLog
{
    public long OrderOperationLogId { get; set; }

    public Guid? OrderOperationEmployeeId { get; set; }

    public string OperationType { get; set; } = null!;

    public string? OperationDescription { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid? OrderId { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Employee? OrderOperationEmployee { get; set; }
}
