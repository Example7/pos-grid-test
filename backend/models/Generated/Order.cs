using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Order
{
    public long? StoreId { get; set; }

    public long? PosId { get; set; }

    public Guid? EmployeeId { get; set; }

    public long? PaymentMethodId { get; set; }

    public long? StoreOrderTypeId { get; set; }

    public long? OrderRealizationTypeId { get; set; }

    public long? LoyaltyWalletId { get; set; }

    public decimal TotalAmount { get; set; }

    public DateTime CreatedAt { get; set; }

    public DateTime UpdatedAt { get; set; }

    public long? OrderPaymentStatusId { get; set; }

    public decimal DiscountAmount { get; set; }

    public string? Nip { get; set; }

    public string Source { get; set; } = null!;

    public Guid OrderId { get; set; }

    public string OrderNumber { get; set; } = null!;

    public long OrderRealizationStatusId { get; set; }

    public virtual Employee? Employee { get; set; }

    public virtual LoyaltiesWallet? LoyaltyWallet { get; set; }

    public virtual OrdersPaymentsStatus? OrderPaymentStatus { get; set; }

    public virtual OrdersRealizationsStatus OrderRealizationStatus { get; set; } = null!;

    public virtual OrdersRealizationsType? OrderRealizationType { get; set; }

    public virtual ICollection<OrdersInvoice> OrdersInvoices { get; set; } = new List<OrdersInvoice>();

    public virtual ICollection<OrdersItem> OrdersItems { get; set; } = new List<OrdersItem>();

    public virtual ICollection<OrdersOperationsLog> OrdersOperationsLogs { get; set; } = new List<OrdersOperationsLog>();

    public virtual ICollection<OrdersVatSummary> OrdersVatSummaries { get; set; } = new List<OrdersVatSummary>();

    public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();

    public virtual ICollection<OutcomesFinancialDocument> OutcomesFinancialDocuments { get; set; } = new List<OutcomesFinancialDocument>();

    public virtual PaymentsMethod? PaymentMethod { get; set; }

    public virtual Pose? Pos { get; set; }

    public virtual Store? Store { get; set; }

    public virtual StoresOrdersType? StoreOrderType { get; set; }
}
