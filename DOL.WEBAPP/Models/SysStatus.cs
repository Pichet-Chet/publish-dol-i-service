using System;
using System.Collections.Generic;

namespace DOL.WEBAPP.Models;

public partial class SysStatus
{
    public int Id { get; set; }

    public string NameTh { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public bool IsActive { get; set; }

    public string Category { get; set; } = null!;
}
