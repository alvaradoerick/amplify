using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AseIsthmusAPI.Data;

public partial class CategoryAgreement
{
    public int CategoryAgreementId { get; set; }

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    [JsonIgnore]
    public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();
}
