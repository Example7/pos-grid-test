using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OrdersRealizationsType
{
    [Key]
    public long OrderRealizationTypeId { get; set; }

    public string RealizationTypeName { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<OrdersRealizationsTypesStatusesPath> OrdersRealizationsTypesStatusesPaths { get; set; } = new List<OrdersRealizationsTypesStatusesPath>();
}
