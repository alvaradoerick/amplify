using System;
using System.Collections.Generic;

namespace AseIsthmusAPI.Data.AseIsthmusModels;

public partial class CateryAgreement
{
    public int CateryAgreementId { get; set; }

    public string Description { get; set; } = null!;

    public bool IsActive { get; set; }

    public virtual ICollection<Agreement> Agreements { get; set; } = new List<Agreement>();
}
