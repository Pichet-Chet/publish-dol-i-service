using System;
using System.Collections.Generic;

namespace DOL.WEBAPP.Models;

public partial class JobRepair
{
    public int Id { get; set; }

    public string? DocumentNo { get; set; }

    public int? SiteNetworkId { get; set; }

    public int? SiteInformationId { get; set; }

    public string? JobDescription { get; set; }

    public string? JobContactName { get; set; }

    public string? JobContactTel { get; set; }

    public string? JobSenderContactName { get; set; }

    public string? JobSenderContactTel { get; set; }

    public string? JobSenderRemark { get; set; }

    public string? JobFixedDescription { get; set; }

    public string? JobFixedComment { get; set; }

    public string? JobFixedContactName { get; set; }

    public string? JobFixedContactTel { get; set; }

    public string? JobImage1 { get; set; }

    public string? JobImage2 { get; set; }

    public string? JobImage3 { get; set; }

    public string? JobImage4 { get; set; }

    public int? SysStatusId { get; set; }

    public string? JobCreatedBy { get; set; }

    public DateTime? JobCreatedDate { get; set; }

    public string? JobAcceptBy { get; set; }

    public DateTime? JobAcceptDate { get; set; }

    public string? JobProcessBy { get; set; }

    public DateTime? JobProcessDate { get; set; }

    public string? JobCompleteBy { get; set; }

    public DateTime? JobCompleteDate { get; set; }

    public string? TypeRepairData { get; set; }

    public string? TypeRepairValue { get; set; }

    public int? CaseOfIssueId { get; set; }

    public int? CaseOfFixId { get; set; }

    public string? DocumentRequest { get; set; }
}
