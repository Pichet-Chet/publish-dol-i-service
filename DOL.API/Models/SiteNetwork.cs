using System;
using System.Collections.Generic;

namespace DOL.API.Models;

public partial class SiteNetwork
{
    public int Id { get; set; }

    public string Name { get; set; } = null!;

    public bool JobWan1 { get; set; }

    public bool JobWan2 { get; set; }

    public bool JobInternet { get; set; }

    public bool JobCorpnet { get; set; }

    public bool JobCellular { get; set; }

    public bool JobDevice { get; set; }

    public DateTime CreateDate { get; set; }

    public string CreateBy { get; set; } = null!;

    public DateTime UpdateDate { get; set; }

    public string UpdateBy { get; set; } = null!;

    public bool IsActive { get; set; }
}
