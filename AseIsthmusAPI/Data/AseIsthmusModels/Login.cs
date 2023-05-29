using Newtonsoft.Json;
using System;
using System.Collections.Generic;

namespace AseIsthmusAPI.Data;

public partial class Login
{
    public int LoginId { get; set; }

    public string PersonId { get; set; } = null!;

    public string? Pw { get; set; }

    [JsonIgnore]
    public virtual User Person { get; set; } = null!;
}
