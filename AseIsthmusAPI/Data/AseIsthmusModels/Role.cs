using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace AseIsthmusAPI.Data;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleDescription { get; set; } = null!;

    [JsonIgnore]
    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
