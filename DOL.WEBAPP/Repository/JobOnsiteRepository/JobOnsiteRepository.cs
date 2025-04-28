using System;
using System.Net.Http.Headers;
using DOL.WEBAPP.Extension;
using DOL.WEBAPP.Models;
using DOL.WEBAPP.Models.Filter;
using DOL.WEBAPP.Models.Request;
using DOL.WEBAPP.Models.Response;
using Newtonsoft.Json;

namespace DOL.WEBAPP.Repository.JobOnsiteRepository
{
    public class JobOnsiteRepository : IJobOnsiteRepository
    {
        IConfiguration _configuration;

        private string _domain = string.Empty;

        public JobOnsiteRepository(IConfiguration configuration)
        {
            _configuration = configuration;
            _domain = _configuration["DefaultApi"];
        }

        public async Task<Response> CardJobs(SiteInformationFilter param)
        {
            Response resp = new Response();

            try
            {
                var queryString = AppHelper.GetQueryString(param);

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(_domain + $"api/SiteInformation/CardJobs?" + queryString);

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);


                if (response.IsSuccessStatusCode)
                {
                    string data = await Task.Run(() => response.Content.ReadAsStringAsync().Result);

                    resp = JsonConvert.DeserializeObject<Response>(data);

                    if (resp.status == true)
                    {
                        resp.data = JsonConvert.DeserializeObject<List<JobOnsiteCardResponse>>(resp.data.ToString());
                    }
                }
                else
                {
                    resp.message = Convert.ToString(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                resp.exception = ex.Message;
            }

            return resp;
        }


        public async Task<Response> Detail(int id)
        {
            Response resp = new Response();

            try
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(_domain + $"api/SiteInformation/" + id);

                HttpResponseMessage response = await client.GetAsync(client.BaseAddress);


                if (response.IsSuccessStatusCode)
                {
                    string data = await Task.Run(() => response.Content.ReadAsStringAsync().Result);

                    resp = JsonConvert.DeserializeObject<Response>(data);

                    if (resp.status == true)
                    {
                        resp.data = JsonConvert.DeserializeObject<SiteInformationResponse>(resp.data.ToString());
                    }
                }
                else
                {
                    resp.message = Convert.ToString(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                resp.exception = ex.Message;
            }

            return resp;
        }


        public async Task<Response> Update(SiteInformationRequest param)
        {
            Response resp = new Response();

            try
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(_domain + $"api/SiteInformation");

                HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress , param);


                if (response.IsSuccessStatusCode)
                {
                    string data = await Task.Run(() => response.Content.ReadAsStringAsync().Result);

                    resp = JsonConvert.DeserializeObject<Response>(data);

                    if (resp.status == true)
                    {
                        resp.data = JsonConvert.DeserializeObject<SiteInformationResponse>(resp.data.ToString());
                    }
                }
                else
                {
                    resp.message = Convert.ToString(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                resp.exception = ex.Message;
            }

            return resp;
        }


        public async Task<Response> UpdateImageName(UpdateImageNameRequest param)
        {
            Response resp = new Response();

            try
            {
                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(_domain + $"api/SiteInformation/UpdateImageName");

                HttpResponseMessage response = await client.PutAsJsonAsync(client.BaseAddress, param);


                if (response.IsSuccessStatusCode)
                {
                    string data = await Task.Run(() => response.Content.ReadAsStringAsync().Result);

                    resp = JsonConvert.DeserializeObject<Response>(data);

                    if (resp.status == true)
                    {
                        resp.data = JsonConvert.DeserializeObject<SiteInformation>(resp.data.ToString());
                    }
                }
                else
                {
                    resp.message = Convert.ToString(response.StatusCode);
                }
            }
            catch (Exception ex)
            {
                resp.exception = ex.Message;
            }

            return resp;
        }

    }
}

