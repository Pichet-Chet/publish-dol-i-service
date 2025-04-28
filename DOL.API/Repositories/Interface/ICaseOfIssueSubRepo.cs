using System;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface ICaseOfIssueSubRepo
	{
        Task<Response> Get(CaseOfIssueSubFilter param);

        Task<Response> Dropdown(int id);

        Task<Response> GetFixCase(int id);
    }
}

