using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;
using AseIsthmusAPI.Data.AseIsthmusModels;

namespace AseIsthmusAPI.Data;

public partial class District
{
    public int DistrictId { get; set; }

    public string DistrictName { get; set; } = null!;

    public int CantonId { get; set; }

    [JsonIgnore]
    public virtual Canton Canton { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
