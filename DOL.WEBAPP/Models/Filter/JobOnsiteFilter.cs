using System;
using DOL.WEBAPP.Models.Pagination;

namespace DOL.WEBAPP.Models.Filter
{
	public class JobOnsiteFilter : PaginationModel
    {
		public JobOnsiteFilter()
		{
		}

        public string? TextSearch { get; set; }

        public int? Id { get; set; }

        public string? DocumentNo { get; set; }

        public int? SiteInformationId { get; set; }

        public int? SysUserId { get; set; }

        public int? SysStatusId { get; set; }

        public string? TypeOnsiteValue { get; set; }
    }
}

