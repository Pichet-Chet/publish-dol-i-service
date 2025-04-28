using System;
using DOL.WEBAPP.Extension;
using DOL.WEBAPP.Models.Filter;
using DOL.WEBAPP.Models.Response;
using Newtonsoft.Json;


namespace DOL.WEBAPP.Repository.AuthenticationRepository
{
    public class AuthenticationRepositories : IAuthenticationRepositories
    {
        IConfiguration _configuration;
        private string _domain = string.Empty;

        public AuthenticationRepositories(IConfiguration configuration)
        {
            _configuration = configuration;
            _domain = _configuration["DefaultApi"];
        }



        public async Task<Response> SignIn(AuthenticationFilter param)
        {
            Response resp = new Response();

            try
            {
                var queryString = AppHelper.GetQueryString(param);

                HttpClient client = new HttpClient();

                client.BaseAddress = new Uri(_domain + $"api/Authentication");

                HttpResponseMessage response = await client.PostAsJsonAsync(client.BaseAddress, param);

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

