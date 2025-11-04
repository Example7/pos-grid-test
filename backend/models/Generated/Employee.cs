using System;
using System.Collections.Generic;

namespace DevExpress.Models.Generated;

public partial class Employee
{
    public Guid EmployeeId { get; set; }

    public Guid? SupervisorEmployeeId { get; set; }

    public long? EmployeePositionId { get; set; }

    public string? EmployeeLogo { get; set; }

    public string EmployeeName { get; set; } = null!;

    public string? EmployeeEmail { get; set; }

    public string? EmployeePhone { get; set; }

    public DateTime CreatedAt { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Employee> InverseSupervisorEmployee { get; set; } = new List<Employee>();

    public virtual ICollection<Order> Orders { get; set; } = new List<Order>();

    public virtual ICollection<OrdersItem> OrdersItems { get; set; } = new List<OrdersItem>();

    public virtual ICollection<OrdersItemsOperationsLog> OrdersItemsOperationsLogs { get; set; } = new List<OrdersItemsOperationsLog>();

    public virtual ICollection<OrdersOperationsLog> OrdersOperationsLogs { get; set; } = new List<OrdersOperationsLog>();

    public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();

    public virtual ICollection<OutcomesFinancialDocumentsItem> OutcomesFinancialDocumentsItems { get; set; } = new List<OutcomesFinancialDocumentsItem>();

    public virtual ICollection<Shop> Shops { get; set; } = new List<Shop>();

    public virtual ICollection<StockHistory> StockHistories { get; set; } = new List<StockHistory>();

    public virtual Employee? SupervisorEmployee { get; set; }
}
