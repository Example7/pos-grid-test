using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OrdersItem
{
    public long? ProductId { get; set; }

    public long? DiscountId { get; set; }

    /// <summary>
    /// ID stawki VAT produktu - wymagane, nie może być NULL
    /// </summary>
    public long OrderItemProductVatRateId { get; set; }

    public decimal OrderItemQuantity { get; set; }

    public decimal OrderItemPrice { get; set; }

    public DateTime OrderItemCreatedAt { get; set; }

    public long? SetItemProductId { get; set; }

    public Guid? OrderId { get; set; }

    /// <summary>
    /// ID kursu dla pozycji zamówienia
    /// </summary>
    public long? OrderItemCourseId { get; set; }

    /// <summary>
    /// Cena katalogowa brutto
    /// </summary>
    public decimal? OrderItemListGrossPrice { get; set; }

    /// <summary>
    /// Cena brutto pozycji
    /// </summary>
    public decimal? OrderItemGrossPrice { get; set; }

    /// <summary>
    /// Wartość brutto pozycji
    /// </summary>
    public decimal? OrderItemGrossValue { get; set; }

    /// <summary>
    /// ID rabatu produktu
    /// </summary>
    public long? ProductDiscountId { get; set; }

    /// <summary>
    /// Czy produkt ma rabat
    /// </summary>
    public bool? OrderItemIsProductDiscounted { get; set; }

    /// <summary>
    /// Procent rabatu produktu
    /// </summary>
    public decimal? OrderItemProductDiscountPercentRatio { get; set; }

    /// <summary>
    /// Procent rabatu pozycji zestawu
    /// </summary>
    public decimal? OrderItemSetItemDiscountPercentRatio { get; set; }

    /// <summary>
    /// Czy pozycja jest w zestawie
    /// </summary>
    public bool? OrderItemIsInSet { get; set; }

    /// <summary>
    /// Stawka VAT pozycji
    /// </summary>
    public decimal? OrderItemVatRateRatio { get; set; }

    /// <summary>
    /// Wartość netto pozycji
    /// </summary>
    public decimal? OrderItemNetValue { get; set; }

    /// <summary>
    /// Wartość VAT pozycji
    /// </summary>
    public decimal? OrderItemVatValue { get; set; }

    /// <summary>
    /// ID pracownika tworzącego pozycję
    /// </summary>
    public Guid? OrderItemCreatedByEmployeId { get; set; }

    [Key]
    public Guid OrderItemId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    public decimal OrderItemOriginQuantity { get; set; }

    public virtual Discount? Discount { get; set; }

    public virtual Order? Order { get; set; }

    public virtual Employee? OrderItemCreatedByEmploye { get; set; }

    public virtual ProductsVatRate OrderItemProductVatRate { get; set; } = null!;

    public virtual Product? Product { get; set; }

    public virtual SetsItemsProduct? SetItemProduct { get; set; }
}
