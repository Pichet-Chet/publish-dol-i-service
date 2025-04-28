using System;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs.View;
using DOL.API.Models.Customs.Request;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Services.Validation;
using Microsoft.EntityFrameworkCore;
using WatchDog;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Net.Http.Headers;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Hosting.Internal;
using SharpCompress.Common;
using MongoDB.Driver.Linq;
using DOL.API.Models.Customs.Response;
using DOL.API.Services.Helper;
using DocumentFormat.OpenXml.Office2010.Excel;

namespace DOL.API.Services
{
    public class JobRepairService
    {
        private readonly DolContext _context;

        public JobRepairService(DolContext context)
        {
            _context = context;
        }

        public async Task<Response> Get(JobRepairFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<JobRepairResponse> jobRepairViews = new List<JobRepairResponse>();

            string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiAppRootImageUrl"];

            try
            {
                var queryable = await Task.Run(() => _context.JobRepairs.AsQueryable());

                var tempSiteInformation = await Task.Run(() => _context.SiteInformations.AsQueryable());
                var tempSiteNetworks = await Task.Run(() => _context.SiteNetworks.AsQueryable());
                var tempSysStatuses = await Task.Run(() => _context.SysStatuses.AsQueryable());
                var tempSysUsers = await Task.Run(() => _context.SysUsers.AsQueryable());

                var tempCaseOfFixeds = await Task.Run(() => _context.CaseOfFixeds.AsQueryable());
                var tempCaseOfIssues = await Task.Run(() => _context.CaseOfIssues.AsQueryable());
                var tempCaseOfIssueSubs = await Task.Run(() => _context.CaseOfIssueSubs.AsQueryable());

                #region Filter Data

                if (param.NetworkId != null)
                {
                    queryable = queryable.Where(x => x.SiteNetworkId == param.NetworkId).AsQueryable();
                }

                if (param.StatusId != null)
                {
                    queryable = queryable.Where(x => x.SysStatusId == param.StatusId).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.JobType))
                {
                    queryable = queryable.Where(x => x.TypeRepairValue.ToLower() == param.JobType.ToLower()).AsQueryable();
                }

                if (param.JobCreateDate != null)
                {
                    queryable = queryable.Where(x => x.JobCreatedDate.Value.Date == param.JobCreateDate.Value.Date).AsQueryable();
                }

                if (param.JobCreateDateFrom != null)
                {
                    queryable = queryable.Where(x => x.JobCreatedDate.Value.Date >= param.JobCreateDateFrom.Value.Date).AsQueryable();
                }

                if (param.JobCreateDateTo != null)
                {
                    queryable = queryable.Where(x => x.JobCreatedDate.Value.Date <= param.JobCreateDateTo.Value.Date).AsQueryable();
                }

                List<JobRepair> execute = new List<JobRepair>();

                execute = queryable.AsNoTracking().ToList();

                execute = execute.OrderByDescending(x => x.Id).ToList();

                resp.effectRow = execute.Count();

                #endregion


                #region Card Statistic


                if (execute.Count <= 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeDataNotFound;
                    resp.message = Constants.recordDataNotFound;

                    resp.effectRow = null;

                    return resp;
                }

                List<JobRepair> executeAll = new List<JobRepair>();

                executeAll = await Task.Run(() => _context.JobRepairs.ToList());

                var setAcceptJobToday = executeAll.Where(x => x.SysStatusId == 5 && x.JobAcceptDate.Value.Date == DateTime.Now.Date).Count();
                var setOnprocessJobToday = executeAll.Where(x => x.SysStatusId == 6 && x.JobAcceptDate.Value.Date == DateTime.Now.Date).Count();
                var setSuccesJobTody = executeAll.Where(x => x.SysStatusId == 7 && x.JobAcceptDate.Value.Date == DateTime.Now.Date).Count();

                var setAcceptJobAll = executeAll.Where(x => x.SysStatusId == 5).Count();
                var setOnprocessJobAll = executeAll.Where(x => x.SysStatusId == 6).Count();
                var setSuccesJobAll = executeAll.Where(x => x.SysStatusId == 7).Count();

                var setJobAll = executeAll.Count();
                double setAcceptJobAllPercent = 0;
                double setOnprocessJobAllPercent = 0;
                double setSuccesJobAllPercent = 0;


                if (setJobAll > 0)
                {

                    if (setAcceptJobAll > 0)
                    {
                        var a = Convert.ToDouble(setAcceptJobAll);
                        var b = setJobAll;
                        var c = a / b;

                        setAcceptJobAllPercent = (c * 100);

                        setAcceptJobAllPercent = Math.Round(setAcceptJobAllPercent, 2);

                    }

                    if (setOnprocessJobAll > 0)
                    {
                        var a = Convert.ToDouble(setOnprocessJobAll);
                        var b = setJobAll;
                        var c = a / b;

                        setOnprocessJobAllPercent = c * 100;

                        setOnprocessJobAllPercent = Math.Round(setOnprocessJobAllPercent, 2);

                    }


                    if (setSuccesJobAll > 0)
                    {
                        var a = Convert.ToDouble(setSuccesJobAll);
                        var b = setJobAll;
                        var c = a / b;

                        setSuccesJobAllPercent = c * 100;

                        setSuccesJobAllPercent = Math.Round(setSuccesJobAllPercent, 2);

                    }

                }

                #endregion


                #region Tranform Data
                int CountOutOffSla = 0;

                foreach (var item in execute)
                {
                    JobRepairResponse jobRepairView = new JobRepairResponse();




                    jobRepairView.sumJobRepair.acceptJobToday = setAcceptJobToday;
                    jobRepairView.sumJobRepair.onProcessJobToday = setOnprocessJobToday;
                    jobRepairView.sumJobRepair.succesJobToday = setSuccesJobTody;

                    jobRepairView.sumJobRepair.JobAll = setJobAll;
                    jobRepairView.sumJobRepair.acceptJobAll = setAcceptJobAll;
                    jobRepairView.sumJobRepair.onProcessJobAll = setOnprocessJobAll;
                    jobRepairView.sumJobRepair.succesJobAll = setSuccesJobAll;

                    jobRepairView.sumJobRepair.acceptJobPercent = setAcceptJobAllPercent;
                    jobRepairView.sumJobRepair.onpProcessJobPercent = setOnprocessJobAllPercent;
                    jobRepairView.sumJobRepair.succesJobPercent = setSuccesJobAllPercent;


                    AppHelper.TransferData_ClassA_to_ClassB<JobRepair, JobRepairResponse>(item, ref jobRepairView, new List<string>());

                    jobRepairView.siteInformation = tempSiteInformation.Where(x => x.Id == item.SiteInformationId).FirstOrDefault();
                    jobRepairView.siteNetwork = tempSiteNetworks.Where(x => x.Id == item.SiteNetworkId).FirstOrDefault();
                    jobRepairView.SysStatus = tempSysStatuses.Where(x => x.Id == item.SysStatusId).FirstOrDefault();

                    jobRepairView.caseOfFixed = item.CaseOfFixId != null ? tempCaseOfFixeds.Where(x => x.Id == item.CaseOfFixId).FirstOrDefault() : null;
                    jobRepairView.caseOfIssue = item.CaseOfIssueId != null ? tempCaseOfIssues.Where(x => x.Id == item.CaseOfIssueId).FirstOrDefault() : null;
                    jobRepairView.caseOfIssueSub = item.CaseOfIssueSubId != null ? tempCaseOfIssueSubs.Where(x => x.Id == item.CaseOfIssueSubId).FirstOrDefault() : null;


                    jobRepairView.JobImage1 = jobRepairView.JobImage1 == null ? null : folderUploadFile + jobRepairView.JobImage1;
                    jobRepairView.JobImage2 = jobRepairView.JobImage2 == null ? null : folderUploadFile + jobRepairView.JobImage2;
                    jobRepairView.JobImage3 = jobRepairView.JobImage3 == null ? null : folderUploadFile + jobRepairView.JobImage3;
                    jobRepairView.JobImage4 = jobRepairView.JobImage4 == null ? null : folderUploadFile + jobRepairView.JobImage4;


                    jobRepairView.LocationName = jobRepairView.siteInformation.LocationName;
                    jobRepairView.ProvinceName = jobRepairView.siteInformation.ProvinceName;
                    jobRepairView.SiteNetworkName = jobRepairView.siteNetwork.Name;
                    jobRepairView.SiteNetworkSeq = jobRepairView.siteInformation.SiteNetworkSeq;


                    jobRepairView.jobCreatedByName = tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobCreatedBy).FirstOrDefault() == null ? "" : tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobCreatedBy).Select(x => x.Name).FirstOrDefault();
                    jobRepairView.jobAcceptByName = tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobAcceptBy).FirstOrDefault() == null ? "" : tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobAcceptBy).Select(x => x.Name).FirstOrDefault();
                    jobRepairView.jobProcessByName = tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobProcessBy).FirstOrDefault() == null ? "" : tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobProcessBy).Select(x => x.Name).FirstOrDefault();
                    jobRepairView.jobCompleteByName = tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobCompleteBy).FirstOrDefault() == null ? "" : tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobCompleteBy).Select(x => x.Name).FirstOrDefault();


                    //public string? LocationName { get; set; }
                    //public string? ProvinceName { get; set; }
                    //public string? SiteNetworkName { get; set; }
                    //public int? SiteNetworkSeq { get; set; }

                    double? setHour = null;

                    if (jobRepairView.TypeRepairValue == Constants.jobWan1)
                    {
                        setHour = jobRepairView.siteInformation == null ? 0 : jobRepairView.siteInformation.MainMaxHour == null ? 0 : jobRepairView.siteInformation.MainMaxHour;
                    }

                    if (jobRepairView.TypeRepairValue == Constants.jobWan2)
                    {
                        setHour = jobRepairView.siteInformation == null ? 0 : jobRepairView.siteInformation.SecondaryMaxHour == null ? 0 : jobRepairView.siteInformation.SecondaryMaxHour;
                    }

                    if (jobRepairView.TypeRepairValue == Constants.jobInternet)
                    {
                        setHour = jobRepairView.siteInformation == null ? 0 : jobRepairView.siteInformation.InternetMaxHour == null ? 0 : jobRepairView.siteInformation.InternetMaxHour;
                    }

                    if (jobRepairView.TypeRepairValue == Constants.jobCorpnet)
                    {
                        setHour = jobRepairView.siteInformation == null ? 0 : jobRepairView.siteInformation.CorpnetMaxHour == null ? 0 : jobRepairView.siteInformation.CorpnetMaxHour;
                    }

                    if (jobRepairView.TypeRepairValue == Constants.jobCellular)
                    {
                        setHour = jobRepairView.siteInformation == null ? 0 : jobRepairView.siteInformation.CellularMaxHour == null ? 0 : jobRepairView.siteInformation.CellularMaxHour;
                    }


                    var LimitTime = item.JobCreatedDate.Value.AddHours((double)setHour);

                    TimeSpan remainingTime = new TimeSpan();

                    if (item.SysStatusId == 7)
                    {
                        if (item.JobCompleteDate != null)
                        {
                            remainingTime = LimitTime - item.JobCompleteDate.Value;
                        }
                        else
                        {
                            remainingTime = new TimeSpan();
                        }
                    }
                    else
                    {
                        remainingTime = LimitTime - DateTime.Now;
                    }



                    if (remainingTime.Seconds > 0)
                    {
                        jobRepairView.OutOfSla = false;

                        jobRepairView.RemainingTime = $"{(int)remainingTime.TotalHours:D2}:{remainingTime.Minutes:D2}";

                    }
                    else
                    {
                        jobRepairView.OutOfSla = true;

                        if (item.SysStatusId != 7)
                        {
                            CountOutOffSla++;
                        }

                        var textMinute = $"{remainingTime.Minutes:D2}";

                        jobRepairView.RemainingTime = $"{(int)remainingTime.TotalHours:D2}:" + textMinute.Substring(1);

                    }

                    jobRepairViews.Add(jobRepairView);
                }

                jobRepairViews.ForEach(item => item.sumJobRepair.outOfSlaAll = CountOutOffSla);

                double slaAll = Convert.ToDouble(jobRepairViews.FirstOrDefault().sumJobRepair.outOfSlaAll);
                var jobAll = setJobAll;
                var result = slaAll / jobAll;
                var xxx = result * 100;
                xxx = Math.Round(xxx, 2);


                jobRepairViews.ForEach(item => item.sumJobRepair.outOfSlaPercent = xxx);


                #endregion

                if (param.OutOfSla == true)
                {
                    if (jobRepairViews != null && jobRepairViews.Count > 0)
                    {
                        jobRepairViews = jobRepairViews.Where(x => x.OutOfSla == true).ToList();

                        resp.effectRow = jobRepairViews.Count();
                    }
                }

                if (param.OutOfSla == false)
                {
                    if (jobRepairViews != null && jobRepairViews.Count > 0)
                    {
                        jobRepairViews = jobRepairViews.Where(x => x.OutOfSla == false).ToList();

                        resp.effectRow = jobRepairViews.Count();
                    }
                }

                if (!string.IsNullOrWhiteSpace(param.TextSearch))
                {
                    if (jobRepairViews != null && jobRepairViews.Count > 0)
                    {
                        jobRepairViews = jobRepairViews.Where(x =>
                        x.siteInformation.LocationName.ToLower().Contains(param.TextSearch) ||
                        x.siteInformation.ProvinceName.ToLower().Contains(param.TextSearch) ||
                        x.siteNetwork.Name.ToLower().Contains(param.TextSearch)
                        )
                            .ToList();

                        resp.effectRow = jobRepairViews.Count();
                    }
                }

                #region Sorting

                IQueryable<JobRepairResponse> myQueryable = jobRepairViews.AsQueryable();

                myQueryable = JobRepairResponse.ApplySorting(myQueryable, param.SortName, param.SortType);

                var output = myQueryable.ToList();

                #endregion

                if (param.isAll != null)
                {
                    if (param.isAll == true)
                    {
                        output = output.ToList();
                    }
                    else
                    {
                        output = output
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize)
                       .ToList();
                    }
                }
                else
                {
                    output = output
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }


                if (output != null && output.Count > 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = output;

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
                resp.message = ex.Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return resp;
        }


        public async Task<Response> Detail(int id) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<JobRepairResponse> jobRepairViews = new List<JobRepairResponse>();

            string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiAppRootImageUrl"];


            try
            {
                var queryable = await Task.Run(() => _context.JobRepairs.Where(x => x.Id == id).AsQueryable());

                var tempSiteInformation = await Task.Run(() => _context.SiteInformations.AsQueryable());
                var tempSiteNetworks = await Task.Run(() => _context.SiteNetworks.AsQueryable());
                var tempSysStatuses = await Task.Run(() => _context.SysStatuses.AsQueryable());
                var tempSysUsers = await Task.Run(() => _context.SysUsers.AsQueryable());

                #region Filter Data

                JobRepair execute = new JobRepair();

                execute = queryable.AsNoTracking().FirstOrDefault();

                if (execute != null)
                {
                    JobRepairResponse jobRepairView = new JobRepairResponse();

                    AppHelper.TransferData_ClassA_to_ClassB<JobRepair, JobRepairResponse>(execute, ref jobRepairView, new List<string>());

                    jobRepairView.siteInformation = tempSiteInformation.Where(x => x.Id == execute.SiteInformationId).FirstOrDefault();

                    jobRepairView.siteNetwork = tempSiteNetworks.Where(x => x.Id == execute.SiteNetworkId).FirstOrDefault();

                    jobRepairView.SysStatus = tempSysStatuses.Where(x => x.Id == execute.SysStatusId).FirstOrDefault();

                    jobRepairView.JobImage1 = jobRepairView.JobImage1 == null ? null : folderUploadFile + jobRepairView.JobImage1;
                    jobRepairView.JobImage2 = jobRepairView.JobImage2 == null ? null : folderUploadFile + jobRepairView.JobImage2;
                    jobRepairView.JobImage3 = jobRepairView.JobImage3 == null ? null : folderUploadFile + jobRepairView.JobImage3;
                    jobRepairView.JobImage4 = jobRepairView.JobImage4 == null ? null : folderUploadFile + jobRepairView.JobImage4;

                    jobRepairView.jobCreatedByName = tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobCreatedBy).FirstOrDefault() == null ? "" : tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobCreatedBy).Select(x => x.Name).FirstOrDefault();
                    jobRepairView.jobAcceptByName = tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobAcceptBy).FirstOrDefault() == null ? "" : tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobAcceptBy).Select(x => x.Name).FirstOrDefault();
                    jobRepairView.jobProcessByName = tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobProcessBy).FirstOrDefault() == null ? "" : tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobProcessBy).Select(x => x.Name).FirstOrDefault();
                    jobRepairView.jobCompleteByName = tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobCompleteBy).FirstOrDefault() == null ? "" : tempSysUsers.Where(x => x.Username.ToLower() == jobRepairView.JobCompleteBy).Select(x => x.Name).FirstOrDefault();


                    jobRepairViews.Add(jobRepairView);
                }
                else
                {
                    jobRepairViews = null;
                }



                #endregion

                if (jobRepairViews != null)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = jobRepairViews;

                    resp.effectRow = 1;
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
                resp.message = ex.Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return resp;
        }


        public async Task<Response> Create(JobRepair param, IFormFile? jobImage1, IFormFile? jobImage2, IFormFile? jobImage3, IFormFile? jobImage4)
        {
            Response resp = new Response();

            try
            {
                #region Model Setting

                Guid newGuid = Guid.NewGuid();

                string guidString = newGuid.ToString().Substring(0, 7);

                param.Id = _context.JobRepairs.ToList().Count() > 0 ? _context.JobRepairs.Max(x => x.Id) + 1 : 0;

                param.JobAcceptBy = param.JobCreatedBy;


                var currenrRunning = guidString;

                param.DocumentNo = $"RP-{DateTime.Now.ToString("yyyyMMdd")}-{currenrRunning}";

                param.JobCreatedDate = DateTime.Now;

                param.JobAcceptDate = param.JobCreatedDate;


                string uploads = Directory.GetCurrentDirectory();

                string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["FolderUploadFile"];

                string? folderReadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["FolderReadFile"];


                string pathUrl = System.IO.Path.Combine($"{uploads}/{folderUploadFile}/");

                //string subFolder = $"{DateTime.Now.ToString("yyyy-MM")}";

                string physicalPath = $"{pathUrl}/{currenrRunning}/";

                #endregion

                #region Validation Zone

                Response validation = new Response();

                Func<Response>[] validationFunctions = new Func<Response>[]
                {
                    () => JobRepairValidation.Id(param.Id),
                    () => JobRepairValidation.SiteNetworkId(param.SiteNetworkId),
                    () => JobRepairValidation.SiteInformationId(param.SiteInformationId),

                    () => JobRepairValidation.JobDescription(param.JobDescription),
                    //() => JobRepairValidation.JobContactName(param.JobContactName),
                    //() => JobRepairValidation.JobContactTel(param.JobContactTel),

                    //() => JobRepairValidation.JobSenderContactName(param.JobSenderContactName),
                    //() => JobRepairValidation.JobSenderContactTel(param.JobSenderContactTel),
                    //() => JobRepairValidation.JobSenderRemark(param.JobSenderRemark),

                    //() => JobRepairValidation.JobFixedDescription(param.JobFixedDescription),
                    //() => JobRepairValidation.JobFixedComment(param.JobFixedComment),
                    //() => JobRepairValidation.JobFixedContactName(param.JobFixedContactName),
                    //() => JobRepairValidation.JobFixedContactTel(param.JobFixedContactTel),

                    () => JobRepairValidation.SysStatusId(param.SysStatusId),
                    () => JobRepairValidation.JobCreatedBy(param.JobCreatedBy),
                    () => JobRepairValidation.JobCreatedDate((DateTime)param.JobCreatedDate),


                    () => param.DocumentNo != null ? JobRepairValidation.DocumentNoLenght(param.DocumentNo.Length) : null,
                    () => param.JobDescription != null ? JobRepairValidation.JobDescriptionLenght(param.JobDescription.Length) : null,
                    () => param.JobContactName != null ? JobRepairValidation.JobContactNameLenght(param.JobContactName.Length) : null,
                    () => param.JobContactTel != null ? JobRepairValidation.JobContactTelLenght(param.JobContactTel.Length) : null,
                    () => param.JobContactName != null ? JobRepairValidation.JobSenderContactNameLenght(param.JobSenderContactName.Length) : null,
                    () => param.JobContactTel != null ? JobRepairValidation.JobSenderContactTelLenght(param.JobSenderContactTel.Length) : null,
                    () => param.JobSenderRemark != null ? JobRepairValidation.JobSenderRemarkLenght(param.JobSenderRemark.Length) : null,
                    



                    //() => param.JobContactName != null ? JobRepairValidation.JobFixedDescriptionLenght(param.JobFixedDescription.Length) : null,
                    //() => param.JobContactTel != null ? JobRepairValidation.JobFixedCommentLenght(param.JobFixedComment.Length) : null,
                    //() => param.JobContactName != null ? JobRepairValidation.JobFixedContactNameLenght(param.JobFixedContactName.Length) : null,
                    //() => param.JobContactTel != null ? JobRepairValidation.JobFixedContactTelLenght(param.JobFixedContactTel.Length) : null,


                    //() => param.Wan1Provider != null ? JobRepairValidation.Wan1ProviderLenght(param.Wan1Provider.Length) : null,
                    //() => param.Wan1Cid != null ? JobRepairValidation.Wan1CidLenght(param.Wan1Cid.Length) : null,
                    //() => param.Wan1Speed != null ? JobRepairValidation.Wan1SpeedLenght(param.Wan1Speed.Length) : null,
                    //() => param.Wan1AsNumber != null ? JobRepairValidation.Wan1AsNumberLenght(param.Wan1AsNumber.Length) : null,
                    //() => param.Wan1IpWan1Pe != null ? JobRepairValidation.Wan1IpWan1PeLenght(param.Wan1IpWan1Pe.Length) : null,
                    //() => param.Wan1IpWan1Ce != null ? JobRepairValidation.Wan1IpWan1CeLenght(param.Wan1IpWan1Ce.Length) : null,
                    //() => param.Wan1Subnet != null ? JobRepairValidation.Wan1SubnetLenght(param.Wan1Subnet.Length) : null,
                    //() => param.Wan2Provider != null ? JobRepairValidation.Wan2ProviderLenght(param.Wan2Provider.Length) : null,
                    //() => param.Wan2Cid != null ? JobRepairValidation.Wan2CidLenght(param.Wan2Cid.Length) : null,
                    //() => param.Wan2Speed != null ? JobRepairValidation.Wan2SpeedLenght(param.Wan2Speed.Length) : null,
                    //() => param.Wan2AsNumber != null ? JobRepairValidation.Wan2AsNumberLenght(param.Wan2AsNumber.Length) : null,
                    //() => param.Wan2IpWan1Pe != null ? JobRepairValidation.Wan2IpWan1PeLenght(param.Wan2IpWan1Pe.Length) : null,
                    //() => param.Wan2IpWan1Ce != null ? JobRepairValidation.Wan2IpWan1CeLenght(param.Wan2IpWan1Ce.Length) : null,
                    //() => param.Wan2Subnet != null ? JobRepairValidation.Wan2SubnetLenght(param.Wan2Subnet.Length) : null,
                    //() => param.InternetCid != null ? JobRepairValidation.InternetCidLenght(param.InternetCid.Length) : null,
                    //() => param.InternetSpeed != null ? JobRepairValidation.InternetSpeedLenght(param.InternetSpeed.Length) : null,
                    //() => param.InternetAsNumber != null ? JobRepairValidation.InternetAsNumberLenght(param.InternetAsNumber.Length) : null,
                    //() => param.InternetWanIpAddress != null ? JobRepairValidation.InternetWanIpAddressLenght(param.InternetWanIpAddress.Length) : null,
                    //() => param.InternetSubnet != null ? JobRepairValidation.InternetSubnetLenght(param.InternetSubnet.Length) : null,
                    //() => param.CellularSim != null ? JobRepairValidation.CellularSimLenght(param.CellularSim.Length) : null,
                    //() => param.CellularAr109 != null ? JobRepairValidation.CellularAr109Lenght(param.CellularAr109.Length) : null,
                    //() => param.JobCreatedBy != null ? JobRepairValidation.JobCreatedByLenght(param.JobCreatedBy.Length) : null,
                    //() => param.JobAcceptBy != null ? JobRepairValidation.JobAcceptByLenght(param.JobAcceptBy.Length) : null,
                    //() => param.JobProcessBy != null ? JobRepairValidation.JobProcessByLenght(param.JobProcessBy.Length) : null,
                    //() => param.JobCompleteBy != null ? JobRepairValidation.JobCompleteByLenght(param.JobCompleteBy.Length) : null,


                    () => param.SiteNetworkId != null ? JobRepairValidation.SiteNetworkIdMaster(param.SiteNetworkId,_context) : null,
                    () => param.SiteInformationId != null ? JobRepairValidation.SiteInformationMaster(param.SiteInformationId,_context) : null,
                    () => param.SysStatusId != null ? JobRepairValidation.SysStatusMaster(param.SysStatusId,_context) : null,
                    () => param.JobCreatedBy != null ? JobRepairValidation.JobCreatedByMaster(param.JobCreatedBy,_context) : null,
                    () => param.JobAcceptBy != null ? JobRepairValidation.JobAcceptByMaster(param.JobAcceptBy,_context) : null,
                    () => param.JobProcessBy != null ? JobRepairValidation.JobProcessByMaster(param.JobProcessBy,_context) : null,
                    () => param.JobCompleteBy != null ? JobRepairValidation.JobCompleteByMaster(param.JobCompleteBy,_context) : null

                    //() => param.CaseOfIssueId != null ? JobRepairValidation.CaseOfIssueIdMaster(param.CaseOfIssueId,_context) : null,
                    //() => param.CaseOfFixId != null ? JobRepairValidation.CaseOfFixIdMaster(param.CaseOfFixId,_context) : null,
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
                    if (jobImage1 != null && jobImage1.Length != null)
                    {
                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                        }

                        if (jobImage1.Length > 0)
                        {
                            Guid guid = Guid.NewGuid();

                            Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                            string fileName = illegalInFileName.Replace(ContentDispositionHeaderValue.Parse(jobImage1.ContentDisposition).FileName.Trim('"'), "");
                            string fileType = "";


                            if (jobImage1.ContentType == "image/jpeg")
                            {
                                fileType = ".jpg";
                            }
                            else if (jobImage1.ContentType == "image/png")
                            {
                                fileType = ".png";
                            }
                            else
                            {
                                validation.httpCode = Constants.httpCode200;
                                validation.status = Constants.statusError;
                                validation.statusCode = Constants.statusCodeOK;
                                validation.message = "รูปแบบไฟล์ JobImage1 ไม่ถูกต้องกรุณาอัพโหลดเฉพาะไฟล์รูปภาพเท่านั้น [.jpg , .png]";

                                return validation;
                            }

                            string fileNameAttach = $"{guid}{fileType}";

                            string fullPathUrl = System.IO.Path.Combine(pathUrl, fileNameAttach);

                            using (var stream = new FileStream(physicalPath + fileNameAttach, FileMode.Create))
                            {
                                jobImage1.CopyTo(stream);

                                param.JobImage1 = $"{folderReadFile}/{currenrRunning}/{fileNameAttach}";
                            }
                        }
                    }

                    if (jobImage2 != null && jobImage2.Length != null)
                    {

                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                        }

                        if (jobImage2.Length > 0)
                        {
                            Guid guid = Guid.NewGuid();

                            Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                            string fileName = illegalInFileName.Replace(ContentDispositionHeaderValue.Parse(jobImage2.ContentDisposition).FileName.Trim('"'), "");
                            string fileType = "";


                            if (jobImage2.ContentType == "image/jpeg")
                            {
                                fileType = ".jpg";
                            }
                            else if (jobImage2.ContentType == "image/png")
                            {
                                fileType = ".png";
                            }
                            else
                            {
                                validation.httpCode = Constants.httpCode200;
                                validation.status = Constants.statusError;
                                validation.statusCode = Constants.statusCodeOK;
                                validation.message = "รูปแบบไฟล์ JobImage2 ไม่ถูกต้องกรุณาอัพโหลดเฉพาะไฟล์รูปภาพเท่านั้น [.jpg , .png]";

                                return validation;
                            }

                            string fileNameAttach = $"{guid}{fileType}";

                            string fullPathUrl = System.IO.Path.Combine(pathUrl, fileNameAttach);

                            using (var stream = new FileStream(physicalPath + fileNameAttach, FileMode.Create))
                            {
                                jobImage2.CopyTo(stream);

                                param.JobImage2 = $"{folderReadFile}/{currenrRunning}/{fileNameAttach}";
                            }
                        }
                    }

                    if (jobImage3 != null && jobImage3.Length != null)
                    {

                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                        }

                        if (jobImage3.Length > 0)
                        {
                            Guid guid = Guid.NewGuid();

                            Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                            string fileName = illegalInFileName.Replace(ContentDispositionHeaderValue.Parse(jobImage3.ContentDisposition).FileName.Trim('"'), "");
                            string fileType = "";


                            if (jobImage3.ContentType == "image/jpeg")
                            {
                                fileType = ".jpg";
                            }
                            else if (jobImage3.ContentType == "image/png")
                            {
                                fileType = ".png";
                            }
                            else
                            {
                                validation.httpCode = Constants.httpCode200;
                                validation.status = Constants.statusError;
                                validation.statusCode = Constants.statusCodeOK;
                                validation.message = "รูปแบบไฟล์ jobImage3 ไม่ถูกต้องกรุณาอัพโหลดเฉพาะไฟล์รูปภาพเท่านั้น [.jpg , .png]";

                                return validation;
                            }

                            string fileNameAttach = $"{guid}{fileType}";

                            string fullPathUrl = System.IO.Path.Combine(pathUrl, fileNameAttach);

                            using (var stream = new FileStream(physicalPath + fileNameAttach, FileMode.Create))
                            {
                                jobImage3.CopyTo(stream);

                                param.JobImage3 = $"{folderReadFile}/{currenrRunning}/{fileNameAttach}";
                            }
                        }
                    }

                    if (jobImage4 != null && jobImage4.Length != null)
                    {
                        if (!Directory.Exists(physicalPath))
                        {
                            Directory.CreateDirectory(physicalPath);
                        }

                        if (jobImage4.Length > 0)
                        {
                            Guid guid = Guid.NewGuid();

                            Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                            string fileName = illegalInFileName.Replace(ContentDispositionHeaderValue.Parse(jobImage4.ContentDisposition).FileName.Trim('"'), "");
                            string fileType = "";


                            if (jobImage4.ContentType == "image/jpeg")
                            {
                                fileType = ".jpg";
                            }
                            else if (jobImage4.ContentType == "image/png")
                            {
                                fileType = ".png";
                            }
                            else
                            {
                                validation.httpCode = Constants.httpCode200;
                                validation.status = Constants.statusError;
                                validation.statusCode = Constants.statusCodeOK;
                                validation.message = "รูปแบบไฟล์ jobImage4 ไม่ถูกต้องกรุณาอัพโหลดเฉพาะไฟล์รูปภาพเท่านั้น [.jpg , .png]";

                                return validation;
                            }

                            string fileNameAttach = $"{guid}{fileType}";

                            string fullPathUrl = System.IO.Path.Combine(pathUrl, fileNameAttach);

                            using (var stream = new FileStream(physicalPath + fileNameAttach, FileMode.Create))
                            {
                                jobImage4.CopyTo(stream);

                                param.JobImage4 = $"{folderReadFile}/{currenrRunning}/{fileNameAttach}";
                            }
                        }
                    }
                }

                #endregion

                if (validation.status == true)
                {
                    await Task.Run(() => _context.JobRepairs.Add(param));

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


        public async Task<Response> Update(JobRepair param, IFormFile? jobImage1, IFormFile? jobImage2, IFormFile? jobImage3, IFormFile? jobImage4)
        {
            Response resp = new Response();

            try
            {
                #region Model Setting

                var update = await Task.Run(() => _context.JobRepairs.Where(x => x.Id == param.Id).FirstOrDefault());

                var getRunning = update.DocumentNo.Split("-");

                var CurrentRunning = getRunning[2];


                string uploads = Directory.GetCurrentDirectory();

                string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["FolderUploadFile"];

                string? folderReadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["FolderReadFile"];

                string pathUrl = System.IO.Path.Combine($"{uploads}/{folderUploadFile}/");

                //string subFolder = $"{DateTime.Now.ToString("yyyy-MM")}";

                string physicalPath = $"{pathUrl}/{CurrentRunning}/";

                List<string> columnNotUpdate = new List<string>();

                columnNotUpdate.Add("JobCreatedBy");
                columnNotUpdate.Add("JobCreatedDate");
                columnNotUpdate.Add("JobAcceptBy");
                columnNotUpdate.Add("JobAcceptDate");
                columnNotUpdate.Add("DocumentNo");
                columnNotUpdate.Add("SiteNetworkId");
                columnNotUpdate.Add("SiteInformationId");
                columnNotUpdate.Add("JobImage1");
                columnNotUpdate.Add("JobImage2");
                columnNotUpdate.Add("JobImage3");
                columnNotUpdate.Add("JobImage4");
                //columnNotUpdate.Add("TypeRepairData");
                //columnNotUpdate.Add("TypeRepairValue");

                if (param.SysStatusId == 7)
                {
                    columnNotUpdate.Add("JobProcessDate");
                }


                if (update != null)
                {
                    AppHelper.TransferData_ClassA_to_ClassB<JobRepair, JobRepair>(param, ref update, columnNotUpdate);

                    update.JobCreatedBy = update.JobCreatedBy;
                    update.JobCreatedDate = update.JobCreatedDate;
                    update.JobAcceptBy = update.JobAcceptBy;
                    update.JobAcceptDate = update.JobAcceptDate;

                    if (param.SysStatusId == 6)
                    {
                        if (update.JobCompleteDate == null)
                        {
                            update.JobCompleteDate = DateTime.Now;
                        }
                        else
                        {
                            update.JobCompleteDate = update.JobCompleteDate;
                        }
                    }

                    if (param.SysStatusId == 7)
                    {
                        if (update.JobCompleteDate == null)
                        {
                            update.JobCompleteDate = DateTime.Now;
                        }
                        else
                        {
                            update.JobCompleteDate = update.JobCompleteDate;
                        }
                    }


                    #region Validation Zone

                    Response validation = new Response();

                    Func<Response>[] validationFunctions = new Func<Response>[]
                    {
                    () => JobRepairValidation.Id(update.Id),
                    () => JobRepairValidation.SiteNetworkId(update.SiteNetworkId),
                    () => JobRepairValidation.SiteInformationId(update.SiteInformationId),
                    () => JobRepairValidation.JobDescription(update.JobDescription),

                    //() => JobRepairValidation.JobContactName(update.JobContactName),
                    //() => JobRepairValidation.JobContactTel(update.JobContactTel),
                    //() => JobRepairValidation.JobSenderContactName(update.JobSenderContactName),
                    //() => JobRepairValidation.JobSenderContactTel(update.JobSenderContactTel),
                    //() => JobRepairValidation.JobSenderRemark(update.JobSenderRemark),

                    //() => update.SysStatusId == 7 ? JobRepairValidation.JobFixedDescription(update.JobFixedDescription) : null,
                    //() => update.SysStatusId == 7 ?JobRepairValidation.JobFixedComment(update.JobFixedComment): null,
                    //() => update.SysStatusId == 7 ?JobRepairValidation.JobFixedContactName(update.JobFixedContactName): null,
                    //() => update.SysStatusId == 7 ?JobRepairValidation.JobFixedContactTel(update.JobFixedContactTel): null,

                    () => JobRepairValidation.SysStatusId(update.SysStatusId),
                    () => JobRepairValidation.JobCreatedBy(update.JobCreatedBy),
                    () => JobRepairValidation.JobCreatedDate((DateTime)update.JobCreatedDate),


                    () => update.DocumentNo != null ? JobRepairValidation.DocumentNoLenght(update.DocumentNo.Length) : null,
                    () => update.JobDescription != null ? JobRepairValidation.JobDescriptionLenght(update.JobDescription.Length) : null,
                    () => update.JobContactName != null ? JobRepairValidation.JobContactNameLenght(update.JobContactName.Length) : null,
                    () => update.JobContactTel != null ? JobRepairValidation.JobContactTelLenght(update.JobContactTel.Length) : null,
                    () => update.JobSenderContactName != null ? JobRepairValidation.JobSenderContactNameLenght(update.JobSenderContactName.Length) : null,
                    () => update.JobSenderContactTel != null ? JobRepairValidation.JobSenderContactTelLenght(update.JobSenderContactTel.Length) : null,
                    () => update.JobSenderRemark != null ? JobRepairValidation.JobSenderRemarkLenght(update.JobSenderRemark.Length) : null,

                    () => update.JobFixedDescription != null ? JobRepairValidation.JobFixedDescriptionLenght(update.JobFixedDescription.Length) : null,
                    () => update.JobFixedComment != null ? JobRepairValidation.JobFixedCommentLenght(update.JobFixedComment.Length) : null,
                    () => update.JobFixedContactName != null ? JobRepairValidation.JobFixedContactNameLenght(update.JobFixedContactName.Length) : null,
                    () => update.JobFixedContactTel != null ? JobRepairValidation.JobFixedContactTelLenght(update.JobFixedContactTel.Length) : null,
                    //() => update.Wan1Provider != null ? JobRepairValidation.Wan1ProviderLenght(update.Wan1Provider.Length) : null,
                    //() => update.Wan1Cid != null ? JobRepairValidation.Wan1CidLenght(update.Wan1Cid.Length) : null,
                    //() => update.Wan1Speed != null ? JobRepairValidation.Wan1SpeedLenght(update.Wan1Speed.Length) : null,
                    //() => update.Wan1AsNumber != null ? JobRepairValidation.Wan1AsNumberLenght(update.Wan1AsNumber.Length) : null,
                    //() => update.Wan1IpWan1Pe != null ? JobRepairValidation.Wan1IpWan1PeLenght(update.Wan1IpWan1Pe.Length) : null,
                    //() => update.Wan1IpWan1Ce != null ? JobRepairValidation.Wan1IpWan1CeLenght(update.Wan1IpWan1Ce.Length) : null,
                    //() => update.Wan1Subnet != null ? JobRepairValidation.Wan1SubnetLenght(update.Wan1Subnet.Length) : null,
                    //() => update.Wan2Provider != null ? JobRepairValidation.Wan2ProviderLenght(update.Wan2Provider.Length) : null,
                    //() => update.Wan2Cid != null ? JobRepairValidation.Wan2CidLenght(update.Wan2Cid.Length) : null,
                    //() => update.Wan2Speed != null ? JobRepairValidation.Wan2SpeedLenght(update.Wan2Speed.Length) : null,
                    //() => update.Wan2AsNumber != null ? JobRepairValidation.Wan2AsNumberLenght(update.Wan2AsNumber.Length) : null,
                    //() => update.Wan2IpWan1Pe != null ? JobRepairValidation.Wan2IpWan1PeLenght(update.Wan2IpWan1Pe.Length) : null,
                    //() => update.Wan2IpWan1Ce != null ? JobRepairValidation.Wan2IpWan1CeLenght(update.Wan2IpWan1Ce.Length) : null,
                    //() => update.Wan2Subnet != null ? JobRepairValidation.Wan2SubnetLenght(update.Wan2Subnet.Length) : null,
                    //() => update.InternetCid != null ? JobRepairValidation.InternetCidLenght(update.InternetCid.Length) : null,
                    //() => update.InternetSpeed != null ? JobRepairValidation.InternetSpeedLenght(update.InternetSpeed.Length) : null,
                    //() => update.InternetAsNumber != null ? JobRepairValidation.InternetAsNumberLenght(update.InternetAsNumber.Length) : null,
                    //() => update.InternetWanIpAddress != null ? JobRepairValidation.InternetWanIpAddressLenght(update.InternetWanIpAddress.Length) : null,
                    //() => update.InternetSubnet != null ? JobRepairValidation.InternetSubnetLenght(update.InternetSubnet.Length) : null,
                    //() => update.CellularSim != null ? JobRepairValidation.CellularSimLenght(update.CellularSim.Length) : null,
                    //() => update.CellularAr109 != null ? JobRepairValidation.CellularAr109Lenght(update.CellularAr109.Length) : null,
                    //() => update.JobCreatedBy != null ? JobRepairValidation.JobCreatedByLenght(update.JobCreatedBy.Length) : null,
                    //() => update.JobAcceptBy != null ? JobRepairValidation.JobAcceptByLenght(update.JobAcceptBy.Length) : null,
                    //() => update.JobProcessBy != null ? JobRepairValidation.JobProcessByLenght(update.JobProcessBy.Length) : null,
                    //() => update.JobCompleteBy != null ? JobRepairValidation.JobCompleteByLenght(update.JobCompleteBy.Length) : null,


                    () => update.SiteNetworkId != null ? JobRepairValidation.SiteNetworkIdMaster(update.SiteNetworkId,_context) : null,
                    () => update.SiteInformationId != null ? JobRepairValidation.SiteInformationMaster(update.SiteInformationId,_context) : null,
                    () => update.SysStatusId != null ? JobRepairValidation.SysStatusMaster(update.SysStatusId,_context) : null,
                    () => update.JobCreatedBy != null ? JobRepairValidation.JobCreatedByMaster(update.JobCreatedBy,_context) : null,
                    () => update.JobAcceptBy != null ? JobRepairValidation.JobAcceptByMaster(update.JobAcceptBy,_context) : null,
                    () => update.JobProcessBy != null ? JobRepairValidation.JobProcessByMaster(update.JobProcessBy,_context) : null,
                    () => update.JobCompleteBy != null ? JobRepairValidation.JobCompleteByMaster(update.JobCompleteBy,_context) : null
                    //() => param.CaseOfIssueId != null ? JobRepairValidation.CaseOfIssueIdMaster(param.CaseOfIssueId,_context) : null,
                    //() => param.CaseOfFixId != null ? JobRepairValidation.CaseOfFixIdMaster(param.CaseOfFixId,_context) : null,
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
                        if (jobImage1 != null && jobImage1.Length > 0)
                        {

                            if (!Directory.Exists(physicalPath))
                            {
                                Directory.CreateDirectory(physicalPath);
                            }

                            if (jobImage1.Length > 0)
                            {
                                Guid guid = Guid.NewGuid();

                                Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                                string fileName = illegalInFileName.Replace(ContentDispositionHeaderValue.Parse(jobImage1.ContentDisposition).FileName.Trim('"'), "");
                                string fileType = "";


                                if (jobImage1.ContentType == "image/jpeg")
                                {
                                    fileType = ".jpg";
                                }
                                else if (jobImage1.ContentType == "image/png")
                                {
                                    fileType = ".png";
                                }
                                else
                                {
                                    validation.httpCode = Constants.httpCode200;
                                    validation.status = Constants.statusError;
                                    validation.statusCode = Constants.statusCodeOK;
                                    validation.message = "รูปแบบไฟล์ jobImage1 ไม่ถูกต้องกรุณาอัพโหลดเฉพาะไฟล์รูปภาพเท่านั้น [.jpg , .png]";

                                    return validation;
                                }

                                string fileNameAttach = $"{guid}{fileType}";

                                string fullPathUrl = System.IO.Path.Combine(pathUrl, fileNameAttach);

                                using (var stream = new FileStream(physicalPath + fileNameAttach, FileMode.Create))
                                {
                                    jobImage1.CopyTo(stream);

                                    if (File.Exists(update.JobImage1))
                                    {
                                        File.Delete(update.JobImage1);
                                    }

                                    update.JobImage1 = $"{folderReadFile}/{CurrentRunning}/{fileNameAttach}";
                                }

                            }
                        }

                        if (jobImage2 != null && jobImage1.Length > 0)
                        {

                            if (!Directory.Exists(physicalPath))
                            {
                                Directory.CreateDirectory(physicalPath);
                            }

                            if (jobImage2.Length > 0)
                            {
                                Guid guid = Guid.NewGuid();

                                Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                                string fileName = illegalInFileName.Replace(ContentDispositionHeaderValue.Parse(jobImage2.ContentDisposition).FileName.Trim('"'), "");
                                string fileType = "";


                                if (jobImage2.ContentType == "image/jpeg")
                                {
                                    fileType = ".jpg";
                                }
                                else if (jobImage2.ContentType == "image/png")
                                {
                                    fileType = ".png";
                                }
                                else
                                {
                                    validation.httpCode = Constants.httpCode200;
                                    validation.status = Constants.statusError;
                                    validation.statusCode = Constants.statusCodeOK;
                                    validation.message = "รูปแบบไฟล์ jobImage2 ไม่ถูกต้องกรุณาอัพโหลดเฉพาะไฟล์รูปภาพเท่านั้น [.jpg , .png]";

                                    return validation;
                                }

                                string fileNameAttach = $"{guid}{fileType}";

                                string fullPathUrl = System.IO.Path.Combine(pathUrl, fileNameAttach);

                                using (var stream = new FileStream(physicalPath + fileNameAttach, FileMode.Create))
                                {
                                    jobImage2.CopyTo(stream);

                                    if (File.Exists(update.JobImage2))
                                    {
                                        File.Delete(update.JobImage2);
                                    }

                                    update.JobImage2 = $"{folderReadFile}/{CurrentRunning}/{fileNameAttach}";
                                }
                            }
                        }

                        if (jobImage3 != null && jobImage3.Length > 0)
                        {

                            if (!Directory.Exists(physicalPath))
                            {
                                Directory.CreateDirectory(physicalPath);
                            }

                            if (jobImage3.Length > 0)
                            {
                                Guid guid = Guid.NewGuid();

                                Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                                string fileName = illegalInFileName.Replace(ContentDispositionHeaderValue.Parse(jobImage3.ContentDisposition).FileName.Trim('"'), "");
                                string fileType = "";


                                if (jobImage3.ContentType == "image/jpeg")
                                {
                                    fileType = ".jpg";
                                }
                                else if (jobImage3.ContentType == "image/png")
                                {
                                    fileType = ".png";
                                }
                                else
                                {
                                    validation.httpCode = Constants.httpCode200;
                                    validation.status = Constants.statusError;
                                    validation.statusCode = Constants.statusCodeOK;
                                    validation.message = "รูปแบบไฟล์ jobImage3 ไม่ถูกต้องกรุณาอัพโหลดเฉพาะไฟล์รูปภาพเท่านั้น [.jpg , .png]";

                                    return validation;
                                }

                                string fileNameAttach = $"{guid}{fileType}";

                                string fullPathUrl = System.IO.Path.Combine(pathUrl, fileNameAttach);

                                using (var stream = new FileStream(physicalPath + fileNameAttach, FileMode.Create))
                                {
                                    jobImage3.CopyTo(stream);

                                    if (File.Exists(update.JobImage3))
                                    {
                                        File.Delete(update.JobImage3);
                                    }

                                    update.JobImage3 = $"{folderReadFile}/{CurrentRunning}/{fileNameAttach}";
                                }
                            }
                        }

                        if (jobImage4 != null && jobImage4.Length > 0)
                        {

                            if (!Directory.Exists(physicalPath))
                            {
                                Directory.CreateDirectory(physicalPath);
                            }

                            if (jobImage4.Length > 0)
                            {
                                Guid guid = Guid.NewGuid();

                                Regex illegalInFileName = new Regex(@"[\\/:*?""<>|]");

                                string fileName = illegalInFileName.Replace(ContentDispositionHeaderValue.Parse(jobImage4.ContentDisposition).FileName.Trim('"'), "");
                                string fileType = "";


                                if (jobImage4.ContentType == "image/jpeg")
                                {
                                    fileType = ".jpg";
                                }
                                else if (jobImage4.ContentType == "image/png")
                                {
                                    fileType = ".png";
                                }
                                else
                                {
                                    validation.httpCode = Constants.httpCode200;
                                    validation.status = Constants.statusError;
                                    validation.statusCode = Constants.statusCodeOK;
                                    validation.message = "รูปแบบไฟล์ jobImage4 ไม่ถูกต้องกรุณาอัพโหลดเฉพาะไฟล์รูปภาพเท่านั้น [.jpg , .png]";

                                    return validation;
                                }

                                string fileNameAttach = $"{guid}{fileType}";

                                string fullPathUrl = System.IO.Path.Combine(pathUrl, fileNameAttach);

                                using (var stream = new FileStream(physicalPath + fileNameAttach, FileMode.Create))
                                {
                                    jobImage4.CopyTo(stream);

                                    if (File.Exists(update.JobImage4))
                                    {
                                        File.Delete(update.JobImage4);
                                    }

                                    update.JobImage4 = $"{folderReadFile}/{CurrentRunning}/{fileNameAttach}";
                                }
                            }
                        }
                    }


                    #endregion

                    if (validation.status == true)
                    {
                        //await Task.Run(() => _context.JobRepairs.Add(update));

                        _context.SaveChanges();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = update;

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

                #endregion

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

        public async Task<Response> UpdateTime(JobRepairUpdateTimeRequest param)
        {
            Response resp = new Response();

            try
            {
                var update = await Task.Run(() => _context.JobRepairs.Where(x => x.Id == param.Id).FirstOrDefault());

                if (update != null)
                {
                    if (param.Flag.ToLower() == "JobCreatedDate".ToLower())
                    {
                        update.JobCreatedDate = param.Value;
                        update.JobAcceptDate = param.Value;
                    }
                    else if (param.Flag.ToLower() == "JobAcceptDate".ToLower())
                    {
                        update.JobCreatedDate = param.Value;
                        update.JobAcceptDate = param.Value;
                    }
                    else if (param.Flag.ToLower() == "JobProcessDate".ToLower())
                    {
                        update.JobProcessDate = param.Value;
                    }
                    else if (param.Flag.ToLower() == "JobCompleteDate".ToLower())
                    {
                        update.JobCompleteDate = param.Value;
                    }
                    else
                    {
                        resp.httpCode = Constants.httpCode400;
                        resp.status = Constants.statusError;
                        resp.statusCode = Constants.statusCodeDataNotFound;
                        resp.message = Constants.recordDataNotFound;
                    }

                    _context.SaveChanges();

                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = update;
                }
                else
                {
                    resp.httpCode = Constants.httpCode400;
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

        public async Task<Response> ReportRepairAdmin(ExportJobRepairFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<ReportRepairResponse> reportRepairResponses = new List<ReportRepairResponse>();

            try
            {
                var tempJobRepair = await Task.Run(() => _context.JobRepairs.ToList());
                var tempSiteNetwork = await Task.Run(() => _context.SiteNetworks.ToList());
                var tempSiteInformation = await Task.Run(() => _context.SiteInformations.ToList());


                var distinctMonthYears = tempJobRepair
                    .Select(date => new { date.JobCreatedDate.Value.Year, date.JobCreatedDate.Value.Month })
                    .Distinct()
                    .OrderBy(x => x.Year).ThenBy(x => x.Month)
                    .ToList();

                foreach (var item in distinctMonthYears)
                {
                    ReportRepairResponse reportRepairResponse = new ReportRepairResponse();

                    string monthTh = Convert.ToString(item.Month).Length == 1 ? Convert.ToString("0" + item.Month) : Convert.ToString(item.Month);

                    reportRepairResponse.MonthName = AppHelper.ConvertMonthNumberToShortMonthThai(monthTh) + " " + item.Year;

                    reportRepairResponse.Year = item.Year;
                    reportRepairResponse.Month = item.Month;

                    reportRepairResponses.Add(reportRepairResponse);
                }

                foreach (var item in tempJobRepair.OrderBy(x => x.JobCreatedDate))
                {
                    ReportRepairResponse reportRepairResponse = new ReportRepairResponse();

                    var checkSiteNetwork = tempSiteNetwork.Where(x => x.Id == item.SiteNetworkId).FirstOrDefault();
                    var checkSiteInformation = tempSiteInformation.Where(x => x.Id == item.SiteInformationId).FirstOrDefault();
                    var SiteInformationName = checkSiteInformation.ProvinceName.Substring(0, 3);

                    #region DC Check

                    if (SiteInformationName == "DC1")
                    {
                        foreach (var listDateTime in reportRepairResponses)
                        {
                            if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                            {
                                listDateTime.Dc1 += 1;
                            }
                        }
                    }
                    else if (SiteInformationName == "DC2")
                    {
                        foreach (var listDateTime in reportRepairResponses)
                        {
                            if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                            {
                                listDateTime.Dc2 += 1;
                            }
                        }
                    }
                    else
                    {
                        foreach (var listDateTime in reportRepairResponses)
                        {
                            if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                            {
                                listDateTime.Other += 1;
                            }
                        }
                    }


                    #endregion


                    #region Site Network Check

                    if (checkSiteInformation.SiteNetworkId == 2) // ผนวก 1
                    {
                        foreach (var listDateTime in reportRepairResponses)
                        {
                            if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                            {
                                listDateTime.SiteNetwork1 += 1;
                                listDateTime.All += 1;
                            }
                        }
                    }
                    else if (checkSiteInformation.SiteNetworkId == 3) // ผนวก 2
                    {
                        foreach (var listDateTime in reportRepairResponses)
                        {
                            if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                            {
                                listDateTime.SiteNetwork2 += 1;
                                listDateTime.All += 1;
                            }
                        }
                    }
                    else if (checkSiteInformation.SiteNetworkId == 4) // ผนวก 3
                    {
                        foreach (var listDateTime in reportRepairResponses)
                        {
                            if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                            {
                                listDateTime.SiteNetwork3 += 1;
                                listDateTime.All += 1;
                            }
                        }
                    }
                    else if (checkSiteInformation.SiteNetworkId == 5) // ผนวก 4
                    {
                        foreach (var listDateTime in reportRepairResponses)
                        {
                            if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                            {
                                listDateTime.SiteNetwork4 += 1;
                                listDateTime.All += 1;
                            }
                        }
                    }
                    else
                    {
                        foreach (var listDateTime in reportRepairResponses)
                        {
                            if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                            {
                                listDateTime.All += 1;
                            }
                        }
                    }

                    #endregion


                    #region Provider Check

                    if (item.TypeRepairData == "วงจรหลัก")
                    {
                        if (checkSiteInformation.Wan1Provider != null)
                        {
                            if (checkSiteInformation.Wan1Provider.ToUpper() == "UIH")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Uih += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "AWN")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Awn += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "CAT")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Cat += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "INTERLINK")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Interlink += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "SYMPHONY")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Symphony += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "JINET")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Jinet += 1;
                                    }
                                }
                            }
                        }

                    }
                    else if (item.TypeRepairData == "วงจรรอง")
                    {
                        if (checkSiteInformation.Wan2Provider != null)
                        {
                            if (checkSiteInformation.Wan2Provider.ToUpper() == "UIH")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Uih += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "AWN")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Awn += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "CAT")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Cat += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "INTERLINK")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Interlink += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "SYMPHONY")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Symphony += 1;
                                    }
                                }
                            }
                            else if (checkSiteInformation.Wan1Provider.ToUpper() == "JINET")
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Jinet += 1;
                                    }
                                }
                            }
                        }
                    }

                    #endregion


                    #region Hr4 Check

                    if (SiteInformationName == "DC1" || SiteInformationName == "DC2")
                    {
                        DateTime getDateTimeRequest = item.JobCreatedDate.Value;
                        DateTime getDatetimeNow = DateTime.Now;

                        TimeSpan difference = getDatetimeNow - getDateTimeRequest;

                        if (difference.TotalHours > 4.00)
                        {
                            foreach (var listDateTime in reportRepairResponses)
                            {
                                if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                {
                                    listDateTime.Hr4 += 1;
                                }
                            }
                        }

                    }

                    #endregion


                    #region Hr5 Check

                    if (item.TypeRepairData == "วงจรหลัก" || item.TypeRepairData == "วงจรรอง")
                    {
                        if (checkSiteInformation.SiteNetworkId == 2 ||
                            checkSiteInformation.SiteNetworkId == 3 ||
                            checkSiteInformation.SiteNetworkId == 4 ||
                            checkSiteInformation.SiteNetworkId == 5)
                        {
                            DateTime getDateTimeRequest = item.JobCreatedDate.Value;
                            DateTime getDatetimeNow = DateTime.Now;

                            TimeSpan difference = getDatetimeNow - getDateTimeRequest;

                            if (difference.TotalHours > 5.00)
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Hr5 += 1;
                                    }
                                }
                            }
                        }
                    }
                    else if (item.TypeRepairData == "วงจร Corpnet")
                    {
                        if (checkSiteInformation.SiteNetworkId == 2)
                        {
                            DateTime getDateTimeRequest = item.JobCreatedDate.Value;
                            DateTime getDatetimeNow = DateTime.Now;

                            TimeSpan difference = getDatetimeNow - getDateTimeRequest;

                            if (difference.TotalHours > 5.00)
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Hr5 += 1;
                                    }
                                }
                            }
                        }
                    }

                    #endregion


                    #region Hr15 Check

                    if (SiteInformationName == "DC1" || SiteInformationName == "DC2")
                    {
                        DateTime getDateTimeRequest = item.JobCreatedDate.Value;
                        DateTime getDatetimeNow = DateTime.Now;

                        TimeSpan difference = getDatetimeNow - getDateTimeRequest;

                        if (difference.TotalHours > 15.00)
                        {
                            foreach (var listDateTime in reportRepairResponses)
                            {
                                if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                {
                                    listDateTime.Hr15 += 1;
                                }
                            }
                        }

                    }

                    #endregion


                    #region Hr24 Check

                    if (item.TypeRepairData == "วงจร Corpnet")
                    {
                        if (checkSiteInformation.SiteNetworkId == 2)
                        {
                            DateTime getDateTimeRequest = item.JobCreatedDate.Value;
                            DateTime getDatetimeNow = DateTime.Now;

                            TimeSpan difference = getDatetimeNow - getDateTimeRequest;

                            if (difference.TotalHours > 24.00)
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Hr24 += 1;
                                    }
                                }
                            }
                        }
                    }

                    #endregion


                    #region Hr30 Check

                    if (item.TypeRepairData == "วงจรหลัก" || item.TypeRepairData == "วงจรรอง")
                    {
                        if (checkSiteInformation.SiteNetworkId == 2 ||
                            checkSiteInformation.SiteNetworkId == 3 ||
                            checkSiteInformation.SiteNetworkId == 4 ||
                            checkSiteInformation.SiteNetworkId == 5)
                        {
                            DateTime getDateTimeRequest = item.JobCreatedDate.Value;
                            DateTime getDatetimeNow = DateTime.Now;

                            TimeSpan difference = getDatetimeNow - getDateTimeRequest;

                            if (difference.TotalHours > 30.00)
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Hr30 += 1;
                                    }
                                }
                            }
                        }
                    }
                    else if (item.TypeRepairData == "วงจร Corpnet")
                    {
                        if (checkSiteInformation.SiteNetworkId == 2)
                        {
                            DateTime getDateTimeRequest = item.JobCreatedDate.Value;
                            DateTime getDatetimeNow = DateTime.Now;

                            TimeSpan difference = getDatetimeNow - getDateTimeRequest;

                            if (difference.TotalHours > 30.00)
                            {
                                foreach (var listDateTime in reportRepairResponses)
                                {
                                    if (item.JobCreatedDate.Value.Year == listDateTime.Year && item.JobCreatedDate.Value.Month == listDateTime.Month)
                                    {
                                        listDateTime.Hr30 += 1;
                                    }
                                }
                            }
                        }
                    }


                    #endregion

                    #region Line2 Check



                    #endregion

                }

                #region Sorting

                IQueryable<ReportRepairResponse> myQueryable = reportRepairResponses.AsQueryable();

                myQueryable = ReportRepairResponse.ApplySorting(myQueryable, param.SortName, param.SortType);

                var output = myQueryable.ToList();

                #endregion

                if (param.isAll != null)
                {
                    if (param.isAll == true)
                    {
                        output = output.ToList();
                    }
                    else
                    {
                        output = output
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize)
                       .ToList();
                    }
                }
                else
                {
                    output = output
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }

                resp.httpCode = Constants.httpCode200;
                resp.status = Constants.statusSuccess;
                resp.statusCode = Constants.statusCodeOK;
                resp.data = reportRepairResponses.OrderByDescending(x => x.Year).ThenByDescending(x => x.Month);

            }

            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = ex.Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return resp;
        }

        public async Task<Response> ExportJobRepair(ExportJobRepairRequest param)
        {
            Response resp = new Response();

            List<ExportJobRepairResponse> exportJobRepairResponses = new List<ExportJobRepairResponse>();

            try
            {
                var tempJobRepair = await Task.Run(() => _context.JobRepairs.Where(x =>
                                    x.JobCreatedDate.Value.Year == param.Year &&
                                    x.JobCreatedDate.Value.Month == param.Month)
                                    .ToList());

                if (param.Type == Constants.ExportTypeDc1)
                {
                    tempJobRepair = tempJobRepair.Where(x =>
                                    x.SiteInformationId == 2 ||
                                    x.SiteInformationId == 487 ||
                                    x.SiteInformationId == 488 ||
                                    x.SiteInformationId == 489 ||
                                    x.SiteInformationId == 7 ||
                                    x.SiteInformationId == 490
                                    ).ToList();

                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }

                }
                else if (param.Type == Constants.ExportTypeDc2)
                {
                    tempJobRepair = tempJobRepair.Where(x =>
                                    x.SiteInformationId == 486 ||
                                    x.SiteInformationId == 9 ||
                                    x.SiteInformationId == 491
                                    ).ToList();

                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }

                }
                else if (param.Type == Constants.ExportTypeSite1)
                {
                    tempJobRepair = tempJobRepair.Where(x => x.SiteNetworkId == 2).ToList();

                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeSite2)
                {
                    tempJobRepair = tempJobRepair.Where(x => x.SiteNetworkId == 3).ToList();

                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeSite3)
                {
                    tempJobRepair = tempJobRepair.Where(x => x.SiteNetworkId == 4).ToList();

                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeSite4)
                {
                    tempJobRepair = tempJobRepair.Where(x => x.SiteNetworkId == 5).ToList();

                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeAll)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeUIH)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportTypeUIH).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeAWN)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportTypeAWN).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeCAT)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportTypeCAT).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeInterLink)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportTypeInterLink).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeSymphony)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportTypeSymphony).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportTypeJinet)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportTypeJinet).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportType4Hr)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportType4Hr).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportType5Hr)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportType5Hr).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportType15Hr)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportType15Hr).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportType24Hr)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportType24Hr).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportType30Hr)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportType30Hr).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else if (param.Type == Constants.ExportType2Link)
                {
                    if (tempJobRepair != null && tempJobRepair.Count > 0)
                    {
                        exportJobRepairResponses = await MappingExportJobRepairResponse(tempJobRepair);

                        exportJobRepairResponses = exportJobRepairResponses.Where(x => x.ProviderName == Constants.ExportType2Link).ToList();

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = ServiceHelper.ExportExcel(exportJobRepairResponses);
                    }
                }
                else
                {

                }

            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = ex.Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return resp;
        }


        public async Task<List<ExportJobRepairResponse>> MappingExportJobRepairResponse(List<JobRepair> param)
        {
            List<ExportJobRepairResponse> exportJobs = new List<ExportJobRepairResponse>();

            var tempSiteNetwork = await Task.Run(() => _context.SiteNetworks.ToList());

            var tempSiteInformation = await Task.Run(() => _context.SiteInformations.ToList());

            var tempCaseOfIssueSub = await Task.Run(() => _context.CaseOfIssueSubs.ToList());

            int indexSeq = 1;

            foreach (var item in param)
            {
                ExportJobRepairResponse objItem = new ExportJobRepairResponse();

                SiteInformation siteInformation = new SiteInformation();
                CaseOfIssueSub caseOfIssueSub = new CaseOfIssueSub();

                siteInformation = tempSiteInformation.Where(x => x.Id == item.SiteInformationId).FirstOrDefault();
                caseOfIssueSub = tempCaseOfIssueSub.Where(x => x.Id == item.CaseOfIssueSubId).FirstOrDefault();

                objItem.Seq = indexSeq;
                objItem.DocumentNo = item.DocumentRequest;
                objItem.ProvinceName = siteInformation.ProvinceName;
                objItem.SiteNetworkName = siteInformation.LocationName;
                objItem.CircuitNo = item.TypeRepairValue == "WAN1" ? siteInformation.Wan1Cid : siteInformation.Wan2Cid;
                objItem.Speed = item.TypeRepairValue == "WAN1" ? Convert.ToInt32(siteInformation.Wan1Speed) : Convert.ToInt32(siteInformation.Wan2Speed);
                objItem.CircuitType = item.TypeRepairData;
                objItem.IssueCase = caseOfIssueSub.Name;
                objItem.JobRequestDate = item.JobCreatedDate.Value.ToString("dd/MM/yyyy HH.mm");
                objItem.JobAcceptDate = item.JobAcceptDate.Value.ToString("dd/MM/yyyy HH.mm");
                objItem.JobOnProcessDate = item.JobProcessDate == null ? "N/A" : item.JobProcessDate.Value.ToString("dd/MM/yyyy HH.mm");
                objItem.JobFinishDate = item.JobCompleteDate == null ? "N/A" : item.JobCompleteDate.Value.ToString("dd/MM/yyyy HH.mm");

                TimeSpan DiffResponseTime = item.JobCreatedDate.Value - item.JobAcceptDate.Value;

                objItem.ResponseTime = DiffResponseTime.Days + "วัน " + DiffResponseTime.Hours + "ชั่วโมง " + DiffResponseTime.Minutes + "นาที";


                if (item.JobProcessDate != null)
                {
                    TimeSpan DiffOnProcessTime = item.JobProcessDate.Value - item.JobAcceptDate.Value;

                    objItem.OnProcessTime = DiffOnProcessTime.Days + "วัน " + DiffOnProcessTime.Hours + "ชั่วโมง " + DiffOnProcessTime.Minutes + "นาที";
                }
                else
                {
                    objItem.OnProcessTime = "N/A";
                }


                if (item.JobCompleteDate != null)
                {
                    TimeSpan DiffAllTimeProcessString = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                    objItem.AllTimeProcessString = DiffAllTimeProcessString.Days + "วัน " + DiffAllTimeProcessString.Hours + "ชั่วโมง " + DiffAllTimeProcessString.Minutes + "นาที";
                }
                else
                {
                    objItem.AllTimeProcessString = "N/A";
                }


                objItem.StaffName = item.JobCreatedBy;
                objItem.Remark = item.JobSenderRemark;
                objItem.ProviderName = item.TypeRepairValue == "WAN1" ? siteInformation.Wan1Provider : siteInformation.Wan2Provider;

                indexSeq++;

                exportJobs.Add(objItem);
            }

            return exportJobs;
        }


        public async Task<Response> ExportJobRepairMonth(ExportJobRepairMonthRequest param)
        {
            Response resp = new Response();

            ExportJobRepairMonthReponse exportJobs = new ExportJobRepairMonthReponse();

            try
            {

                var tempJobRepair = _context.JobRepairs.Where(x =>
                x.JobCreatedDate.Value.Year == param.Year &&
                x.JobCreatedDate.Value.Month == param.Month).AsNoTracking().ToList();

                var tempSiteNetwork = _context.SiteNetworks.AsNoTracking().ToList();

                var tempSiteInformation = _context.SiteInformations.AsNoTracking().ToList();

                foreach (var item in tempJobRepair)
                {
                    string monthTh = Convert.ToString(item.JobCreatedDate.Value.Month).Length == 1 ? Convert.ToString("0" + item.JobCreatedDate.Value.Month) : Convert.ToString(item.JobCreatedDate.Value.Month);

                    exportJobs.MonthName = AppHelper.ConvertMonthNumberToMonthThaiShort(monthTh);

                    exportJobs.Year = Convert.ToString(item.JobCreatedDate.Value.Year);


                    #region Finer


                    if (item.CaseOfIssueSubId == 1)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber1 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 2)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber2 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 3)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber3 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 4)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber4 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 5)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber5 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 6)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber6 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 7)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber7 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 8)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber8 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 9)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber9 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 10)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber10 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 11)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber11 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 12)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber12 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 13)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber13 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 14)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber14 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 15)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber15 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 16)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Fiber16 += totalMinute.TotalMinutes;
                        }
                    }


                    #endregion

                    #region Network

                    if (item.CaseOfIssueSubId == 2)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network1 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 3)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network2 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 4)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network3 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 17)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network4 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 18)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network5 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 19)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network6 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 20)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network7 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 21)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network8 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 22)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network9 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 79)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network10 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 23)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network11 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 24)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network12 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 80)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network13 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 25)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network14 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 26)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network15 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 27)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network16 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 28)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network17 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 29)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network18 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 30)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network19 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 31)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network20 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 32)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network21 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 33)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network22 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 34)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network23 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 35)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network24 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 36)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network25 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 37)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network26 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 38)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network27 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 39)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network28 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 40)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network29 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 41)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network30 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 42)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network31 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 43)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network32 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 81)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network33 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 82)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Network34 += totalMinute.TotalMinutes;
                        }
                    }

                    #endregion

                    #region Customer

                    if (item.CaseOfIssueSubId == 44)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer1 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 45)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer2 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 46)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer3 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 47)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer4 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 48)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer5 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 49)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer6 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 50)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer7 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 51)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer8 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 52)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer9 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 53)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer10 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 54)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer11 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 55)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer12 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 56)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer13 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 57)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer14 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 58)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer15 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 69)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer16 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 60)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer17 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 61)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer18 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 62)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer19 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 63)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer20 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 64)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer21 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 65)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer22 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 66)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer23 += totalMinute.TotalMinutes;
                        }
                    }


                    if (item.CaseOfIssueSubId == 83)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Customer24 += totalMinute.TotalMinutes;
                        }
                    }

                    #endregion

                    #region Other

                    if (item.CaseOfIssueSubId == 67)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Other1 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 84)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Other2 += totalMinute.TotalMinutes;
                        }
                    }

                    if (item.CaseOfIssueSubId == 85)
                    {
                        if (item.JobCreatedDate != null && item.JobCompleteDate != null)
                        {
                            var totalMinute = item.JobCompleteDate.Value - item.JobCreatedDate.Value;

                            exportJobs.Other3 += totalMinute.TotalMinutes;
                        }
                    }

                    #endregion


                }

                exportJobs.Fiber1 = exportJobs.Fiber1 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber1 / 60), 2);
                exportJobs.Fiber2 = exportJobs.Fiber2 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber2 / 60), 2);
                exportJobs.Fiber3 = exportJobs.Fiber3 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber3 / 60), 2);
                exportJobs.Fiber4 = exportJobs.Fiber4 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber4 / 60), 2);
                exportJobs.Fiber5 = exportJobs.Fiber5 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber5 / 60), 2);
                exportJobs.Fiber6 = exportJobs.Fiber6 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber6 / 60), 2);
                exportJobs.Fiber7 = exportJobs.Fiber7 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber7 / 60), 2);
                exportJobs.Fiber8 = exportJobs.Fiber8 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber8 / 60), 2);
                exportJobs.Fiber9 = exportJobs.Fiber9 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber9 / 60), 2);
                exportJobs.Fiber10 = exportJobs.Fiber10 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber10 / 60), 2);
                exportJobs.Fiber11 = exportJobs.Fiber11 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber11 / 60), 2);
                exportJobs.Fiber12 = exportJobs.Fiber12 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber12 / 60), 2);
                exportJobs.Fiber13 = exportJobs.Fiber13 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber13 / 60), 2);
                exportJobs.Fiber14 = exportJobs.Fiber14 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber14 / 60), 2);
                exportJobs.Fiber15 = exportJobs.Fiber15 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber15 / 60), 2);
                exportJobs.Fiber16 = exportJobs.Fiber16 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Fiber16 / 60), 2);

                exportJobs.Network1 = exportJobs.Network1 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network1 / 60), 2);
                exportJobs.Network2 = exportJobs.Network2 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network2 / 60), 2);
                exportJobs.Network3 = exportJobs.Network3 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network3 / 60), 2);
                exportJobs.Network4 = exportJobs.Network4 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network4 / 60), 2);
                exportJobs.Network5 = exportJobs.Network5 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network5 / 60), 2);
                exportJobs.Network6 = exportJobs.Network6 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network6 / 60), 2);
                exportJobs.Network7 = exportJobs.Network7 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network7 / 60), 2);
                exportJobs.Network8 = exportJobs.Network8 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network8 / 60), 2);
                exportJobs.Network9 = exportJobs.Network9 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network9 / 60), 2);
                exportJobs.Network10 = exportJobs.Network10 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network10 / 60), 2);
                exportJobs.Network11 = exportJobs.Network11 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network11 / 60), 2);
                exportJobs.Network12 = exportJobs.Network12 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network12 / 60), 2);
                exportJobs.Network13 = exportJobs.Network13 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network13 / 60), 2);
                exportJobs.Network14 = exportJobs.Network14 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network14 / 60), 2);
                exportJobs.Network15 = exportJobs.Network15 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network15 / 60), 2);
                exportJobs.Network16 = exportJobs.Network16 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network16 / 60), 2);
                exportJobs.Network17 = exportJobs.Network17 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network17 / 60), 2);
                exportJobs.Network18 = exportJobs.Network18 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network18 / 60), 2);
                exportJobs.Network19 = exportJobs.Network19 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network19 / 60), 2);
                exportJobs.Network20 = exportJobs.Network20 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network20 / 60), 2);
                exportJobs.Network21 = exportJobs.Network21 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network21 / 60), 2);
                exportJobs.Network22 = exportJobs.Network22 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network22 / 60), 2);
                exportJobs.Network23 = exportJobs.Network23 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network23 / 60), 2);
                exportJobs.Network24 = exportJobs.Network24 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network24 / 60), 2);
                exportJobs.Network25 = exportJobs.Network25 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network25 / 60), 2);
                exportJobs.Network26 = exportJobs.Network26 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network26 / 60), 2);
                exportJobs.Network27 = exportJobs.Network27 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network27 / 60), 2);
                exportJobs.Network28 = exportJobs.Network28 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network28 / 60), 2);
                exportJobs.Network29 = exportJobs.Network29 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network29 / 60), 2);
                exportJobs.Network30 = exportJobs.Network30 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network30 / 60), 2);
                exportJobs.Network31 = exportJobs.Network31 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network31 / 60), 2);
                exportJobs.Network32 = exportJobs.Network32 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network32 / 60), 2);
                exportJobs.Network33 = exportJobs.Network33 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network33 / 60), 2);
                exportJobs.Network34 = exportJobs.Network34 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Network34 / 60), 2);

                exportJobs.Customer1 = exportJobs.Customer1 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer1 / 60), 2);
                exportJobs.Customer2 = exportJobs.Customer2 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer2 / 60), 2);
                exportJobs.Customer3 = exportJobs.Customer3 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer3 / 60), 2);
                exportJobs.Customer4 = exportJobs.Customer4 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer4 / 60), 2);
                exportJobs.Customer5 = exportJobs.Customer5 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer5 / 60), 2);
                exportJobs.Customer6 = exportJobs.Customer6 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer6 / 60), 2);
                exportJobs.Customer7 = exportJobs.Customer7 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer7 / 60), 2);
                exportJobs.Customer8 = exportJobs.Customer8 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer8 / 60), 2);
                exportJobs.Customer9 = exportJobs.Customer9 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer9 / 60), 2);
                exportJobs.Customer10 = exportJobs.Customer10 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer10 / 60), 2);
                exportJobs.Customer11 = exportJobs.Customer11 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer11 / 60), 2);
                exportJobs.Customer12 = exportJobs.Customer12 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer12 / 60), 2);
                exportJobs.Customer13 = exportJobs.Customer13 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer13 / 60), 2);
                exportJobs.Customer14 = exportJobs.Customer14 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer14 / 60), 2);
                exportJobs.Customer15 = exportJobs.Customer15 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer15 / 60), 2);
                exportJobs.Customer16 = exportJobs.Customer16 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer16 / 60), 2);
                exportJobs.Customer17 = exportJobs.Customer17 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer17 / 60), 2);
                exportJobs.Customer18 = exportJobs.Customer18 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer18 / 60), 2);
                exportJobs.Customer19 = exportJobs.Customer19 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer19 / 60), 2);
                exportJobs.Customer20 = exportJobs.Customer20 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer20 / 60), 2);
                exportJobs.Customer21 = exportJobs.Customer21 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer21 / 60), 2);
                exportJobs.Customer22 = exportJobs.Customer22 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer22 / 60), 2);
                exportJobs.Customer23 = exportJobs.Customer23 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer23 / 60), 2);
                exportJobs.Customer24 = exportJobs.Customer24 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Customer24 / 60), 2);

                exportJobs.Other1 = exportJobs.Other1 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Other1 / 60), 2);
                exportJobs.Other2 = exportJobs.Other2 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Other2 / 60), 2);
                exportJobs.Other3 = exportJobs.Other3 == 0 ? 0 : Math.Round(Convert.ToDouble(exportJobs.Other3 / 60), 2);


                resp.httpCode = Constants.httpCode200;
                resp.status = Constants.statusSuccess;
                resp.statusCode = Constants.statusCodeOK;
                resp.data = exportJobs;
            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = ex.Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return resp;
        }
    }
}

