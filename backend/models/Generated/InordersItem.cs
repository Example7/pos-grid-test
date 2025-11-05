using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class InordersItem
{
    [Key]
    public long InorderItemId { get; set; }

    public long InorderId { get; set; }

    public long? ProductId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Inorder Inorder { get; set; } = null!;

    public virtual Product? Product { get; set; }
}
