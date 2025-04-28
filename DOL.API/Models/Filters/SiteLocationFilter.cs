using System;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class SiteLocationFilter : PaginationModel
    {
        public string? TextSearch { get; set; }

        public int? Id { get; set; }

        public string? ProvinceName { get; set; } = null!;

        public string? LocationName { get; set; } = null!;

        public bool? IsActive { get; set; }

        public string? SiteNetworkName { get; set; } = null!;

        public int? SiteNetworkSeq { get; set; }
    }
}

