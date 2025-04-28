using System;
namespace DOL.WEBAPP.Models.Filter
{
	public class JobRepairFilter
	{
		public JobRepairFilter()
		{

		}

        public string? TextSearch { get; set; }

        public int? NetworkId { get; set; }

        public string? ProvinceName { get; set; }

        public string? LocationName { get; set; }

        public int? StatusId { get; set; }

        public string? JobType { get; set; }

        public bool? OutOfSla { get; set; }

        public DateTime? JobCreateDate { get; set; }

        public DateTime? JobCreateDateFrom { get; set; }

        public DateTime? JobCreateDateTo { get; set; }

        public string? DocumentRequet { get; set; }
    }
}

