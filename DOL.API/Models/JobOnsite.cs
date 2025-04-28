using System;
using System.Collections.Generic;

namespace DOL.API.Models;

public partial class JobOnsite
{
    public int Id { get; set; }

    public string? DocumentNo { get; set; }

    public int SiteInformationId { get; set; }

    public int SysUserId { get; set; }

    public DateTime AssignDate { get; set; }

    public string TypeOnsiteValue { get; set; } = null!;

    public string? TeamInstallContactName { get; set; }

    public string? TeamInstallContactTel { get; set; }

    public string? TeamInstallComment { get; set; }

    public DateTime? TeamInstallDate { get; set; }

    public string? AcceptSign { get; set; }

    public string? AcceptBy { get; set; }

    public string? AcceptPosition { get; set; }

    public DateTime? AcceptDate { get; set; }

    public int SysStatusId { get; set; }
}
