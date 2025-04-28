using System;
namespace DOL.API.Models.Customs.Response
{
	public class ExportJobRepairResponse
	{
		public ExportJobRepairResponse()
		{
            Seq = 0;
            DocumentNo = string.Empty;
            ProvinceName = string.Empty;
            SiteNetworkName = string.Empty;
            CircuitNo = string.Empty;
            Speed = 0;
            CircuitType = string.Empty;
            IssueCase = string.Empty;
            JobRequestDate = string.Empty;
            JobAcceptDate = string.Empty;
            JobOnProcessDate = string.Empty;
            JobFinishDate = string.Empty;
            ResponseTime = string.Empty;
            OnProcessTime = string.Empty;
            AllTimeProcessString = string.Empty;
            AllTimeProcess = new TimeSpan();
            StaffName = string.Empty;
            Remark = string.Empty;
            ProviderName = string.Empty;
        }

        public int? Seq { get; set; }
        public string? DocumentNo { get; set; }
        public string? ProvinceName { get; set; }
        public string? SiteNetworkName { get; set; }
        public string? CircuitNo { get; set; }
        public int? Speed { get; set; }
        public string? CircuitType { get; set; }
        public string? IssueCase { get; set; }

        public string? JobRequestDate { get; set; }
        public string? JobAcceptDate { get; set; }
        public string? JobOnProcessDate { get; set; }
        public string? JobFinishDate { get; set; }

        public string? ResponseTime { get; set; }
        public string? OnProcessTime { get; set; }
        public string? AllTimeProcessString { get; set; }
        public TimeSpan? AllTimeProcess { get; set; }


        public string? StaffName { get; set; }
        public string? Remark { get; set; }
        public string? ProviderName { get; set; }

    }
}

