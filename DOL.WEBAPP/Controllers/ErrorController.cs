using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DOL.WEBAPP.Extension;
using DOL.WEBAPP.Models.Response;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DOL.WEBAPP.Controllers
{
    public class ErrorController : Controller
    {
        // GET: /<controller>/
        public IActionResult Index()
        {
            return View();
        }

        public IActionResult Error403()
        {
            //UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

            //if (userInfo != null)
            //{
            //    return RedirectToAction("Index", "Home");
            //}

            return View();
        }
    }
}

