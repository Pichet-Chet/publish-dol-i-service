using System;
using DOL.API.Models;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface IMigrateRepo
    {
        Task<Response> SiteInformation(IFormFile? fileUpload);

    }
}

