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
    public class CaseOfIssueController : ControllerBase
    {
        private readonly ICaseOfIssueRepo repoCollection;

        public CaseOfIssueController()
        {
            this.repoCollection = new CaseOfIssueRepo();
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] CaseOfIssueFilter param)
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
                result.message = Constants.httpCode500Message;
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

