using System;
using DOL.API.Models.Customs.Request;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using Microsoft.AspNetCore.Mvc;

namespace DOL.API.Repositories.Interface
{
	public interface ISiteInformationRepo
	{
        Task<Response> Schedule(SiteInformationFilter param);
        Task<Response> Overview(SiteInformationFilter param);
        Task<Response> CardJobs(SiteInformationFilter param);

        Task<Response> Get(SiteInformationFilter param);
        Task<Response> Detail(int id);
        Task<Response> Province();
        Task<Response> Location(string provinceName);

        Task<Response> Update(SiteInformationRequest param);
        Task<Response> UpdateImage(UpdateImageRequest param);
        Task<Response> UpdateImages(UpdateImageListRequest param);

        Task<Response> DeleteImage(DeleteImageRequest param);
        Task<Response> UpdateImageName(UpdateImageNameRequest param);

    }
}

