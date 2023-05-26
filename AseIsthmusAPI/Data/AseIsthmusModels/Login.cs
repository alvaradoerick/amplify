using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AseIsthmusAPI.Data.AseIsthmusModels;

public partial class Login
{
    public int LoginId { get; set; }

    public string PersonId { get; set; } = null!;

    public string? Pw { get; set; }

    [JsonIgnore]
    public virtual User Person { get; set; } = null!;
}
