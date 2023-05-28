using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AseIsthmusAPI.Data;

public partial class Agreement
{
    public int AgreementId { get; set; }

    public string Title { get; set; } = null!;

    public string Description { get; set; } = null!;

    public byte[] Image { get; set; } = null!;

    public int CategoryAgreementId { get; set; }

    public bool IsActive { get; set; }

    public string PersonId { get; set; } = null!;

    [JsonIgnore]
    public virtual CategoryAgreement CategoryAgreement { get; set; } = null!;

    [JsonIgnore]
    public virtual User Person { get; set; } = null!;
}
