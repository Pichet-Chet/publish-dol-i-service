using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;

namespace DOL.API.Repositories
{
	public class SysUserRepo : ISysUserRepo
    {
        private readonly DolContext _chmContext;

        private readonly SysUserService service;

        public SysUserRepo()
        {
            _chmContext = new DolContext();

            service = new SysUserService(_chmContext);
        }

        public async Task<Response> Get(SysUserFilter param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Get(param));

            return resp;
        }

        public async Task<Response> Detail(int id)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Detail(id));

            return resp;
        }

        public async Task<Response> Create(SysUser param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Create(param));

            return resp;
        }


        public async Task<Response> Update(SysUser param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Update(param));

            return resp;
        }
    }
}

