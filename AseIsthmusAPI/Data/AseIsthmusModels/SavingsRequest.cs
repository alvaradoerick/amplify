using System;
using System.Collections.Generic;
using AseIsthmusAPI.Data.AseIsthmusModels;

namespace AseIsthmusAPI.Data;

public partial class SavingsRequest
{
    public int SavingsRequestId { get; set; }

    public string PersonId { get; set; } = null!;

    public int SavingsTypeId { get; set; }

    public DateTime ApplicationDate { get; set; }

    public decimal Amount { get; set; }

    public bool IsActive { get; set; }

    public int Term { get; set; }

    public DateTime ApprovedDate { get; set; }

    public bool IsApproved { get; set; }

    public virtual ICollection<LoanBalance> LoanBalances { get; set; } = new List<LoanBalance>();

    public virtual User Person { get; set; } = null!;

    public virtual ICollection<SavingsBalance> SavingsBalances { get; set; } = new List<SavingsBalance>();

    public virtual SavingsType SavingsType { get; set; } = null!;
}
