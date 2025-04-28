using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;

namespace DOL.API.Repositories
{
	public class SysStatusRepo : ISysStatusRepo
    {
        private readonly DolContext _chmContext;

        private readonly SysStatusService service;

        public SysStatusRepo()
        {
            _chmContext = new DolContext();

            service = new SysStatusService(_chmContext);
        }

        public async Task<Response> Get(SysStatusFilter param)
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

        public async Task<Response> Create(SysStatus param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Create(param));

            return resp;
        }


        public async Task<Response> Update(SysStatus param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Update(param));

            return resp;
        }
    }
}

