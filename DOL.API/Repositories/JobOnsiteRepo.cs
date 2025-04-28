using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;

namespace DOL.API.Repositories
{
	public class JobOnsiteRepo : IJobOnsiteRepo
    {
        private readonly DolContext _chmContext;

        private readonly JobOnsiteService service;

        public JobOnsiteRepo()
        {
            _chmContext = new DolContext();

            service = new JobOnsiteService(_chmContext);
        }

        public async Task<Response> Get(JobOnsiteFilter param)
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

        public async Task<Response> Create(JobOnsite param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Create(param));

            return resp;
        }


        public async Task<Response> Update(JobOnsite param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Update(param));

            return resp;
        }
    }
}

