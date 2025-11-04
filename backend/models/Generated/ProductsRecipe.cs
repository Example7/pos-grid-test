using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class ProductsRecipe
{
    public long ProductRecipeId { get; set; }

    public long ProductId { get; set; }

    public string? RecipeName { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;

    public virtual ICollection<ProductsRecipesItem> ProductsRecipesItems { get; set; } = new List<ProductsRecipesItem>();
}
