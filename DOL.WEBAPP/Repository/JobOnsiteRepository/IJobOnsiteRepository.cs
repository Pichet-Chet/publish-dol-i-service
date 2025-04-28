using System;
using DOL.WEBAPP.Models;
using DOL.WEBAPP.Models.Filter;
using DOL.WEBAPP.Models.Request;
using DOL.WEBAPP.Models.Response;

namespace DOL.WEBAPP.Repository.JobOnsiteRepository
{
	public interface IJobOnsiteRepository
	{
        Task<Response> CardJobs(SiteInformationFilter param);

        Task<Response> Detail(int id);

        Task<Response> Update(SiteInformationRequest param);

        Task<Response> UpdateImageName(UpdateImageNameRequest id);
    }
}

