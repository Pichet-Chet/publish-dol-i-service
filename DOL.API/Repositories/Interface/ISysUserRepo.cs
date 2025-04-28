using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface ISysUserRepo
	{
        Task<Response> Get(SysUserFilter param);
        Task<Response> Detail(int id);
        Task<Response> Create(SysUser param);
        Task<Response> Update(SysUser param);
    }
}

