using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class ProductsBarcode
{
    public long ProductBarcodeId { get; set; }

    public long ProductId { get; set; }

    public string Barcode { get; set; } = null!;

    public DateTime CreatedAt { get; set; }

    public virtual Product Product { get; set; } = null!;
}
