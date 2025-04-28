using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;

namespace DOL.API.Repositories
{
	public class CaseOfIssueSubRepo : ICaseOfIssueSubRepo
    {
        private readonly DolContext _chmContext;

        private readonly CaseOfIssueSubService service;

        public CaseOfIssueSubRepo()
        {
            _chmContext = new DolContext();

            service = new CaseOfIssueSubService(_chmContext);
        }

        public async Task<Response> Get(CaseOfIssueSubFilter param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Get(param));

            return resp;
        }

        public async Task<Response> Dropdown(int id)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Dropdown(id));

            return resp;
        }

        public async Task<Response> GetFixCase(int id)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.GetFixCase(id));

            return resp;
        }
    }
}

