using System;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs.Response;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Services.Helper;
using DOL.API.Services.Validation;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using WatchDog;

namespace DOL.API.Services
{
    public class JobOnsiteService
    {
        public JobOnsiteService()
        {
        }


        private readonly DolContext _context;

        public JobOnsiteService(DolContext context)
        {
            _context = context;
        }


        public async Task<Response> Get(JobOnsiteFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<JobOnsiteResponse> jobOnsiteResponses = new List<JobOnsiteResponse>();

            try
            {
                var queryable = await Task.Run(() => _context.JobOnsites.AsQueryable());

                #region Filter Data

                if (param.Id != null)
                {
                    queryable = queryable.Where(x => x.Id == param.Id).AsQueryable();
                }

                if (param.SiteInformationId != null)
                {
                    queryable = queryable.Where(x => x.SiteInformationId == param.SiteInformationId).AsQueryable();
                }


                if (param.SysUserId != null)
                {
                    queryable = queryable.Where(x => x.SysUserId == param.SysUserId).AsQueryable();
                }

                if (param.SysStatusId != null)
                {
                    queryable = queryable.Where(x => x.SysStatusId == param.SysStatusId).AsQueryable();
                }


                if (!string.IsNullOrEmpty(param.TypeOnsiteValue))
                {
                    queryable = queryable.Where(x => x.TypeOnsiteValue.ToLower().Contains(param.TypeOnsiteValue)).AsQueryable();
                }

                List<JobOnsite> execute = new List<JobOnsite>();

                execute = queryable.AsNoTracking().ToList();

                resp.effectRow = execute.Count();


                #region Tranform Master data


                if (execute != null && execute.Count > 0)
                {
                    var tempSiteInformation = await Task.Run(() => _context.SiteInformations.AsQueryable());
                    var tempSysStatus = await Task.Run(() => _context.SysStatuses.AsQueryable());
                    var tempSysUser = await Task.Run(() => _context.SysUsers.AsQueryable());

                    foreach (var item in execute)
                    {
                        JobOnsiteResponse jobOnsiteResponse = new JobOnsiteResponse();

                        AppHelper.TransferData_ClassA_to_ClassB<JobOnsite, JobOnsiteResponse>(item, ref jobOnsiteResponse, new List<string>());

                        jobOnsiteResponse.siteInformation = tempSiteInformation.Where(x => x.Id == item.SiteInformationId).FirstOrDefault();

                        jobOnsiteResponse.SysStatus = tempSysStatus.Where(x => x.Id == item.SysStatusId).FirstOrDefault();

                        jobOnsiteResponse.SysUser = tempSysUser.Where(x => x.Id == item.SysUserId).FirstOrDefault();

                        jobOnsiteResponses.Add(jobOnsiteResponse);

                    }
                }


                #endregion



                #endregion


                if (param.isAll != null)
                {
                    if (param.isAll == true)
                    {
                        jobOnsiteResponses = jobOnsiteResponses.ToList();
                    }
                    else
                    {
                        jobOnsiteResponses = jobOnsiteResponses
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize)
                       .ToList();
                    }
                }
                else
                {
                    jobOnsiteResponses = jobOnsiteResponses
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }

                if (jobOnsiteResponses != null && jobOnsiteResponses.Count > 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = jobOnsiteResponses;

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

            JobOnsiteResponse objData = new JobOnsiteResponse();

            try
            {
                var queryable = await Task.Run(() => _context.JobOnsites.Where(x => x.Id == id).AsQueryable());

                var execute = queryable.AsNoTracking().FirstOrDefault();

                if (execute != null)
                {
                    var tempSiteInformation = await Task.Run(() => _context.SiteInformations.AsQueryable());
                    var tempSysStatus = await Task.Run(() => _context.SysStatuses.AsQueryable());
                    var tempSysUser = await Task.Run(() => _context.SysUsers.AsQueryable());

                    AppHelper.TransferData_ClassA_to_ClassB<JobOnsite, JobOnsiteResponse>(execute, ref objData, new List<string>());

                    objData.siteInformation = tempSiteInformation.Where(x => x.Id == execute.SiteInformationId).FirstOrDefault();

                    objData.SysStatus = tempSysStatus.Where(x => x.Id == execute.SysStatusId).FirstOrDefault();

                    objData.SysUser = tempSysUser.Where(x => x.Id == execute.SysUserId).FirstOrDefault();

                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.effectRow = 1;
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

        public async Task<Response> Create(JobOnsite param)
        {
            Response resp = new Response();

            try
            {
                #region Model Setting

                param.Id = _context.JobOnsites.ToList().Count() > 0 ? _context.JobOnsites.Max(x => x.Id) + 1 : 0;

                param.AssignDate = ServiceHelper.gmtPlus7(param.AssignDate);
                param.TeamInstallDate = param.TeamInstallDate == null ? null : ServiceHelper.gmtPlus7(param.TeamInstallDate.Value);
                param.AcceptDate = param.AcceptDate == null ? null : ServiceHelper.gmtPlus7(param.AcceptDate.Value);

                Guid newGuid = Guid.NewGuid();
                string guidString = newGuid.ToString().Substring(0, 7);
                var currenrRunning = guidString;

                param.DocumentNo = $"OS-{DateTime.Now.ToString("yyyyMMdd")}-{currenrRunning}";

                param.TeamInstallContactName = string.Empty;
                param.TeamInstallContactTel = string.Empty;
                param.TeamInstallComment = string.Empty;
                param.TeamInstallDate = null;
                param.AcceptSign = string.Empty;
                param.AcceptBy = string.Empty;
                param.AcceptPosition = string.Empty;
                param.AcceptDate = null;

                #endregion

                #region Validation Zone

                Response validation = new Response();

                Func<Response>[] validationFunctions = new Func<Response>[]
                {
                    () => JobOnsiteValidation.Id(param.Id),
                    () => JobOnsiteValidation.SiteInformationId(param.SiteInformationId),
                    () => JobOnsiteValidation.SysUserId(param.SysUserId),
                    () => JobOnsiteValidation.SysStatusId(param.SysStatusId),
                    () => JobOnsiteValidation.AssignDate(param.AssignDate),
                    () => JobOnsiteValidation.TypeOnsiteValue(param.TypeOnsiteValue),


                    () => JobOnsiteValidation.DocumentNoLenght(param.DocumentNo.Length),
                    () => JobOnsiteValidation.TypeOnsiteValueLenght(param.TypeOnsiteValue.Length),
                    () => JobOnsiteValidation.AcceptByLenght(param.AcceptBy.Length),
                    () => JobOnsiteValidation.AcceptPositionLenght(param.AcceptPosition.Length),


                    () => JobOnsiteValidation.SiteInformationMaster(param.SiteInformationId,_context),
                    () => JobOnsiteValidation.SysStatusMaster(param.SysStatusId,_context),
                    () => JobOnsiteValidation.SysUserMaster(param.SysUserId,_context),


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
                    var DuplicationCheck = _context.JobOnsites.Where(x => x.SiteInformationId == param.SiteInformationId &&
                    x.SysUserId == param.SysUserId &&
                    x.TypeOnsiteValue.ToLower() == param.TypeOnsiteValue.ToLower()).Any();

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
                    await Task.Run(() => _context.JobOnsites.Add(param));

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

        public async Task<Response> Update(JobOnsite param)
        {
            Response resp = new Response();

            try
            {
                var DuplicationCheck = _context.JobOnsites.Where(x => x.SiteInformationId == param.SiteInformationId &&
                    x.SysUserId == param.SysUserId &&
                    x.TypeOnsiteValue.ToLower() == param.TypeOnsiteValue.ToLower()
                    && x.Id != param.Id).Any();

                if (DuplicationCheck == false)
                {
                    var update = await Task.Run(() => _context.JobOnsites.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (update != null)
                    {

                        List<string> ColumnNotUpdate = new List<string>();
                        ColumnNotUpdate.Add("Id");
                        ColumnNotUpdate.Add("DocumentNo");
                        ColumnNotUpdate.Add("SiteInformationId");
                        ColumnNotUpdate.Add("SysUserId");
                        ColumnNotUpdate.Add("AssignDate");
                        ColumnNotUpdate.Add("TypeOnsiteValue");

                        if (param.SysStatusId == 3)
                        {
                            param.TeamInstallDate = param.TeamInstallDate == null ? null : ServiceHelper.gmtPlus7(param.TeamInstallDate.Value);

                            ColumnNotUpdate.Add("AcceptSign");
                            ColumnNotUpdate.Add("AcceptBy");
                            ColumnNotUpdate.Add("AcceptPosition");
                            ColumnNotUpdate.Add("AcceptDate");

                            if (update.SysStatusId == 4)
                            {
                                resp.httpCode = Constants.httpCode200;
                                resp.status = Constants.statusError;
                                resp.statusCode = Constants.statusCodeOK;
                                resp.message = "ขอภัยค่ะ สถานะเอกสารรอการตรวจรับ ไม่สามารถทำรายการซ้ำได้.";

                                return resp;
                            }
                        }

                        if (param.SysStatusId == 4)
                        {
                            param.AcceptDate = param.AcceptDate == null ? null : ServiceHelper.gmtPlus7(param.AcceptDate.Value);

                            if (string.IsNullOrEmpty(param.TeamInstallContactName) ||
                                string.IsNullOrEmpty(param.TeamInstallContactTel))
                            {
                                resp.httpCode = Constants.httpCode200;
                                resp.status = Constants.statusError;
                                resp.statusCode = Constants.statusCodeOK;
                                resp.message = "กรุณารอทีมติดตั้งดำเนินการแล้วเสร็จ จึงจะสามารถตรวจรับงานได้ ขอบคุณค่ะ";

                                return resp;
                            }

                            ColumnNotUpdate.Add("TeamInstallContactName");
                            ColumnNotUpdate.Add("TeamInstallContactTel");
                            ColumnNotUpdate.Add("TeamInstallComment");
                            ColumnNotUpdate.Add("TeamInstallDate");
                        }

                        if (param.SysStatusId != 3 && param.SysStatusId != 4)
                        {
                            resp.httpCode = Constants.httpCode200;
                            resp.status = Constants.statusError;
                            resp.statusCode = Constants.statusCodeOK;
                            resp.message = "สถานะเอกสารใบงานไม่ถูกต้อง กรุณาตรวจสอบใหม่อีกครั้ง";

                            return resp;
                        }


                        AppHelper.TransferData_ClassA_to_ClassB<JobOnsite, JobOnsite>(param, ref update, ColumnNotUpdate);

                        #region Validation Zone

                        Response validation = new Response();

                        Func<Response>[] validationFunctions = new Func<Response>[]
                        {
                            () => JobOnsiteValidation.Id(update.Id),
                            () => JobOnsiteValidation.SiteInformationId(update.SiteInformationId),
                            () => JobOnsiteValidation.SysUserId(update.SysUserId),
                            () => JobOnsiteValidation.SysStatusId(update.SysStatusId),
                            () => JobOnsiteValidation.AssignDate(update.AssignDate),
                            () => JobOnsiteValidation.TypeOnsiteValue(update.TypeOnsiteValue),


                            () => JobOnsiteValidation.DocumentNoLenght(update.DocumentNo.Length),
                            () => JobOnsiteValidation.TypeOnsiteValueLenght(update.TypeOnsiteValue.Length),
                            () => JobOnsiteValidation.TeamInstallContactNameLenght(update.TeamInstallContactName.Length),
                            () => JobOnsiteValidation.TeamInstallContactTelLenght(update.TeamInstallContactTel.Length),
                            () => JobOnsiteValidation.TeamInstallCommentLenght(update.TeamInstallComment.Length),
                            () => JobOnsiteValidation.AcceptByLenght(update.AcceptBy.Length),
                            () => JobOnsiteValidation.AcceptPositionLenght(update.AcceptPosition.Length),


                            () => JobOnsiteValidation.SiteInformationMaster(update.SiteInformationId,_context),
                            () => JobOnsiteValidation.SysStatusMaster(update.SysStatusId,_context),
                            () => JobOnsiteValidation.SysUserMaster(update.SysUserId,_context),
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

                            param = await Task.Run(() => _context.JobOnsites.Where(x => x.Id == param.Id).FirstOrDefault());

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

