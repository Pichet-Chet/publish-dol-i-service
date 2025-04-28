using System;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class CaseOfIssueFilter : PaginationModel
	{
		public CaseOfIssueFilter()
		{
		}

        public int? Id { get; set; }

        public string? NameTh { get; set; } = null!;

        public string? NameEn { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}

