using System;
using System.Collections.Generic;

namespace AseIsthmusAPI.Data;

public partial class ContributionUsage
{
    public int ContributionUsageId { get; set; }

    public string? Description { get; set; }

    public virtual ICollection<LoansType> LoansTypes { get; set; } = new List<LoansType>();
}
