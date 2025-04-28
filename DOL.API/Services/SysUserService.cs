using System;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Services.Helper;
using DOL.API.Services.Validation;
using Microsoft.EntityFrameworkCore;
using WatchDog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;

namespace DOL.API.Services
{
    public class SysUserService
    {
        private readonly DolContext _context;

        public SysUserService(DolContext context)
        {
            _context = context;
        }


        public async Task<Response> Get(SysUserFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            try
            {
                var queryable = await Task.Run(() => _context.SysUsers.AsQueryable());

                #region Sorting

                queryable = SysUserFilter.ApplySorting(queryable, param.SortName, param.SortType);

                #endregion

                #region Filter Data

                if (param.Id != null)
                {
                    queryable = queryable.Where(x => x.Id == param.Id).AsQueryable();
                }

                if (!string.IsNullOrWhiteSpace(param.TextSearch))
                {
                    queryable = queryable.Where(x =>
                    x.Username.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.Name.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.UserGroup.ToLower().Contains(param.TextSearch.ToLower())
                    ).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Username))
                {
                    queryable = queryable.Where(x => x.Username.ToLower().Contains(param.Username.ToLower())).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Name))
                {
                    queryable = queryable.Where(x => x.Name.ToLower().Contains(param.Name.ToLower())).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.UserGroup))
                {
                    queryable = queryable.Where(x => x.UserGroup.ToLower().Contains(param.UserGroup.ToLower())).AsQueryable();
                }

                List<SysUser> execute = new List<SysUser>();

                execute = queryable.AsNoTracking().ToList();

                resp.effectRow = execute.Count();

                if (execute.Count > 0)
                {
                    foreach (var item in execute)
                    {
                        item.Password = null;
                    }
                }

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
            SysUser objData = new SysUser();

            try
            {
                var queryable = await Task.Run(() => _context.SysUsers.Where(x => x.Id == id).AsQueryable());

                var execute = queryable.AsNoTracking().FirstOrDefault();

                if (execute != null)
                {
                    objData = execute;

                    objData.Password = null;

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

        public async Task<Response> Create(SysUser param)
        {
            Response resp = new Response();

            try
            {
                #region Model Setting

                param.Id = _context.SysUsers.ToList().Count() > 0 ? _context.SysUsers.Max(x => x.Id) + 1 : 0;

                param.CreateDate = DateTime.Now;
                param.UpdateDate = DateTime.Now;

                #endregion

                #region Validation Zone

                Response validation = new Response();

                Func<Response>[] validationFunctions = new Func<Response>[]
                {
                    () => SysUserValidation.Id(param.Id),
                    () => SysUserValidation.Username(param.Username),
                    () => SysUserValidation.Password(param.Password),
                    () => SysUserValidation.UserGroup(param.UserGroup),

                    () => SysUserValidation.CreateDate(param.CreateDate),
                    () => SysUserValidation.CreateBy(param.CreateBy),
                    () => SysUserValidation.UpdateDate(param.UpdateDate),
                    () => SysUserValidation.UpdateBy(param.UpdateBy),
                    () => SysUserValidation.IsActive(param.IsActive),

                    () => SysUserValidation.UsernameLenght(param.Username.Length),
                    () => SysUserValidation.PasswordLenght(param.Password.Length),
                    () => SysUserValidation.CreateByLenght(param.CreateBy.Length),
                    () => SysUserValidation.UpdateByLenght(param.UpdateBy.Length),

                    () => SysUserValidation.UserGroupMaster(param.UserGroup,_context),

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
                    var DuplicationCheck = _context.SysUsers.Where(x => x.Username.ToLower() == param.Username.ToLower()).Any();

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
                    await Task.Run(() => _context.SysUsers.Add(param));

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

        public async Task<Response> Update(SysUser param)
        {
            Response resp = new Response();

            try
            {
                var DuplicationCheck = _context.SysUsers.Where(x
                    => x.Username.ToLower() == param.Username.ToLower()
                    && x.Id != param.Id).Any();

                if (DuplicationCheck == false)
                {
                    var update = await Task.Run(() => _context.SysUsers.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (update != null)
                    {
                        List<string> columnNoUpdate = new List<string>();

                        columnNoUpdate.Add("CreateBy");
                        columnNoUpdate.Add("CreateDate");

                        if (string.IsNullOrEmpty(param.Password))
                        {
                            columnNoUpdate.Add("Password");
                        }


                        AppHelper.TransferData_ClassA_to_ClassB<SysUser, SysUser>(param, ref update, columnNoUpdate);

                        update.CreateBy = update.CreateBy;
                        update.CreateDate = update.CreateDate;
                        update.Username = update.Username;
                        update.UpdateDate = DateTime.Now;

                        if (string.IsNullOrEmpty(param.Password))
                        {
                            update.Password = update.Password;
                        }
                        else
                        {
                            update.Password = param.Password;
                        }



                        #region Validation Zone

                        Response validation = new Response();

                        Func<Response>[] validationFunctions = new Func<Response>[]
                        {

                            () => SysUserValidation.Id(update.Id),
                            () => SysUserValidation.Username(update.Username),
                            () => SysUserValidation.Password(update.Password),
                            () => SysUserValidation.UserGroup(update.UserGroup),
                            () => SysUserValidation.CreateDate(param.CreateDate),
                            () => SysUserValidation.CreateBy(param.CreateBy),
                            () => SysUserValidation.UpdateDate(update.UpdateDate),
                            () => SysUserValidation.UpdateBy(update.UpdateBy),
                            () => SysUserValidation.IsActive(update.IsActive),

                            () => SysUserValidation.UsernameLenght(update.Username.Length),
                            () => SysUserValidation.PasswordLenght(update.Password.Length),
                            () => SysUserValidation.CreateByLenght(update.CreateBy.Length),
                            () => SysUserValidation.UpdateByLenght(update.UpdateBy.Length),

                            () => SysUserValidation.UserGroupMaster(update.UserGroup,_context),
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

