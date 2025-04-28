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
    public class SysStatusController : ControllerBase
    {
        private readonly ISysStatusRepo repoCollection;

        public SysStatusController()
        {
            this.repoCollection = new SysStatusRepo();
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SysStatusFilter param)
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

        // GET api/values/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Detail(id));

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

        // POST api/values
        [HttpPost]
        public async Task<IActionResult> Create([FromBody] SysStatus param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Create(param));

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

        // PUT api/values/5
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] SysStatus param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Update(param));

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

