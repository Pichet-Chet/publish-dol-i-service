using System;
using System.Collections.Generic;

namespace DOL.WEBAPP.Models;

public partial class CaseOfIssueSub
{
    public int Id { get; set; }

    public int CaseOfIssueId { get; set; }

    public string Name { get; set; } = null!;

    public string CaseFix { get; set; } = null!;

    public string CreateBy { get; set; } = null!;

    public DateTime CreateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public bool IsActive { get; set; }
}
