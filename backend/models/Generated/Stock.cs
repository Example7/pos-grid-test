using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DevExpress.Models.Generated
{
    public partial class Stock
    {
        public long? YearId { get; set; }

        public long StoreId { get; set; }

        public long ProductId { get; set; }

        public decimal? StockIncomesQuantity { get; set; }

        public decimal? StockIncomesNotYetAllowedQuantity { get; set; }

        public decimal? StockOrdersQuantity { get; set; }

        public decimal? StockOutcomesQuantity { get; set; }

        public decimal? StockBlockedQuantity { get; set; }

        public decimal? StockMinQuantity { get; set; }

        public DateTime? CreatedAt { get; set; }

        public DateTime? UpdatedAt { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? StockTotalQuantity { get; set; }

        [Key]
        public Guid StockId { get; set; }

        [DatabaseGenerated(DatabaseGeneratedOption.Computed)]
        public decimal? StockAvailableQuantity { get; set; }

        public virtual Product Product { get; set; } = null!;

        public virtual Store Store { get; set; } = null!;

        public virtual Year? Year { get; set; }
    }
}
