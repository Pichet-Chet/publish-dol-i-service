using System;
using DOL.API.Models.Pagination;

namespace DOL.API.Models.Filters
{
	public class CaseOfIssueSubFilter : PaginationModel
	{
		public CaseOfIssueSubFilter()
		{
		}

        public int? Id { get; set; }

        public string? Name { get; set; } = null!;

        public bool? IsActive { get; set; }
    }
}

