using System;
using DOL.WEBAPP.Models.Pagination;

namespace DOL.WEBAPP.Models.Filter
{
	public class SiteInformationFilter : PaginationModel
	{
		public SiteInformationFilter()
		{

		}

        public string? TextSearch { get; set; }

        public int? Id { get; set; }

        public int? SiteNetworkId { get; set; }

        public string? SiteNetworkName { get; set; } = null!;

        public int? SiteNetworkSeq { get; set; }

        public string? ProviceName { get; set; } = null!;

        public string? LocationName { get; set; } = null!;

        public string? Address { get; set; } = null!;

        public string? StaffOrganize { get; set; } = null!;

        public string? TelephoneNumber { get; set; } = null!;

        public int? SysStatusId { get; set; }

        public int? SysUserId { get; set; }
    }
}

