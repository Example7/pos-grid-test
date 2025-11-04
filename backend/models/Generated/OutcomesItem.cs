using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class OutcomesItem
{
    [Key]
    public long? ProductId { get; set; }

    public decimal Quantity { get; set; }

    /// <summary>
    /// Cena kosztowa jednostkowa - cena zakupu/dostawy towaru, szczególnie ważna dla dokumentów MM+
    /// </summary>
    public decimal CostPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid OutcomeItemId { get; set; }

    public Guid? OutcomeId { get; set; }

    public long? ProductVatRateId { get; set; }

    public DateTime? UpdatedAt { get; set; }

    /// <summary>
    /// Cena netto sprzedaży jednostkowa - używana przy sprzedaży
    /// </summary>
    public decimal? NetPrice { get; set; }

    /// <summary>
    /// Wartość kosztowa pozycji - automatycznie obliczana jako ROUND(cost_price * quantity, 2)
    /// </summary>
    public decimal? CostValue { get; set; }

    /// <summary>
    /// Cena brutto jednostkowa - cena sprzedaży brutto
    /// </summary>
    public decimal? GrossPrice { get; set; }

    /// <summary>
    /// Wartość netto pozycji - automatycznie obliczana jako ROUND(quantity * net_price, 2)
    /// </summary>
    public decimal? NetValue { get; set; }

    /// <summary>
    /// Wartość brutto pozycji - automatycznie obliczana jako ROUND(quantity * gross_price, 2)
    /// </summary>
    public decimal? GrossValue { get; set; }

    public virtual Outcome? Outcome { get; set; }

    public virtual Product? Product { get; set; }

    public virtual ProductsVatRate? ProductVatRate { get; set; }
}
