using System.Diagnostics;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories;
using DOL.API.Repositories.Interface;
using Microsoft.AspNetCore.Mvc;


namespace DOL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class CaseOfFixedController : ControllerBase
    {
        private readonly ICaseOfFixedRepo repoCollection;

        public CaseOfFixedController()
        {
            this.repoCollection = new CaseOfFixedRepo();
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CaseOfFixedFilter param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Get(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = ex.InnerException == null ? ex.Message : ex.InnerException.Message.ToString();
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        }

        [HttpGet]
        [Route("Dropdrown")]
        public async Task<IActionResult> Dropdrown()
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Dropdown());

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

