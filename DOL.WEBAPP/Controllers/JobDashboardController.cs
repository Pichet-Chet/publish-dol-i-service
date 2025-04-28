using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.WEBAPP.Extension;
using DOL.WEBAPP.Models.Filter;
using DOL.WEBAPP.Models.Response;
using DOL.WEBAPP.Repository.AuthenticationRepository;
using DOL.WEBAPP.Repository.JobOnsiteRepository;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DOL.WEBAPP.Controllers
{
    public class JobDashboardController : Controller
    {
        IConfiguration _configuration;

        private readonly IJobOnsiteRepository _repoCollection;

        public JobDashboardController(IConfiguration configuration)
        {
            _configuration = configuration;

            _repoCollection = new JobOnsiteRepository(configuration);

        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

            if (userInfo == null)
            {
                return RedirectToAction("Index", "SignIn");
            }


            return View(userInfo);
        }


        public async Task<IActionResult> Get(SiteInformationFilter param)
        {
            UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

            Response resp = new Response();

            if (userInfo == null)
            {
                return RedirectToAction("Index", "SignIn");
            }

            else
            {
                param.SysUserId = userInfo.Id;

                resp = await Task.Run(() => _repoCollection.CardJobs(param));
            }


            return new JsonResult(resp);
        }
    }
}

