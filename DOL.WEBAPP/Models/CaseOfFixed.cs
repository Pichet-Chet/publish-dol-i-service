using System;
using System.Collections.Generic;

namespace DOL.WEBAPP.Models;

public partial class CaseOfFixed
{
    public int Id { get; set; }

    public string NameTh { get; set; } = null!;

    public string NameEn { get; set; } = null!;

    public string CreateBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public bool IsActive { get; set; }
}
