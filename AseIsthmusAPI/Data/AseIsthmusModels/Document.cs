using System;
using System.Collections.Generic;

namespace AseIsthmusAPI.Data;

public partial class Document
{
    public int DocumentId { get; set; }

    public string DocumentTypeLink { get; set; } = null!;

    public string? Link { get; set; }
}
