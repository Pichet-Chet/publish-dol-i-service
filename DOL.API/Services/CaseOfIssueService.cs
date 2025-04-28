using System;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace DOL.API.Services
{
    public class CaseOfIssueService
    {
        private readonly DolContext _context;

        public CaseOfIssueService(DolContext context)
        {
            _context = context;
        }

        public async Task<Response> Get(CaseOfIssueFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            try
            {
                var queryable = await Task.Run(() => _context.CaseOfIssues.AsQueryable());

                #region Filter Data

                if (param.Id != null)
                {
                    queryable = queryable.Where(x => x.Id == param.Id).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.NameTh))
                {
                    queryable = queryable.Where(x => x.NameTh.ToLower().Contains(param.NameTh.ToLower())).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.NameEn))
                {
                    queryable = queryable.Where(x => x.NameEn.ToLower().Contains(param.NameEn.ToLower())).AsQueryable();
                }

                if (param.IsActive != null)
                {
                    queryable = queryable.Where(x => x.IsActive == param.IsActive).AsQueryable();
                }

                List<CaseOfIssue> execute = new List<CaseOfIssue>();

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


        public async Task<Response> Dropdown() // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<Dropdown> dropdowns = new List<Dropdown>();

            try
            {
                var queryable = await Task.Run(() => _context.CaseOfIssues.AsQueryable());

                #region Filter Data

                queryable = queryable.Where(x => x.IsActive == true).AsQueryable();

                List<CaseOfIssue> execute = new List<CaseOfIssue>();

                execute = queryable.AsNoTracking().ToList();

                resp.effectRow = execute.Count();

                if (execute != null && execute.Count > 0)
                {
                    foreach (var item in execute)
                    {
                        Dropdown dropdown = new Dropdown();

                        dropdown.value = Convert.ToString(item.Id);
                        dropdown.data = item.NameTh;

                        dropdowns.Add(dropdown);
                    }
                }

                #endregion

                if (execute != null && execute.Count > 0)
                {
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

    }
}

