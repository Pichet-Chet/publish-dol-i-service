using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface IJobOnsiteRepo
	{
        Task<Response> Get(JobOnsiteFilter param);

        Task<Response> Detail(int id);

        Task<Response> Create(JobOnsite param);

        Task<Response> Update(JobOnsite param);
    }
}

