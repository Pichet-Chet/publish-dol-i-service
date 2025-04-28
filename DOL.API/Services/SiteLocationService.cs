//using System;
//using DOL.API.Extension.Helper;
//using DOL.API.Models;
//using DOL.API.Models.Constants;
//using DOL.API.Models.Filters;
//using DOL.API.Models.Response;
//using DOL.API.Services.Validation;
//using Microsoft.EntityFrameworkCore;
//using WatchDog;

//namespace DOL.API.Services
//{
//    public class SiteLocationService
//    {
//        private readonly DolContext _context;

//        public SiteLocationService(DolContext context)
//        {
//            _context = context;
//        }


//        public async Task<Response> Get(SiteLocationFilter param) // Additional models are imported from GlobalFilter and PaginationModel
//        {
//            Response resp = new Response();

//            try
//            {
//                var queryable = await Task.Run(() => _context.SiteLocations.AsQueryable());

//                #region Filter Data

//                if (param.Id != null)
//                {
//                    queryable = queryable.Where(x => x.Id == param.Id).AsQueryable();
//                }

//                if (!string.IsNullOrWhiteSpace(param.textSearch))
//                {
//                    queryable = queryable.Where(x =>
//                    x.ProvinceName.ToLower().Contains(param.textSearch.ToLower()) ||
//                    x.LocationName.ToLower().Contains(param.textSearch.ToLower())
//                    ).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.ProvinceName))
//                {
//                    queryable = queryable.Where(x => x.ProvinceName.ToLower().Contains(param.ProvinceName.ToLower())).AsQueryable();
//                }

//                if (!string.IsNullOrEmpty(param.LocationName))
//                {
//                    queryable = queryable.Where(x => x.LocationName.ToLower().Contains(param.LocationName.ToLower())).AsQueryable();
//                }

//                List<SiteLocation> execute = new List<SiteLocation>();

//                execute = queryable.AsNoTracking().ToList();

//                #endregion


//                if (param.isAll != null)
//                {
//                    if (param.isAll == true)
//                    {
//                        execute = execute.ToList();
//                    }
//                    else
//                    {
//                        execute = execute
//                       .Skip((param.PageNumber - 1) * param.PageSize)
//                       .Take(param.PageSize)
//                       .ToList();
//                    }
//                }
//                else
//                {
//                    execute = execute
//                   .Skip((param.PageNumber - 1) * param.PageSize)
//                   .Take(param.PageSize)
//                   .ToList();
//                }

//                if (execute != null && execute.Count > 0)
//                {
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusSuccess;
//                    resp.statusCode = Constants.statusCodeOK;
//                    resp.data = execute;

//                    resp.pageNumber = param.PageNumber;
//                    resp.pageSize = param.PageSize;
//                    resp.effectRow = execute.Count();
//                }

//                else
//                {
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusError;
//                    resp.statusCode = Constants.statusCodeDataNotFound;
//                    resp.message = Constants.recordDataNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.httpCode = Constants.httpCode500;
//                resp.status = Constants.statusError;
//                resp.statusCode = Constants.statusCodeException;
//                resp.message = Constants.httpCode500Message;
//                resp.exception = ex.Message;

//                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
//            }

//            return resp;
//        }

//        public async Task<Response> Detail(int id)
//        {
//            Response resp = new Response();

//            SiteLocation objData = new SiteLocation();

//            try
//            {
//                var queryable = await Task.Run(() => _context.SiteLocations.Where(x => x.Id == id).AsQueryable());

//                var execute = queryable.AsNoTracking().FirstOrDefault();

//                if (execute != null)
//                {
//                    objData = execute;

//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusSuccess;
//                    resp.statusCode = Constants.statusCodeOK;
//                    resp.data = objData;
//                }

//                else
//                {
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusError;
//                    resp.statusCode = Constants.statusCodeDataNotFound;
//                    resp.message = Constants.recordDataNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.httpCode = Constants.httpCode500;
//                resp.status = Constants.statusError;
//                resp.statusCode = Constants.statusCodeException;
//                resp.message = Constants.httpCode500Message;
//                resp.exception = ex.Message;

//                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
//            }
//            return resp;
//        }

//        public async Task<Response> Create(SiteLocation param)
//        {
//            Response resp = new Response();

//            try
//            {
//                #region Model Setting

//                param.Id = _context.SiteLocations.ToList().Count() > 0 ? _context.SiteLocations.Max(x => x.Id) + 1 : 0;

//                param.CreateDate = DateTime.Now;
//                param.UpdateDate = DateTime.Now;

//                #endregion

//                #region Validation Zone

//                Response validation = new Response();

//                Func<Response>[] validationFunctions = new Func<Response>[]
//                {
//                    () => SiteLocationValidation.Id(param.Id),
//                    () => SiteLocationValidation.ProviceName(param.ProvinceName),
//                    () => SiteLocationValidation.LocationName(param.LocationName),

//                    () => SiteLocationValidation.CreateDate(param.CreateDate),
//                    () => SiteLocationValidation.CreateBy(param.CreateBy),
//                    () => SiteLocationValidation.UpdateDate(param.UpdateDate),
//                    () => SiteLocationValidation.UpdateBy(param.UpdateBy),
//                    () => SiteLocationValidation.IsActive(param.IsActive),

//                    () => SiteLocationValidation.CreateByLenght(param.CreateBy.Length),
//                    () => SiteLocationValidation.UpdateByLenght(param.UpdateBy.Length),

//                };

//                foreach (var func in validationFunctions)
//                {
//                    if (func() != null)
//                    {
//                        if (func().status == false)
//                        {
//                            validation.httpCode = Constants.httpCode200;
//                            validation.status = Constants.statusError;
//                            validation.statusCode = Constants.statusCodeOK;
//                            validation.message = func().message;

//                            return validation;
//                        }
//                        else
//                        {
//                            validation.status = true;
//                        }
//                    }
//                }

//                if (validation.status == true)
//                {
//                    var DuplicationCheck = _context.SiteLocations.Where(x => x.ProvinceName.ToLower() == param.ProvinceName.ToLower() && x.LocationName == param.LocationName).Any();

//                    if (DuplicationCheck == false)
//                    {
//                        validation.status = true;
//                    }
//                    else
//                    {
//                        validation.status = false;
//                        validation.httpCode = Constants.httpCode200;
//                        validation.status = Constants.statusError;
//                        validation.statusCode = Constants.statusCodeOK;
//                        validation.message = Constants.invalidDataDuplicate;

//                        return validation;
//                    }
//                }

//                #endregion

//                if (validation.status == true)
//                {
//                    await Task.Run(() => _context.SiteLocations.Add(param));

//                    _context.SaveChanges();

//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusSuccess;
//                    resp.statusCode = Constants.statusCodeOK;
//                    resp.data = param;

//                }
//                else
//                {
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusError;
//                    resp.statusCode = Constants.statusCodeOK;
//                    resp.message = validation.message;
//                }


//            }
//            catch (Exception ex)
//            {
//                resp.httpCode = Constants.httpCode500;
//                resp.status = Constants.statusError;
//                resp.statusCode = Constants.statusCodeException;
//                resp.message = Constants.httpCode500Message;
//                resp.exception = ex.Message;

//                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
//            }
//            return resp;
//        }

//        public async Task<Response> Update(SiteLocation param)
//        {
//            Response resp = new Response();

//            try
//            {
//                var DuplicationCheck = _context.SiteLocations.Where(x
//                    => x.ProvinceName.ToLower() == param.ProvinceName.ToLower() &&
//                    x.LocationName.ToLower() == param.LocationName.ToLower()
//                    && x.Id != param.Id).Any();

//                if (DuplicationCheck == false)
//                {
//                    var update = await Task.Run(() => _context.SiteLocations.Where(x => x.Id == param.Id).FirstOrDefault());

//                    if (update != null)
//                    {

//                        AppHelper.TransferData_ClassA_to_ClassB<SiteLocation, SiteLocation>(param, ref update, new List<string>() { AppHelper.CreateBy, AppHelper.CreateDate });

//                        update.CreateBy = update.CreateBy;
//                        update.CreateDate = update.CreateDate;
//                        update.UpdateDate = DateTime.Now;

//                        #region Validation Zone

//                        Response validation = new Response();

//                        Func<Response>[] validationFunctions = new Func<Response>[]
//                        {

//                            () => SiteLocationValidation.Id(update.Id),
//                            () => SiteLocationValidation.ProviceName(update.ProvinceName),
//                            () => SiteLocationValidation.LocationName(update.LocationName),

//                            () => SiteLocationValidation.CreateDate(update.CreateDate),
//                            () => SiteLocationValidation.CreateBy(update.CreateBy),
//                            () => SiteLocationValidation.UpdateDate(update.UpdateDate),
//                            () => SiteLocationValidation.UpdateBy(update.UpdateBy),
//                            () => SiteLocationValidation.IsActive(update.IsActive),

//                            () => SiteLocationValidation.CreateByLenght(update.CreateBy.Length),
//                            () => SiteLocationValidation.UpdateByLenght(update.UpdateBy.Length),
//                                };

//                        foreach (var func in validationFunctions)
//                        {
//                            if (func() != null)
//                            {
//                                if (func().status == false)
//                                {
//                                    validation.httpCode = Constants.httpCode200;
//                                    validation.status = Constants.statusError;
//                                    validation.statusCode = Constants.statusCodeOK;
//                                    validation.message = func().message;

//                                    return validation;
//                                }
//                                else
//                                {
//                                    validation.status = true;
//                                }
//                            }
//                        }

//                        #endregion

//                        if (validation.status == true)
//                        {
//                            await _context.SaveChangesAsync();

//                            resp.httpCode = Constants.httpCode200;
//                            resp.status = Constants.statusSuccess;
//                            resp.statusCode = Constants.statusCodeOK;
//                            resp.data = param;
//                        }
//                        else
//                        {
//                            resp.httpCode = Constants.httpCode200;
//                            resp.status = Constants.statusError;
//                            resp.statusCode = Constants.statusCodeOK;
//                            resp.message = validation.message;
//                        }
//                    }
//                    else
//                    {
//                        resp.httpCode = Constants.httpCode400;
//                        resp.status = Constants.statusError;
//                        resp.statusCode = Constants.statusCodeDataNotFound;
//                        resp.message = Constants.recordDataNotFound;
//                    }
//                }
//                else
//                {
//                    resp.status = false;
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusError;
//                    resp.statusCode = Constants.statusCodeOK;
//                    resp.message = Constants.invalidDataDuplicate;

//                    return resp;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.httpCode = Constants.httpCode500;
//                resp.status = Constants.statusError;
//                resp.statusCode = Constants.statusCodeException;
//                resp.message = Constants.httpCode500Message;
//                resp.exception = ex.Message;

//                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
//            }
//            return resp;
//        }

//        public async Task<Response> Province() // Additional models are imported from GlobalFilter and PaginationModel
//        {
//            Response resp = new Response();

//            try
//            {

//                var queryable = await Task.Run(() => _context.SiteLocations
//                                        .GroupBy(gb => gb.ProvinceName)
//                                        .Select(group => group.OrderByDescending(x => x.SiteNetworkName)
//                                        .FirstOrDefault())
//                                        .AsQueryable());

//                #region Filter Data


//                List<SiteLocation> execute = new List<SiteLocation>();

//                execute = queryable.AsNoTracking().ToList();

//                #endregion

//                if (execute != null && execute.Count > 0)
//                {
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusSuccess;
//                    resp.statusCode = Constants.statusCodeOK;
//                    resp.data = execute.Select(x => x.ProvinceName).ToList();

//                    resp.effectRow = execute.Count();
//                }

//                else
//                {
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusError;
//                    resp.statusCode = Constants.statusCodeDataNotFound;
//                    resp.message = Constants.recordDataNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.httpCode = Constants.httpCode500;
//                resp.status = Constants.statusError;
//                resp.statusCode = Constants.statusCodeException;
//                resp.message = Constants.httpCode500Message;
//                resp.exception = ex.Message;

//                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
//            }

//            return resp;
//        }

//        public async Task<Response> Location(string provinceName) // Additional models are imported from GlobalFilter and PaginationModel
//        {
//            Response resp = new Response();

//            try
//            {

//                var queryable = await Task.Run(() => _context.SiteLocations
//                .Where(x => x.ProvinceName.ToLower() == provinceName.ToLower() && x.IsActive == true)
//                .OrderBy(x=>x.SiteNetworkSeq)
//                .AsQueryable());

//                #region Filter Data


//                List<SiteLocation> execute = new List<SiteLocation>();

//                execute = queryable.AsNoTracking().ToList();

//                #endregion

//                if (execute != null && execute.Count > 0)
//                {
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusSuccess;
//                    resp.statusCode = Constants.statusCodeOK;
//                    resp.data = execute;

//                    resp.effectRow = execute.Count();
//                }

//                else
//                {
//                    resp.httpCode = Constants.httpCode200;
//                    resp.status = Constants.statusError;
//                    resp.statusCode = Constants.statusCodeDataNotFound;
//                    resp.message = Constants.recordDataNotFound;
//                }
//            }
//            catch (Exception ex)
//            {
//                resp.httpCode = Constants.httpCode500;
//                resp.status = Constants.statusError;
//                resp.statusCode = Constants.statusCodeException;
//                resp.message = Constants.httpCode500Message;
//                resp.exception = ex.Message;

//                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
//            }

//            return resp;
//        }


//    }
//}

