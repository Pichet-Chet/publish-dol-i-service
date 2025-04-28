using System;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface ICaseOfFixedRepo
	{
        Task<Response> Get(CaseOfFixedFilter param);

        Task<Response> Dropdown();

    }
}

