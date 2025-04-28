using System;
using System.Collections.Generic;

namespace DOL.WEBAPP.Models;

public partial class SiteLocation
{
    public int Id { get; set; }

    public string ProviceName { get; set; } = null!;

    public string LocationName { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public bool IsActive { get; set; }
}
