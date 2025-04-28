using System;
using DOL.WEBAPP.Models.Filter;
using DOL.WEBAPP.Models.Response;

namespace DOL.WEBAPP.Repository.JobRepairRepository
{
	public interface IJobRepairRepository
	{
        Task<Response> Get(JobRepairFilter param);
    }
}

