using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DOL.API.Extension.Helper;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs;
using DOL.API.Models.Response;
using DOL.API.Repositories;
using DOL.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;
using WatchDog;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DOL.API.Controllers
{
    [Route("api/[controller]")]
    public class AuthorizationController : Controller
    {
        private readonly IAuthorizationRepo repoCollection;

        public AuthorizationController()
        {
            repoCollection = new AuthorizationRepo();
        }

        // GET: api/values
        [HttpGet]
        [Route("Get")]
        public async Task<IActionResult> Get([FromQuery] AuthorizationModel param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.GenerateToken(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.TotalSeconds.ToString("N2") + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
                result.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        }
    }
}

