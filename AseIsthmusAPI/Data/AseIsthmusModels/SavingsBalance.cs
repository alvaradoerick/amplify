using System;
using System.Collections.Generic;
using AseIsthmusAPI.Data.AseIsthmusModels;

namespace AseIsthmusAPI.Data;

public partial class SavingsBalance
{
    public int SavingsBalanceId { get; set; }

    public int? SavingsRequestId { get; set; }

    public string PersonId { get; set; } = null!;

    public decimal LastAmountDeducted { get; set; }

    public DateTime LastDeductedDate { get; set; }

    public virtual User Person { get; set; } = null!;

    public virtual SavingsRequest? SavingsRequest { get; set; }
}
