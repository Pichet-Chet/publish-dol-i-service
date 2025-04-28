using System;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Customs;
using DOL.API.Models.Customs.Response;
using DOL.API.Models.Customs.View;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DOL.API.Repositories
{
    public class AuthenticationRepo : IAuthenticationRepo
    {
        private readonly DolContext _chmContext;

        private readonly AuthenticationService service;

        private readonly AuthorizationService serviceAuthorization;

        public AuthenticationRepo()
        {
            _chmContext = new DolContext();

            service = new AuthenticationService(_chmContext);
            serviceAuthorization = new AuthorizationService(_chmContext);
        }

        public async Task<Response> SignIn(AuthorizationModel param)
        {
            Response resp = new Response();

            AuthorizationModel authorizationModel = new AuthorizationModel();


            SysUser sysUser = new SysUser();
            SignInResponse signInResponse = new SignInResponse();



            string? username = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Username"];
            string? password = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Jwt")["Password"];

            authorizationModel.UserName = username;
            authorizationModel.Password = password;


            resp = await Task.Run(() => serviceAuthorization.GenerateToken(authorizationModel));


            if (resp.status == true)
            {
                signInResponse.Token = Convert.ToString(resp.data);

                resp = await Task.Run(() => service.SignIn(param));

                if (resp.status == true)
                {
                    var json = JsonConvert.SerializeObject(resp.data);

                    sysUser = JsonConvert.DeserializeObject<SysUser>(json);

                    AppHelper.TransferData_ClassA_to_ClassB<SysUser, SignInResponse>(sysUser, ref signInResponse, new List<string>() { "Token" });

                    resp.data = signInResponse;

                }
            }

            return resp;
        }
    }
}

