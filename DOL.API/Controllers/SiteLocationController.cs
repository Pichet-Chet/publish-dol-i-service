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
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DOL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SiteLocationController : ControllerBase
    {
        //private readonly ISiteLocationRepo repoCollection;

        //public SiteLocationController()
        //{
        //    this.repoCollection = new SiteLocationRepo();
        //}

        // GET: api/values
        //[HttpGet]
        //public async Task<IActionResult> Get([FromQuery] SiteLocationFilter param)
        //{
        //    Response result = new Response();

        //    try
        //    {
        //        var watch = new Stopwatch();

        //        watch.Start();

        //        result = await Task.Run(() => repoCollection.Get(param));

        //        watch.Stop();

        //        result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.httpCode = Constants.httpCode500;
        //        result.status = Constants.statusError;
        //        result.statusCode = Constants.statusCodeException;
        //        result.message = Constants.httpCode500Message;
        //    }

        //    return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        //}

        //// GET api/values/5
        //[HttpGet]
        //[Route("{id}")]
        //public async Task<IActionResult> Detail(int id)
        //{
        //    Response result = new Response();

        //    try
        //    {
        //        var watch = new Stopwatch();

        //        watch.Start();

        //        result = await Task.Run(() => repoCollection.Detail(id));

        //        watch.Stop();

        //        result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.httpCode = Constants.httpCode500;
        //        result.status = Constants.statusError;
        //        result.statusCode = Constants.statusCodeException;
        //        result.message = Constants.httpCode500Message;
        //    }

        //    return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        //}

        //[HttpGet]
        //[Route("Province")]
        //public async Task<IActionResult> Province()
        //{
        //    Response result = new Response();

        //    try
        //    {
        //        var watch = new Stopwatch();

        //        watch.Start();

        //        result = await Task.Run(() => repoCollection.Province());

        //        watch.Stop();

        //        result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.httpCode = Constants.httpCode500;
        //        result.status = Constants.statusError;
        //        result.statusCode = Constants.statusCodeException;
        //        result.message = Constants.httpCode500Message;
        //    }

        //    return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        //}

        //[HttpGet]
        //[Route("Location")]
        //public async Task<IActionResult> Location([FromQuery] string province)
        //{
        //    Response result = new Response();

        //    try
        //    {
        //        var watch = new Stopwatch();

        //        watch.Start();

        //        result = await Task.Run(() => repoCollection.Location(province));

        //        watch.Stop();

        //        result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.httpCode = Constants.httpCode500;
        //        result.status = Constants.statusError;
        //        result.statusCode = Constants.statusCodeException;
        //        result.message = Constants.httpCode500Message;
        //    }

        //    return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        //}

        //// POST api/values
        //[HttpPost]
        //public async Task<IActionResult> Create([FromBody] SiteLocation param)
        //{
        //    Response result = new Response();

        //    try
        //    {
        //        var watch = new Stopwatch();

        //        watch.Start();

        //        result = await Task.Run(() => repoCollection.Create(param));

        //        watch.Stop();

        //        result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.httpCode = Constants.httpCode500;
        //        result.status = Constants.statusError;
        //        result.statusCode = Constants.statusCodeException;
        //        result.message = Constants.httpCode500Message;
        //    }

        //    return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        //}

        //// PUT api/values/5
        //[HttpPatch]
        //public async Task<IActionResult> Update([FromBody] SiteLocation param)
        //{
        //    Response result = new Response();

        //    try
        //    {
        //        var watch = new Stopwatch();

        //        watch.Start();

        //        result = await Task.Run(() => repoCollection.Update(param));

        //        watch.Stop();

        //        result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
        //    }
        //    catch (Exception ex)
        //    {
        //        result.httpCode = Constants.httpCode500;
        //        result.status = Constants.statusError;
        //        result.statusCode = Constants.statusCodeException;
        //        result.message = Constants.httpCode500Message;
        //    }

        //    return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        //}


    }
}

