using System;
using DOL.API.Models;
using DOL.API.Models.Customs.Request;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface IJobRepairRepo
	{
        Task<Response> Get(JobRepairFilter param);

        Task<Response> Detail(int id);

        Task<Response> Create(JobRepair param, IFormFile? jobImage1, IFormFile? jobImage2, IFormFile? jobImage3, IFormFile? jobImage4);

        Task<Response> Update(JobRepair param, IFormFile? jobImage1, IFormFile? jobImage2, IFormFile? jobImage3, IFormFile? jobImage4);

        Task<Response> UpdateTime(JobRepairUpdateTimeRequest param);

        Task<Response> ReportRepairAdmin(ExportJobRepairFilter param);

        Task<Response> ExportJobRepair(ExportJobRepairRequest param);

        Task<Response> ExportJobRepairMonth(ExportJobRepairMonthRequest param);
    }
}

