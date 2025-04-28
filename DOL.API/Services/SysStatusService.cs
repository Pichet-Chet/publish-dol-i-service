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
	public class SysStatusService
    {
        private readonly DolContext _context;

        public SysStatusService(DolContext context)
        {
            _context = context;
        }


        public async Task<Response> Get(SysStatusFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            try
            {
                var queryable = await Task.Run(() => _context.SysStatuses.AsQueryable());

                #region Sorting

                queryable = SysStatusFilter.ApplySorting(queryable, param.SortName, param.SortType);

                #endregion

                #region Filter Data

                if (param.Id != null)
                {
                    queryable = queryable.Where(x => x.Id == param.Id).AsQueryable();
                }

                if (!string.IsNullOrWhiteSpace(param.TextSearch))
                {
                    queryable = queryable.Where(x =>
                    x.NameTh.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.NameTh.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.Category.ToLower().Contains(param.TextSearch.ToLower())
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.NameTh))
                {
                    queryable = queryable.Where(x => x.NameTh.ToLower().Contains(param.NameTh.ToLower())).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.NameEn))
                {
                    queryable = queryable.Where(x => x.NameEn.ToLower().Contains(param.NameEn.ToLower())).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Category))
                {
                    queryable = queryable.Where(x => x.Category.ToLower().Contains(param.Category.ToLower())).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                List<SysStatus> execute = new List<SysStatus>();

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

            SysStatus objData = new SysStatus();

            try
            {
                var queryable = await Task.Run(() => _context.SysStatuses.Where(x => x.Id == id).AsQueryable());

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

        public async Task<Response> Create(SysStatus param)
        {
            Response resp = new Response();

            try
            {
                #region Model Setting

                param.Id = _context.SysStatuses.ToList().Count() > 0 ? _context.SysStatuses.Max(x => x.Id) + 1 : 0;

                param.CreateDate = DateTime.Now;
                param.UpdateDate = DateTime.Now;

                #endregion

                #region Validation Zone

                Response validation = new Response();

                Func<Response>[] validationFunctions = new Func<Response>[]
                {
                    () => SysStatusValidation.Id(param.Id),
                    () => SysStatusValidation.NameTh(param.NameTh),
                    () => SysStatusValidation.NameEn(param.NameEn),
                    () => SysStatusValidation.Category(param.Category),

                    () => SysStatusValidation.CreateDate(param.CreateDate),
                    () => SysStatusValidation.CreateBy(param.CreateBy),
                    () => SysStatusValidation.UpdateDate(param.UpdateDate),
                    () => SysStatusValidation.UpdateBy(param.UpdateBy),
                    () => SysStatusValidation.IsActive(param.IsActive),

                    () => SysStatusValidation.CategoryLenght(param.CreateBy.Length),
                    () => SysStatusValidation.CreateByLenght(param.CreateBy.Length),
                    () => SysStatusValidation.UpdateByLenght(param.UpdateBy.Length),

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
                    var DuplicationCheck = _context.SysStatuses.Where(x => x.NameTh.ToLower() == param.NameTh.ToLower() && x.NameEn == param.NameEn && x.Category == param.Category).Any();

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
                    await Task.Run(() => _context.SysStatuses.Add(param));

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

        public async Task<Response> Update(SysStatus param)
        {
            Response resp = new Response();

            try
            {
                var DuplicationCheck = _context.SysStatuses.Where(x
                    => x.NameTh.ToLower() == param.NameTh.ToLower() &&
                    x.NameEn.ToLower() == param.NameEn.ToLower() &&
                    x.Category.ToLower() == param.Category.ToLower()
                    && x.Id != param.Id).Any();

                if (DuplicationCheck == false)
                {
                    var update = await Task.Run(() => _context.SysStatuses.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (update != null)
                    {

                        AppHelper.TransferData_ClassA_to_ClassB<SysStatus, SysStatus>(param, ref update, new List<string>() { AppHelper.CreateBy, AppHelper.CreateDate });

                        update.CreateBy = update.CreateBy;
                        update.CreateDate = update.CreateDate;
                        update.UpdateDate = DateTime.Now;

                        #region Validation Zone

                        Response validation = new Response();

                        Func<Response>[] validationFunctions = new Func<Response>[]
                        {

                            () => SysStatusValidation.Id(update.Id),
                            () => SysStatusValidation.NameTh(update.NameTh),
                            () => SysStatusValidation.NameEn(update.NameEn),
                            () => SysStatusValidation.Category(update.Category),

                            () => SysStatusValidation.CreateDate(update.CreateDate),
                            () => SysStatusValidation.CreateBy(update.CreateBy),
                            () => SysStatusValidation.UpdateDate(update.UpdateDate),
                            () => SysStatusValidation.UpdateBy(update.UpdateBy),
                            () => SysStatusValidation.IsActive(update.IsActive),

                            () => SysStatusValidation.CategoryLenght(update.CreateBy.Length),
                            () => SysStatusValidation.CreateByLenght(update.CreateBy.Length),
                            () => SysStatusValidation.UpdateByLenght(update.UpdateBy.Length),
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
    }
}

