using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AseIsthmusAPI.Data;

public partial class Canton
{
    public int CantonId { get; set; }

    public string CantonName { get; set; } = null!;

    public int ProvinceId { get; set; }

    [JsonIgnore]
    public virtual ICollection<District> Districts { get; set; } = new List<District>();

    [JsonIgnore]
    public virtual Province Province { get; set; } = null!;
}
