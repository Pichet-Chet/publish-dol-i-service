using System;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface ICaseOfIssueRepo
	{
        Task<Response> Get(CaseOfIssueFilter param);

        Task<Response> Dropdown();
    }
}

