using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DevExpress.Models.Generated;

public partial class Contractor
{
    public string? ContractorLogo { get; set; }

    public string ContractorName { get; set; } = null!;

    public string? ContractorTaxid { get; set; }

    public string? ContractorCountryId { get; set; }

    public string? ContractorAddressPostalCode { get; set; }

    public string? ContractorAddressCiti { get; set; }

    public string? ContractorAddressStreet { get; set; }

    public string? ContractorPurchasesPhone { get; set; }

    public string? ContractorPurchasesEmail { get; set; }

    public string? ContractorPurchasesNotes { get; set; }

    public string? ContractorSalesPhone { get; set; }

    public string? ContractorSalesEmail { get; set; }

    public string? ContractorSalesNotes { get; set; }

    public bool ContractorIsSupplier { get; set; }

    public bool ContractorIsCustomer { get; set; }

    public DateTime CreatedAt { get; set; }

    /// <summary>
    /// UUID identifier for contractor
    /// </summary>
    /// 
    [Key]
    public Guid ContractorId { get; set; }

    public virtual ICollection<Income> Incomes { get; set; } = new List<Income>();

    public virtual ICollection<Outcome> Outcomes { get; set; } = new List<Outcome>();

    public virtual ICollection<OutcomesFinancialDocument> OutcomesFinancialDocuments { get; set; } = new List<OutcomesFinancialDocument>();

    public virtual ICollection<Product> Products { get; set; } = new List<Product>();
}
