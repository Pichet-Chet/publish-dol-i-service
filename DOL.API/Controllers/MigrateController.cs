using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Constants;
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
    public class MigrateController : ControllerBase
    {
        private readonly IMigrateRepo repoCollection;

        public MigrateController()
        {
            this.repoCollection = new MigrateRepo();
        }
        
       

        // POST api/values
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> SiteInformation(IFormFile? FileUpload)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.SiteInformation(FileUpload));

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

