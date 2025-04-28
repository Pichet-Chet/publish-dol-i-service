using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DOL.API.Extension.Helper;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories;
using DOL.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DOL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class AuthenticationController : Controller
    {
        private readonly IAuthenticationRepo repoCollection;

        public AuthenticationController()
        {
            this.repoCollection = new AuthenticationRepo();
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> SignIn([FromQuery] AuthorizationModel param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.SignIn(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        }

        [HttpPost]
        public async Task<IActionResult> Login([FromBody] AuthorizationModel param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.SignIn(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        }
    }
}

