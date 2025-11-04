using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class StockHistory
{
    public long Id { get; set; }

    public long ProductId { get; set; }

    public Guid? EmployeeId { get; set; }

    public string? DocumentType { get; set; }

    public string? DocumentNumber { get; set; }

    public decimal QuantityChange { get; set; }

    public decimal QuantityAfter { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual Product Product { get; set; } = null!;
}
