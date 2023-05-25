using System;
using System.Collections.Generic;

namespace AseIsthmusAPI.Data;

public partial class Role
{
    public int RoleId { get; set; }

    public string RoleDescription { get; set; } = null!;

    public virtual ICollection<User> Users { get; set; } = new List<User>();
}
