using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Product
{
    public long ProductId { get; set; }

    public string ProductName { get; set; } = null!;

    public string? ProductDescription { get; set; }

    public decimal ProductPrice { get; set; }

    public long? ProductCategory1Id { get; set; }

    public long? ProductCategory2Id { get; set; }

    public long? ProductQuantityUnitId { get; set; }

    public long? ProductVatRateId { get; set; }

    public bool IsSellable { get; set; }

    public bool IsVisible { get; set; }

    public bool IsComposite { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public decimal? MinStockLevel { get; set; }

    public decimal? OriginalPrice { get; set; }

    public decimal? DiscountedPrice { get; set; }

    public Guid? SupplierId { get; set; }

    public virtual ICollection<DiscountsProduct> DiscountsProducts { get; set; } = new List<DiscountsProduct>();

    public virtual ICollection<IncomesItem> IncomesItems { get; set; } = new List<IncomesItem>();

    public virtual ICollection<InordersItem> InordersItems { get; set; } = new List<InordersItem>();

    public virtual ICollection<OrdersItem> OrdersItems { get; set; } = new List<OrdersItem>();

    public virtual ICollection<OutcomesFinancialDocumentsItem> OutcomesFinancialDocumentsItems { get; set; } = new List<OutcomesFinancialDocumentsItem>();

    public virtual ICollection<OutcomesItem> OutcomesItems { get; set; } = new List<OutcomesItem>();

    public virtual ProductsCategories1? ProductCategory1 { get; set; }

    public virtual ProductsCategories2? ProductCategory2 { get; set; }

    public virtual ProductsQuantityUnit? ProductQuantityUnit { get; set; }

    public virtual ICollection<ProductSetsItem> ProductSetsItems { get; set; } = new List<ProductSetsItem>();

    public virtual ProductsVatRate? ProductVatRate { get; set; }

    public virtual ICollection<ProductsBarcode> ProductsBarcodes { get; set; } = new List<ProductsBarcode>();

    public virtual ICollection<ProductsRecipe> ProductsRecipes { get; set; } = new List<ProductsRecipe>();

    public virtual ICollection<ProductsRecipesItem> ProductsRecipesItemProductRecipeItemProducts { get; set; } = new List<ProductsRecipesItem>();

    public virtual ICollection<ProductsRecipesItem> ProductsRecipesItemProducts { get; set; } = new List<ProductsRecipesItem>();

    public virtual ICollection<SetsItemsProduct> SetsItemsProducts { get; set; } = new List<SetsItemsProduct>();

    public virtual ICollection<StockHistory> StockHistories { get; set; } = new List<StockHistory>();

    public virtual ICollection<Stock> Stocks { get; set; } = new List<Stock>();

    public virtual Contractor? Supplier { get; set; }
}
