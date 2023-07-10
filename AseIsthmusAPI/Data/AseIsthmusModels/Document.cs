using System;
using System.Collections.Generic;

namespace AseIsthmusAPI.Data;

public partial class Document
{
    public int DocumentId { get; set; }

    public string GoogleDriveLink { get; set; } = null!;
}
