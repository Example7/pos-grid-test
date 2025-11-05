using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class ProductsRecipesItem
{
    [Key]
    public long ProductRecipeItemId { get; set; }

    public long ProductRecipeId { get; set; }

    public long ProductId { get; set; }

    public long ProductRecipeItemProductId { get; set; }

    public decimal Quantity { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ProductsRecipe ProductRecipe { get; set; } = null!;

    public virtual Product ProductRecipeItemProduct { get; set; } = null!;
}
