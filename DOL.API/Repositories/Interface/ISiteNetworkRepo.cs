using System;
using DOL.API.Models;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface ISiteNetworkRepo
	{
        Task<Response> Get(SiteNetworkFilter param);
        Task<Response> Detail(int id);
        Task<Response> Create(SiteNetwork param);
        Task<Response> Update(SiteNetwork param);
        Task<Response> WorkActive(int id);

        Task<Response> repairActive(int siteId);
    }
}

