using System;
using DOL.API.Models;
using DOL.API.Models.Customs.Request;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;

namespace DOL.API.Repositories
{
	public class JobRepairRepo : IJobRepairRepo
    {
        private readonly DolContext _chmContext;

        private readonly JobRepairService service;

        public JobRepairRepo()
        {
            _chmContext = new DolContext();

            service = new JobRepairService(_chmContext);
        }

        public async Task<Response> Get(JobRepairFilter param)
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

        public async Task<Response> Create(JobRepair param, IFormFile? jobImage1, IFormFile? jobImage2, IFormFile? jobImage3, IFormFile? jobImage4)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Create(param, jobImage1, jobImage2, jobImage3, jobImage4));

            return resp;
        }

        public async Task<Response> Update(JobRepair param, IFormFile? jobImage1, IFormFile? jobImage2, IFormFile? jobImage3, IFormFile? jobImage4)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Update(param, jobImage1, jobImage2, jobImage3, jobImage4));

            return resp;
        }

        public async Task<Response> UpdateTime(JobRepairUpdateTimeRequest param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.UpdateTime(param));

            return resp;
        }

        public async Task<Response> ReportRepairAdmin(ExportJobRepairFilter param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.ReportRepairAdmin(param));

            return resp;
        }

        public async Task<Response> ExportJobRepair(ExportJobRepairRequest param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.ExportJobRepair(param));

            return resp;
        }

        public async Task<Response> ExportJobRepairMonth(ExportJobRepairMonthRequest param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.ExportJobRepairMonth(param));

            return resp;
        }
    }
}

