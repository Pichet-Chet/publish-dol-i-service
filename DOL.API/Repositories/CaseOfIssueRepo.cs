using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;

namespace DOL.API.Repositories
{
    public class CaseOfIssueRepo : ICaseOfIssueRepo
    {
        private readonly DolContext _chmContext;

        private readonly CaseOfIssueService service;

        public CaseOfIssueRepo()
        {
            _chmContext = new DolContext();

            service = new CaseOfIssueService(_chmContext);
        }

        public async Task<Response> Get(CaseOfIssueFilter param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Get(param));

            return resp;
        }

        public async Task<Response> Dropdown()
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Dropdown());

            return resp;
        }
    }
}

