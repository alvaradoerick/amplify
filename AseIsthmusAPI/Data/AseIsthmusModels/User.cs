using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AseIsthmusAPI.Data.AseIsthmusModels;

public partial class User
{
    public string PersonId { get; set; } = null!;

    public string NumberId { get; set; } = null!;

    public string FirstName { get; set; } = null!;

    public string LastName1 { get; set; } = null!;

    public string? LastName2 { get; set; }

    public string Nationality { get; set; } = null!;

    public DateTime DateBirth { get; set; }

    public DateTime WorkStartDate { get; set; }

    public DateTime? EnrollmentDate { get; set; }

    public string PhoneNumber { get; set; } = null!;

    public string EmailAddress { get; set; } = null!;

    public string BankAccount { get; set; } = null!;

    public bool IsActive { get; set; }

    public int RoleId { get; set; }

    public string Address1 { get; set; } = null!;

    public string? Address2 { get; set; }

    public int Province { get; set; }

    public int Canton { get; set; }

    public int District { get; set; }

    public string PostalCode { get; set; } = null!;
    public DateTime? ApprovedDate { get; set; }

    [JsonIgnore]
    public virtual ICollection<Beneficiary> Beneficiaries { get; set; } = new List<Beneficiary>();

    [JsonIgnore]
    public virtual ICollection<ContributionBalance> ContributionBalances { get; set; } = new List<ContributionBalance>();

    [JsonIgnore]
    public virtual ICollection<LoanBalance> LoanBalances { get; set; } = new List<LoanBalance>();

    [JsonIgnore]
    public virtual ICollection<LoanRequest> LoanRequests { get; set; } = new List<LoanRequest>();

    [JsonIgnore]
    public virtual ICollection<Login> Logins { get; set; } = new List<Login>();

    [JsonIgnore]
    public virtual Role Role { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<SavingsBalance> SavingsBalances { get; set; } = new List<SavingsBalance>();

    [JsonIgnore]
    public virtual ICollection<SavingsRequest> SavingsRequests { get; set; } = new List<SavingsRequest>();

    [JsonIgnore]
    public virtual ICollection<TransactionLog> TransactionLogs { get; set; } = new List<TransactionLog>();
}
