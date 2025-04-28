using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;

namespace DOL.API.Repositories
{
	public class SiteNetworkRepo : ISiteNetworkRepo
    {
        private readonly DolContext _chmContext;

        private readonly SiteNetworkService service;

        public SiteNetworkRepo()
        {
            _chmContext = new DolContext();

            service = new SiteNetworkService(_chmContext);
        }

        public async Task<Response> Get(SiteNetworkFilter param)
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

        public async Task<Response> WorkActive(int id)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.WorkActive(id));

            return resp;
        }

        public async Task<Response> repairActive(int siteId)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.repairActive(siteId));

            return resp;
        }

        public async Task<Response> Create(SiteNetwork param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Create(param));

            return resp;
        }


        public async Task<Response> Update(SiteNetwork param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Update(param));

            return resp;
        }
    }
}

