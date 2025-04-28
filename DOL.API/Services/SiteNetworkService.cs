using System;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Services.Validation;
using Microsoft.EntityFrameworkCore;
using WatchDog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DOL.API.Services
{
    public class SiteNetworkService
    {
        private readonly DolContext _context;

        public SiteNetworkService(DolContext context)
        {
            _context = context;
        }


        public async Task<Response> Get(SiteNetworkFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            try
            {
                var queryable = await Task.Run(() => _context.SiteNetworks.AsQueryable());

                #region Sorting

                queryable = SiteNetworkFilter.ApplySorting(queryable, param.SortName, param.SortType);

                #endregion

                #region Filter Data

                if (param.Id != null)
                {
                    queryable = queryable.Where(x => x.Id == param.Id).AsQueryable();
                }

                if (!string.IsNullOrWhiteSpace(param.TextSearch))
                {
                    queryable = queryable.Where(x =>
                    x.Name.ToLower().Contains(param.TextSearch.ToLower())
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Name))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(param.Name.ToLower())).AsQueryable();
                }

                if (param.JobWan1 != null)
                {
                    queryable = queryable.Where(x => x.JobWan1 == param.JobWan1).AsQueryable();
                }

                if (param.JobWan2 != null)
                {
                    queryable = queryable.Where(x => x.JobWan2 == param.JobWan2).AsQueryable();
                }

                if (param.JobInternet != null)
                {
                    queryable = queryable.Where(x => x.JobInternet == param.JobInternet).AsQueryable();
                }

                if (param.JobCorpnet != null)
                {
                    queryable = queryable.Where(x => x.JobCorpnet == param.JobCorpnet).AsQueryable();
                }

                if (param.JobCellular != null)
                {
                    queryable = queryable.Where(x => x.JobCellular == param.JobCellular).AsQueryable();
                }

                if (param.JobDevice != null)
                {
                    queryable = queryable.Where(x => x.JobDevice == param.JobDevice).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }


                List<SiteNetwork> execute = new List<SiteNetwork>();

                execute = queryable.AsNoTracking().ToList();

                resp.effectRow = execute.Count();

                #endregion


                if (param.isAll != null)
                {
                    if (param.isAll == true)
                    {
                        execute = execute.ToList();
                    }
                    else
                    {
                        execute = execute
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize)
                       .ToList();
                    }
                }
                else
                {
                    execute = execute
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }

                if (execute != null && execute.Count > 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = execute;

                    resp.pageNumber = param.PageNumber;
                    resp.pageSize = param.PageSize;
                }

                else
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeDataNotFound;
                    resp.message = Constants.recordDataNotFound;
                    resp.effectRow = null;
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = Constants.httpCode500Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return resp;
        }

        public async Task<Response> Detail(int id)
        {
            Response resp = new Response();

            SiteNetwork objData = new SiteNetwork();

            try
            {
                var queryable = await Task.Run(() => _context.SiteNetworks.Where(x => x.Id == id).AsQueryable());

                var execute = queryable.AsNoTracking().FirstOrDefault();

                if (execute != null)
                {
                    objData = execute;

                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = objData;
                }

                else
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeDataNotFound;
                    resp.message = Constants.recordDataNotFound;
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = Constants.httpCode500Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }
            return resp;
        }

        public async Task<Response> Create(SiteNetwork param)
        {
            Response resp = new Response();

            try
            {
                #region Model Setting

                param.Id = _context.SiteNetworks.ToList().Count() > 0 ? _context.SiteNetworks.Max(x => x.Id) + 1 : 0;

                param.CreateDate = DateTime.Now;
                param.UpdateDate = DateTime.Now;

                #endregion

                #region Validation Zone

                Response validation = new Response();

                Func<Response>[] validationFunctions = new Func<Response>[]
                {
                    () => SiteNetworkValidation.Id(param.Id),
                    () => SiteNetworkValidation.Name(param.Name),
                    () => SiteNetworkValidation.JobWan1(param.JobWan1),
                    () => SiteNetworkValidation.JobWan2(param.JobWan2),
                    () => SiteNetworkValidation.JobInternet(param.JobInternet),
                    () => SiteNetworkValidation.JobCorpnet(param.JobCorpnet),
                    () => SiteNetworkValidation.JobCellular(param.JobCellular),
                    () => SiteNetworkValidation.JobDevice(param.JobDevice),

                    () => SiteNetworkValidation.CreateDate(param.CreateDate),
                    () => SiteNetworkValidation.CreateBy(param.CreateBy),
                    () => SiteNetworkValidation.UpdateDate(param.UpdateDate),
                    () => SiteNetworkValidation.UpdateBy(param.UpdateBy),
                    () => SiteNetworkValidation.IsActive(param.IsActive),

                    () => SiteNetworkValidation.NameLenght(param.Name.Length),
                    () => SiteNetworkValidation.CreateByLenght(param.CreateBy.Length),
                    () => SiteNetworkValidation.UpdateByLenght(param.UpdateBy.Length),
                };

                foreach (var func in validationFunctions)
                {
                    if (func() != null)
                    {
                        if (func().status == false)
                        {
                            validation.httpCode = Constants.httpCode200;
                            validation.status = Constants.statusError;
                            validation.statusCode = Constants.statusCodeOK;
                            validation.message = func().message;

                            return validation;
                        }
                        else
                        {
                            validation.status = true;
                        }
                    }
                }

                if (validation.status == true)
                {
                    var DuplicationCheck = _context.SiteNetworks.Where(x => x.Name.ToLower() == param.Name.ToLower()).Any();

                    if (DuplicationCheck == false)
                    {
                        validation.status = true;
                    }
                    else
                    {
                        validation.status = false;
                        validation.httpCode = Constants.httpCode200;
                        validation.status = Constants.statusError;
                        validation.statusCode = Constants.statusCodeOK;
                        validation.message = Constants.invalidDataDuplicate;

                        return validation;
                    }
                }

                #endregion

                if (validation.status == true)
                {
                    await Task.Run(() => _context.SiteNetworks.Add(param));

                    _context.SaveChanges();

                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = param;

                }
                else
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.message = validation.message;
                }


            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = Constants.httpCode500Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }
            return resp;
        }

        public async Task<Response> Update(SiteNetwork param)
        {
            Response resp = new Response();

            try
            {
                var DuplicationCheck = _context.SiteNetworks.Where(x
                    => x.Name.ToLower() == param.Name.ToLower()
                    && x.Id != param.Id).Any();

                if (DuplicationCheck == false)
                {
                    var update = await Task.Run(() => _context.SiteNetworks.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (update != null)
                    {

                        AppHelper.TransferData_ClassA_to_ClassB<SiteNetwork, SiteNetwork>(param, ref update, new List<string>() { AppHelper.CreateBy, AppHelper.CreateDate });

                        update.CreateBy = update.CreateBy;
                        update.CreateDate = update.CreateDate;
                        update.UpdateDate = DateTime.Now;

                        #region Validation Zone

                        Response validation = new Response();

                        Func<Response>[] validationFunctions = new Func<Response>[]
                        {

                            () => SiteNetworkValidation.Id(param.Id),
                            () => SiteNetworkValidation.Name(param.Name),
                            () => SiteNetworkValidation.JobWan1(param.JobWan1),
                            () => SiteNetworkValidation.JobWan2(param.JobWan2),
                            () => SiteNetworkValidation.JobInternet(param.JobInternet),
                            () => SiteNetworkValidation.JobCorpnet(param.JobCorpnet),
                            () => SiteNetworkValidation.JobCellular(param.JobCellular),
                            () => SiteNetworkValidation.JobDevice(param.JobDevice),

                            () => SiteNetworkValidation.CreateDate(param.CreateDate),
                            () => SiteNetworkValidation.CreateBy(param.CreateBy),
                            () => SiteNetworkValidation.UpdateDate(param.UpdateDate),
                            () => SiteNetworkValidation.UpdateBy(param.UpdateBy),
                            () => SiteNetworkValidation.IsActive(param.IsActive),

                            () => SiteNetworkValidation.NameLenght(param.Name.Length),
                            () => SiteNetworkValidation.CreateByLenght(param.CreateBy.Length),
                            () => SiteNetworkValidation.UpdateByLenght(param.UpdateBy.Length),
                        };

                        foreach (var func in validationFunctions)
                        {
                            if (func() != null)
                            {
                                if (func().status == false)
                                {
                                    validation.httpCode = Constants.httpCode200;
                                    validation.status = Constants.statusError;
                                    validation.statusCode = Constants.statusCodeOK;
                                    validation.message = func().message;

                                    return validation;
                                }
                                else
                                {
                                    validation.status = true;
                                }
                            }
                        }

                        #endregion

                        if (validation.status == true)
                        {
                            await _context.SaveChangesAsync();

                            resp.httpCode = Constants.httpCode200;
                            resp.status = Constants.statusSuccess;
                            resp.statusCode = Constants.statusCodeOK;
                            resp.data = param;
                        }
                        else
                        {
                            resp.httpCode = Constants.httpCode200;
                            resp.status = Constants.statusError;
                            resp.statusCode = Constants.statusCodeOK;
                            resp.message = validation.message;
                        }
                    }
                    else
                    {
                        resp.httpCode = Constants.httpCode400;
                        resp.status = Constants.statusError;
                        resp.statusCode = Constants.statusCodeDataNotFound;
                        resp.message = Constants.recordDataNotFound;
                    }
                }
                else
                {
                    resp.status = false;
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.message = Constants.invalidDataDuplicate;

                    return resp;
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = Constants.httpCode500Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }
            return resp;
        }

        public async Task<Response> WorkActive(int id)
        {
            Response resp = new Response();

            SiteNetwork objData = new SiteNetwork();

            List<Dropdown> dropdowns = new List<Dropdown>();

            Dropdown dropdown = new Dropdown();

            try
            {
                var queryable = await Task.Run(() => _context.SiteNetworks.Where(x => x.Id == id).AsQueryable());

                var execute = queryable.AsNoTracking().FirstOrDefault();

                if (execute != null)
                {

                    if (execute.JobWan1)
                    {
                        dropdown = new Dropdown();

                        dropdown.data = Constants.jobWan1String;
                        dropdown.value = Constants.jobWan1;

                        dropdowns.Add(dropdown);
                    }

                    if (execute.JobWan2)
                    {
                        dropdown = new Dropdown();
                        dropdown.data = Constants.jobWan2String;
                        dropdown.value = Constants.jobWan2;

                        dropdowns.Add(dropdown);
                    }

                    if (execute.JobInternet)
                    {
                        dropdown = new Dropdown();
                        dropdown.data = Constants.jobInternetString;
                        dropdown.value = Constants.jobInternet;

                        dropdowns.Add(dropdown);
                    }

                    if (execute.JobCorpnet)
                    {
                        dropdown = new Dropdown();
                        dropdown.data = Constants.jobCorpnetString;
                        dropdown.value = Constants.jobCorpnet;

                        dropdowns.Add(dropdown);
                    }

                    if (execute.JobCellular)
                    {
                        dropdown = new Dropdown();
                        dropdown.data = Constants.jobCellularString;
                        dropdown.value = Constants.jobCellular;

                        dropdowns.Add(dropdown);
                    }

                    
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = dropdowns;
                }

                else
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeDataNotFound;
                    resp.message = Constants.recordDataNotFound;
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = Constants.httpCode500Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }
            return resp;
        }



        public async Task<Response> repairActive(int siteId)
        {
            Response resp = new Response();

            SiteNetwork objData = new SiteNetwork();

            List<Dropdown> dropdowns = new List<Dropdown>();

            Dropdown dropdown = new Dropdown();

            try
            {
                var queryable = await Task.Run(() => _context.SiteNetworks.AsQueryable());

                var queryableSite = await Task.Run(() => _context.SiteInformations.Where(x => x.Id == siteId).AsQueryable());


                var execute = queryable.AsNoTracking().FirstOrDefault();

                var executeSite = queryableSite.AsNoTracking().FirstOrDefault();

                if (execute != null)
                {

                    if (execute.JobWan1 != null)
                    {
                        dropdown = new Dropdown();

                        dropdown.data = Constants.jobWan1String;
                        dropdown.value = Constants.jobWan1;

                        dropdowns.Add(dropdown);
                    }

                    if (execute.JobWan2 != null)
                    {
                        dropdown = new Dropdown();
                        dropdown.data = Constants.jobWan2String;
                        dropdown.value = Constants.jobWan2;

                        dropdowns.Add(dropdown);
                    }

                    if (execute.JobInternet != null)
                    {
                        dropdown = new Dropdown();
                        dropdown.data = Constants.jobInternetString;
                        dropdown.value = Constants.jobInternet;

                        dropdowns.Add(dropdown);
                    }

                    if (execute.JobCorpnet != null)
                    {
                        dropdown = new Dropdown();
                        dropdown.data = Constants.jobCorpnetString;
                        dropdown.value = Constants.jobCorpnet;

                        dropdowns.Add(dropdown);
                    }

                    if (execute.JobCellular != null)
                    {
                        dropdown = new Dropdown();
                        dropdown.data = Constants.jobCellularString;
                        dropdown.value = Constants.jobCellular;

                        dropdowns.Add(dropdown);
                    }

                    if (dropdowns != null && dropdowns.Count > 0)
                    {
                        List<string> notUser = new List<string>();

                        if (executeSite.MainMaxHour == null || executeSite.MainMaxHour == 0)
                        {
                            notUser.Add(Constants.jobWan1);
                        }

                        if (executeSite.SecondaryMaxHour == null || executeSite.SecondaryMaxHour == 0)
                        {
                            notUser.Add(Constants.jobWan2);
                        }

                        if (executeSite.InternetMaxHour == null || executeSite.InternetMaxHour == 0)
                        {
                            notUser.Add(Constants.jobInternet);
                        }

                        if (executeSite.CorpnetMaxHour == null || executeSite.CorpnetMaxHour == 0)
                        {
                            notUser.Add(Constants.jobCorpnet);
                        }

                        if (executeSite.CellularMaxHour == null || executeSite.CellularMaxHour == 0)
                        {
                            notUser.Add(Constants.jobCellular);
                        }

                        var itemsToRemove = dropdowns.Where(item => notUser.Contains(item.value)).ToList();

                        foreach (var itemToRemove in itemsToRemove)
                        {
                            dropdowns.Remove(itemToRemove);
                        }
                    }


                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = dropdowns;
                }

                else
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeDataNotFound;
                    resp.message = Constants.recordDataNotFound;
                }
            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = Constants.httpCode500Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }
            return resp;
        }
    }
}

