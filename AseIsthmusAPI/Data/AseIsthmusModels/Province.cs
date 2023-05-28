﻿using System;
using System.Collections.Generic;

namespace AseIsthmusAPI.Data;

public partial class Province
{
    public int ProvinceId { get; set; }

    public string? ProvinceName { get; set; }

    public virtual ICollection<Canton> Cantons { get; set; } = new List<Canton>();
}
