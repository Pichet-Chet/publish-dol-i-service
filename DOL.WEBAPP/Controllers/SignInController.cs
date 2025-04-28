using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.WEBAPP.Extension;
using DOL.WEBAPP.Models;
using DOL.WEBAPP.Models.Filter;
using DOL.WEBAPP.Models.Response;
using DOL.WEBAPP.Repository.AuthenticationRepository;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DOL.WEBAPP.Controllers
{
    public class SignInController : Controller
    {
        IConfiguration _configuration;

        private readonly IAuthenticationRepositories _repoCollection;

        public SignInController(IConfiguration configuration)
        {
            _configuration = configuration;

            _repoCollection = new AuthenticationRepositories(configuration);

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

            if (userInfo != null)
            {
                return RedirectToAction("Index", "Home");
            }


            return View();
        }


        [HttpPost]
        public async Task<IActionResult> VerifyAccount([FromBody] AuthenticationFilter param)
        {
            Response resp = new Response();

            try
            {

                string? roleSetting = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Allow")["Roles"];

                resp = await Task.Run(() => _repoCollection.SignIn(param));

                if (resp.status == true)
                {

                    UserInfo userInfo = new UserInfo();

                    SysUserResponse sysUser = new SysUserResponse();

                    sysUser = JsonConvert.DeserializeObject<SysUserResponse>(resp.data.ToString());

                    AppHelper.TransferData_ClassA_to_ClassB<SysUserResponse, UserInfo>(sysUser, ref userInfo, new List<string>());

                    string[] rolesArray = roleSetting.Split(',');

                    // Convert the array to a List<string> if needed
                    List<string> rolesList = rolesArray.ToList();

                    if (rolesList.Contains(userInfo.UserGroup.ToLower()))
                    {
                        SessionHelper.SetObjectAsJson(this.HttpContext.Session, "UserInfo", userInfo);
                    }

                    else
                    {
                        resp.statusCode = 403;
                    }

                }
            }
            catch (Exception ex)
            {
                resp.status = false;
                resp.exception = ex.Message;
            }

            return new JsonResult(resp);
        }
    }
}

