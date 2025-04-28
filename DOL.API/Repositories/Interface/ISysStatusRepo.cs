using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface ISysStatusRepo
    {
        Task<Response> Get(SysStatusFilter param);
        Task<Response> Detail(int id);
        Task<Response> Create(SysStatus param);
        Task<Response> Update(SysStatus param);
    }
}

