using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using DOL.WEBAPP.Models;
using DOL.WEBAPP.Extension;
using DOL.WEBAPP.Models.Response;

namespace DOL.WEBAPP.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

        if (userInfo == null)
        {
            return RedirectToAction("Index", "SignIn");
        }


        return View(userInfo);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}

