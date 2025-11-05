using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class DiscountsProduct
{
    [Key]
    public long DiscountProductId { get; set; }

    public long DiscountId { get; set; }

    public long ProductId { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Discount Discount { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;
}
