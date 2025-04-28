using System;
using DOL.API.Models;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;

namespace DOL.API.Repositories
{
	public class MigrateRepo : IMigrateRepo
    {
        private readonly DolContext _chmContext;

        private readonly MigrateDataService service;

        public MigrateRepo()
        {
            _chmContext = new DolContext();

            service = new MigrateDataService(_chmContext);
        }

        public async Task<Response> SiteInformation(IFormFile? fileupload)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.SiteInformation(fileupload));

            return resp;
        }
    }
}

