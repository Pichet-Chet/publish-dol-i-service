using System;
using DOL.WEBAPP.Extension;
using DOL.WEBAPP.Models.Filter;
using DOL.WEBAPP.Models.Response;
using Newtonsoft.Json;

namespace DOL.WEBAPP.Repository.JobRepairRepository
{
    public class JobRepairRepository : IJobRepairRepository
    {
        IConfiguration _configuration;
        private string _domain = string.Empty;

        public JobRepairRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _domain = _configuration["DefaultApi"];
        }

        public async Task<Response> Get(JobRepairFilter param)
        {
            Response resp = new Response();

            try
            {
                var queryString = AppHelper.GetQueryString(param);

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(_domain + $"api/Authentication?" + queryString);

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);

                string data = await Task.Run(() => response.Content.ReadAsStringAsync().Result);

                resp = JsonConvert.DeserializeObject<Response>(data);

            }
            catch (Exception ex)
            {
                resp.exception = ex.Message;
            }

            return resp;
        }
    }
}

