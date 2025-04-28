using System;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class CaseOfFixedFilter : PaginationModel
	{
		public CaseOfFixedFilter()
		{
		}

        public int? Id { get; set; }

        public string? NameTh { get; set; } = null!;

        public string? NameEn { get; set; } = null!;

		public bool? IsActive { get; set; }
    }
}

