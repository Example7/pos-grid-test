using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class IncomesItem
{
    public Guid IncomeItemId { get; set; }

    public long ProductId { get; set; }

    public long ProductVatRateId { get; set; }

    public decimal Quantity { get; set; }

    /// <summary>
    /// Cena kosztowa jednostkowa - cena zakupu/dostawy towaru, szczególnie ważna dla dokumentów MM+
    /// </summary>
    public decimal CostPrice { get; set; }

    public DateTime CreatedAt { get; set; }

    public Guid IncomeId { get; set; }

    public decimal? VatRateValue { get; set; }

    public decimal? NetPrice { get; set; }

    public decimal? GrossPrice { get; set; }

    public decimal? GrossValue { get; set; }

    public decimal? NetValue { get; set; }

    /// <summary>
    /// Wartość kosztowa pozycji - automatycznie obliczana jako ROUND(cost_price * quantity, 2)
    /// </summary>
    public decimal? CostValue { get; set; }

    public virtual Income Income { get; set; } = null!;

    public virtual Product Product { get; set; } = null!;

    public virtual ProductsVatRate ProductVatRate { get; set; } = null!;
}
