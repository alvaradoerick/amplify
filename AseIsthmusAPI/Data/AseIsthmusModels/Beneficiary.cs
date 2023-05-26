using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AseIsthmusAPI.Data.AseIsthmusModels;

public partial class Beneficiary
{
    public int BeneficiaryId { get; set; }

    public string BeneficiaryName { get; set; } = null!;

    public string BeneficiaryNumberId { get; set; } = null!;

    public string BeneficiaryRelation { get; set; } = null!;

    public decimal BeneficiaryPercentage { get; set; }

    public string PersonId { get; set; } = null!;

    [JsonIgnore]
    public virtual User Person { get; set; } = null!;
}
