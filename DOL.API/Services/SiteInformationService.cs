using System;
using System.Linq;
using System.Net.Http.Headers;
using System.Security.Policy;
using System.Text.RegularExpressions;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs.Request;
using DOL.API.Models.Customs.Response;
using DOL.API.Models.Customs.View;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Services.Helper;
using DOL.API.Services.Validation;
using Microsoft.EntityFrameworkCore;
using WatchDog;

namespace DOL.API.Services
{
    public class SiteInformationService
    {
        private readonly DolContext _context;

        public SiteInformationService(DolContext context)
        {
            _context = context;
        }

        public async Task<Response> Overview(SiteInformationFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<SiteInformationOverviewResponse> Overview = new List<SiteInformationOverviewResponse>();

            try
            {
                //var tempJobOnSite = await Task.Run(() => _context.JobOnsites.ToList());
                var tempSysUser = await Task.Run(() => _context.SysUsers.ToList());
                var tempSiteInformation = await Task.Run(() => _context.SiteInformations.ToList());

                var jobAll = tempSiteInformation.Count();

                var groupUser = await Task.Run(() => _context.SiteInformations.Where(x => x.SysUserId != null)
                    .GroupBy(gb => gb.SysUserId)
                    .Select(group => group.OrderByDescending(x => x.SysUserId)
                    .FirstOrDefault().SysUserId)
                    .ToList());


                #region Filter Data

                int idx = 0;

                if (groupUser != null && groupUser.Count > 0)
                {
                    foreach (var user in groupUser)
                    {
                        SiteInformationOverviewResponse obj = new SiteInformationOverviewResponse();

                        var siteInfo = tempSiteInformation.Where(x => x.SysUserId == user).ToList();

                        idx++;

                        foreach (var site in siteInfo)
                        {
                            SiteInformation siteInformation = new SiteInformation();

                            JobOnsitePendings jonOnsitePendings = new JobOnsitePendings();
                            JobOnsiteOnprocess jonOnsiteOnprocess = new JobOnsiteOnprocess();
                            JobOnsiteSuccess jonOnsiteSuccess = new JobOnsiteSuccess();


                            obj.Team = tempSysUser.Where(x => x.Id == user).FirstOrDefault().Name;
                            obj.UserId = tempSysUser.Where(x => x.Id == user).FirstOrDefault().Id;


                            if (site.SysStatusId != 3 && site.SysStatusId != 4)
                            {
                                obj.JobOnsitePendingsCount++;

                                siteInformation = site;
                                jonOnsitePendings.Id = siteInformation.Id;
                                jonOnsitePendings.Location = siteInformation.LocationName;
                                jonOnsitePendings.Province = siteInformation.ProvinceName;
                                jonOnsitePendings.Category = siteInformation.SiteNetworkName;

                                obj.JobOnsitePendings.Add(jonOnsitePendings);
                            }


                            if (site.SysStatusId == 3)
                            {
                                if (site.SysStatusId != 4)
                                {
                                    obj.JobOnsiteOnprocessCount++;

                                    siteInformation = site;
                                    jonOnsiteOnprocess.Id = siteInformation.Id;
                                    jonOnsiteOnprocess.Location = siteInformation.LocationName;
                                    jonOnsiteOnprocess.Province = siteInformation.ProvinceName;
                                    jonOnsiteOnprocess.Category = siteInformation.SiteNetworkName;

                                    obj.JobOnsiteOnprocess.Add(jonOnsiteOnprocess);
                                }
                            }


                            if (site.SysStatusId == 4)
                            {
                                obj.JobOnsiteSuccessesCount++;

                                siteInformation = site;
                                jonOnsiteSuccess.Id = siteInformation.Id;
                                jonOnsiteSuccess.Location = siteInformation.LocationName;
                                jonOnsiteSuccess.Province = siteInformation.ProvinceName;
                                jonOnsiteSuccess.Category = siteInformation.SiteNetworkName;

                                obj.JobOnsiteSuccesses.Add(jonOnsiteSuccess);
                            }


                        }

                        obj.Running = idx;

                        Overview.Add(obj);
                    }
                }


                var tempAll = tempSiteInformation.Count();
                var tempPending = 0;
                var tempOnProcess = 0;
                var tempComplete = 0;

                double tempPendingPercent = 0;
                double tempOnProcessPercent = 0;
                double tempCompletePercent = 0;

                if (Overview != null && Overview.Count > 0)
                {
                    SumCard sumCard = new SumCard();

                    foreach (var item in Overview)
                    {
                        tempPending = item.JobOnsitePendingsCount == null ? tempPending : (int)item.JobOnsitePendingsCount + tempPending;
                        tempOnProcess = item.JobOnsiteOnprocessCount == null ? tempOnProcess : (int)item.JobOnsiteOnprocessCount + tempOnProcess;
                        tempComplete = item.JobOnsiteSuccessesCount == null ? tempComplete : (int)item.JobOnsiteSuccessesCount + tempComplete;
                    }

                    if (tempAll > 0)
                    {
                        if (tempPending > 0)
                        {
                            var a = Convert.ToDouble(tempPending);
                            var b = tempAll;
                            var c = a / b;

                            tempPendingPercent = (c * 100);
                            tempPendingPercent = Math.Round(tempPendingPercent, 2);
                        }

                        if (tempOnProcess > 0)
                        {
                            var a = Convert.ToDouble(tempOnProcess);
                            var b = tempAll;
                            var c = a / b;

                            tempOnProcessPercent = c * 100;
                            tempOnProcessPercent = Math.Round(tempOnProcessPercent, 2);
                        }

                        if (tempComplete > 0)
                        {
                            var a = Convert.ToDouble(tempComplete);
                            var b = tempAll;
                            var c = a / b;

                            tempCompletePercent = c * 100;
                            tempCompletePercent = Math.Round(tempCompletePercent, 2);
                        }
                    }

                }

                foreach (var item in Overview)
                {
                    item.SumCard.CardPendingsCount = tempPending;
                    item.SumCard.CardOnprocessCount = tempOnProcess;
                    item.SumCard.CardSuccessesCount = tempComplete;

                    item.SumCard.CardPendingsPercent = tempPendingPercent;
                    item.SumCard.CardOnprocessPercent = tempOnProcessPercent;
                    item.SumCard.CardSuccessesPercent = tempCompletePercent;

                    double allNotComplete = item.JobOnsitePendings.Count() + item.JobOnsiteOnprocess.Count(); ;

                    var a = item.JobOnsiteSuccesses.Count();
                    var b = allNotComplete;
                    var all = a + b;
                    var c = a / all;
                    var d = c * 100;

                    if (d == double.PositiveInfinity)
                    {
                        d = 100;
                    }


                    item.PercentComplete = Math.Round(d, 2); ;

                }

                #region Sorting

                IQueryable<SiteInformationOverviewResponse> myQueryable = Overview.AsQueryable();

                myQueryable = SiteInformationOverviewResponse.ApplySorting(myQueryable, param.SortName, param.SortType);

                var result = myQueryable.ToList();

                #endregion


                if (param.isAll != null)
                {
                    if (param.isAll == true)
                    {
                        result = result.ToList();
                    }
                    else
                    {
                        result = result
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize)
                       .ToList();
                    }
                }
                else
                {
                    result = result
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }


                if (result != null && result.Count > 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = result;

                    resp.pageNumber = param.PageNumber;
                    resp.pageSize = param.PageSize;
                    resp.effectRow = groupUser.Count();

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

        public async Task<Response> Schedule(SiteInformationFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<SiteInformationScheduleResponse> Dashboard = new List<SiteInformationScheduleResponse>();

            try
            {
                string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiDomain"];

                var JobAll = 0;

                var JobPending = 0;
                double JobPendingPercent = 0;

                var JobOnProcess = 0;
                double JobOnProcessPercent = 0;

                var JobComplete = 0;
                double JobCompletePercent = 0;


                var queryable = await Task.Run(() => _context.SiteInformations.AsQueryable());
                var tempSiteNetwork = await Task.Run(() => _context.SiteNetworks.AsQueryable());


                #region Filter Data

                List<SiteInformation> execute = new List<SiteInformation>();

                execute = queryable.AsNoTracking().ToList();

                execute = execute.OrderBy(x => x.SiteNetworkId).ThenBy(x => x.SiteNetworkSeq).ToList();

                resp.effectRow = execute.Count();

                #endregion


                #region Statistic Card

                if (execute.Count <= 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeDataNotFound;
                    resp.message = Constants.recordDataNotFound;

                    resp.effectRow = null;

                    return resp;
                }


                foreach (var item in execute)
                {
                    var siteNetwork = tempSiteNetwork.Where(x => x.Id == item.SiteNetworkId).FirstOrDefault();

                    bool isPending = false;
                    bool isProcess = false;

                    if (item.SysStatusId == 4)
                    {
                        JobComplete++;
                    }
                    else
                    {
                        if (siteNetwork.JobWan1 == true)
                        {
                            if (string.IsNullOrEmpty(item.Image3) && string.IsNullOrEmpty(item.Image23) && string.IsNullOrEmpty(item.Wan1SpeedTestDownload) && string.IsNullOrEmpty(item.Wan1SpeedTestUpload))
                            {
                                isPending = true;
                            }
                        }

                        if (siteNetwork.JobWan2 == true)
                        {
                            if (string.IsNullOrEmpty(item.Image4) && string.IsNullOrEmpty(item.Image24) && string.IsNullOrEmpty(item.Wan2SpeedTestDownload) && string.IsNullOrEmpty(item.Wan2SpeedTestUpload))
                            {
                                isPending = true;
                            }
                        }

                        if (siteNetwork.JobCellular == true)
                        {
                            if (string.IsNullOrEmpty(item.Image11))
                            {
                                isPending = true;
                            }
                        }

                        if (siteNetwork.JobDevice == true)
                        {
                            if (string.IsNullOrEmpty(item.Image1) &&
                                string.IsNullOrEmpty(item.Image2) &&
                                string.IsNullOrEmpty(item.Image5) &&
                                string.IsNullOrEmpty(item.Image6) &&
                                string.IsNullOrEmpty(item.Image7) &&
                                string.IsNullOrEmpty(item.Image8) &&
                                string.IsNullOrEmpty(item.Image9) &&
                                string.IsNullOrEmpty(item.Image10) &&
                                string.IsNullOrEmpty(item.Image12) &&
                                string.IsNullOrEmpty(item.Image13) &&
                                string.IsNullOrEmpty(item.Image14) &&
                                string.IsNullOrEmpty(item.Image15) &&
                                string.IsNullOrEmpty(item.Image16) &&
                                string.IsNullOrEmpty(item.Image17) &&
                                string.IsNullOrEmpty(item.Image18) &&
                                string.IsNullOrEmpty(item.Image19) &&
                                string.IsNullOrEmpty(item.Image20) &&
                                string.IsNullOrEmpty(item.Image21) &&
                                string.IsNullOrEmpty(item.Image22) &&

                                string.IsNullOrEmpty(item.Image29) &&
                                string.IsNullOrEmpty(item.Image30) &&
                                string.IsNullOrEmpty(item.Image31) &&
                                string.IsNullOrEmpty(item.Image32) &&
                                string.IsNullOrEmpty(item.Image33) &&
                                string.IsNullOrEmpty(item.Image34) &&
                                string.IsNullOrEmpty(item.Image35) &&
                                string.IsNullOrEmpty(item.Image36) &&
                                string.IsNullOrEmpty(item.Image37) &&
                                string.IsNullOrEmpty(item.Image38) &&
                                string.IsNullOrEmpty(item.Image39) &&
                                string.IsNullOrEmpty(item.Image40) &&
                                string.IsNullOrEmpty(item.Image41) &&
                                string.IsNullOrEmpty(item.Image42) &&
                                string.IsNullOrEmpty(item.Image43) &&
                                string.IsNullOrEmpty(item.Image44) &&
                                string.IsNullOrEmpty(item.Image45) &&
                                string.IsNullOrEmpty(item.Image46) &&
                                string.IsNullOrEmpty(item.Image47) &&
                                string.IsNullOrEmpty(item.Image48) &&
                                string.IsNullOrEmpty(item.Image49) &&
                                string.IsNullOrEmpty(item.Image50) &&
                                string.IsNullOrEmpty(item.Image51) &&
                                string.IsNullOrEmpty(item.Image52) &&
                                string.IsNullOrEmpty(item.CircuitInternet100mbDownload) &&
                                string.IsNullOrEmpty(item.CircuitInternet100mbUpload) &&
                                string.IsNullOrEmpty(item.Circuit4g20mbDownload) &&
                                string.IsNullOrEmpty(item.Circuit4g20mbUpload) &&
                                string.IsNullOrEmpty(item.FileApproveName))
                            {
                                isPending = true;
                            }
                        }



                        if (siteNetwork.JobWan1 == true)
                        {
                            if (!string.IsNullOrEmpty(item.Image3) || !string.IsNullOrEmpty(item.Image23) || !string.IsNullOrEmpty(item.Wan1SpeedTestDownload) || !string.IsNullOrEmpty(item.Wan1SpeedTestUpload))
                            {
                                if (item.SysStatusId != 4)
                                {
                                    isProcess = true;
                                }
                            }
                        }

                        if (siteNetwork.JobWan2 == true)
                        {
                            if (!string.IsNullOrEmpty(item.Image4) || !string.IsNullOrEmpty(item.Image24) || !string.IsNullOrEmpty(item.Wan2SpeedTestDownload) || !string.IsNullOrEmpty(item.Wan2SpeedTestUpload))
                            {
                                if (item.SysStatusId != 4)
                                {
                                    isProcess = true;
                                }
                            }
                        }

                        if (siteNetwork.JobCellular == true)
                        {
                            if (!string.IsNullOrEmpty(item.Image11))
                            {
                                if (item.SysStatusId != 4)
                                {
                                    isProcess = true;
                                }
                            }
                        }

                        if (siteNetwork.JobDevice == true)
                        {
                            if (!string.IsNullOrEmpty(item.Image1) ||
                                !string.IsNullOrEmpty(item.Image2) ||
                                !string.IsNullOrEmpty(item.Image5) ||
                                !string.IsNullOrEmpty(item.Image6) ||
                                !string.IsNullOrEmpty(item.Image7) ||
                                !string.IsNullOrEmpty(item.Image8) ||
                                !string.IsNullOrEmpty(item.Image9) ||
                                !string.IsNullOrEmpty(item.Image10) ||
                                !string.IsNullOrEmpty(item.Image12) ||
                                !string.IsNullOrEmpty(item.Image13) ||
                                !string.IsNullOrEmpty(item.Image14) ||
                                !string.IsNullOrEmpty(item.Image15) ||
                                !string.IsNullOrEmpty(item.Image16) ||
                                !string.IsNullOrEmpty(item.Image17) ||
                                !string.IsNullOrEmpty(item.Image18) ||
                                !string.IsNullOrEmpty(item.Image19) ||
                                !string.IsNullOrEmpty(item.Image20) ||
                                !string.IsNullOrEmpty(item.Image21) ||
                                !string.IsNullOrEmpty(item.Image22) ||

                                !string.IsNullOrEmpty(item.Image29) ||
                                !string.IsNullOrEmpty(item.Image30) ||
                                !string.IsNullOrEmpty(item.Image31) ||
                                !string.IsNullOrEmpty(item.Image32) ||
                                !string.IsNullOrEmpty(item.Image33) ||
                                !string.IsNullOrEmpty(item.Image34) ||
                                !string.IsNullOrEmpty(item.Image35) ||
                                !string.IsNullOrEmpty(item.Image36) ||
                                !string.IsNullOrEmpty(item.Image37) ||
                                !string.IsNullOrEmpty(item.Image38) ||
                                !string.IsNullOrEmpty(item.Image39) ||
                                !string.IsNullOrEmpty(item.Image40) ||
                                !string.IsNullOrEmpty(item.Image41) ||
                                !string.IsNullOrEmpty(item.Image42) ||
                                !string.IsNullOrEmpty(item.Image43) ||
                                !string.IsNullOrEmpty(item.Image44) ||
                                !string.IsNullOrEmpty(item.Image45) ||
                                !string.IsNullOrEmpty(item.Image46) ||
                                !string.IsNullOrEmpty(item.Image47) ||
                                !string.IsNullOrEmpty(item.Image48) ||
                                !string.IsNullOrEmpty(item.Image49) ||
                                !string.IsNullOrEmpty(item.Image50) ||
                                !string.IsNullOrEmpty(item.Image51) ||
                                !string.IsNullOrEmpty(item.Image52) ||
                                !string.IsNullOrEmpty(item.CircuitInternet100mbDownload) ||
                                !string.IsNullOrEmpty(item.CircuitInternet100mbUpload) ||
                                !string.IsNullOrEmpty(item.Circuit4g20mbDownload) ||
                                !string.IsNullOrEmpty(item.Circuit4g20mbUpload) ||
                                !string.IsNullOrEmpty(item.FileApproveName))
                            {
                                if (item.SysStatusId != 4)
                                {
                                    isProcess = true;
                                }
                            }
                        }
                    }

                    //if (isPending == true)
                    //{
                    //    JobPending++;
                    //}

                    if (isProcess == true)
                    {
                        JobOnProcess++;

                        var updatpStatus = _context.SiteInformations.Where(x => x.Id == item.Id).FirstOrDefault();

                        if (updatpStatus.SysStatusId != 4)
                        {
                            updatpStatus.SysStatusId = 3;

                        }

                        _context.SaveChanges();
                    }

                    JobAll++;
                }

                JobPending = (JobAll - JobOnProcess) - JobComplete;

                if (JobAll > 0)
                {

                    if (JobPending > 0)
                    {
                        var a = Convert.ToDouble(JobPending);
                        var b = JobAll;
                        var c = a / b;

                        JobPendingPercent = (c * 100);

                        JobPendingPercent = Math.Round(JobPendingPercent, 2);

                    }

                    if (JobOnProcess > 0)
                    {
                        var a = Convert.ToDouble(JobOnProcess);
                        var b = JobAll;
                        var c = a / b;

                        JobOnProcessPercent = c * 100;

                        JobOnProcessPercent = Math.Round(JobOnProcessPercent, 2);

                    }


                    if (JobComplete > 0)
                    {
                        var a = Convert.ToDouble(JobComplete);
                        var b = JobAll;
                        var c = a / b;

                        JobCompletePercent = c * 100;

                        JobCompletePercent = Math.Round(JobCompletePercent, 2);

                    }

                }

                #endregion

                #region Tranform master data

                if (!string.IsNullOrWhiteSpace(param.TextSearch))
                {
                    execute = execute.Where(x =>
                    x.ProvinceName.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.LocationName.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.SiteNetworkName.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.Address.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.StaffOrganize.ToLower().Contains(param.TextSearch.ToLower())
                    ).ToList();
                }

                if (param.SysStatusId == 4)
                {
                    execute = execute.Where(x => x.SysStatusId == 4).ToList();
                }

                if (param.SysStatusId == 3)
                {
                    execute = execute.Where(x => x.SysStatusId == 3).ToList();
                }




                if (execute != null && execute.Count > 0)
                {

                    var tempSysStatus = await _context.SysStatuses.ToListAsync();

                    foreach (var item in execute)
                    {
                        SiteInformationScheduleResponse obj = new SiteInformationScheduleResponse();

                        obj.Id = item.Id;
                        obj.Location = item.LocationName;
                        obj.Province = item.ProvinceName;
                        obj.NetworkName = item.SiteNetworkName;
                        obj.Seq = item.SiteNetworkSeq;

                        obj.sumJobOnSite = new SumJobOnSite();

                        DashboardFiles files = new DashboardFiles();

                        files.FileName = "เอกสารรับมอบการติดตั้ง";
                        files.FileSizeUnit = "MB";
                        files.FilePath = string.IsNullOrEmpty(item.FileApproveName) ? null : folderUploadFile + item.FileApproveName;

                        //files.FileSize = files.FilePath == null ? "Unknown" : string.IsNullOrEmpty(item.FileApproveName) ? "Unknown" : Convert.ToString(await ServiceHelper.GetFileSizeAsync(files.FilePath));
                        //files.FileSize = files.FileSize == "Unknown" ? "Unknown" : Convert.ToString(ServiceHelper.ConvertBytesToMegabytes(Convert.ToInt32(files.FileSize)));

                        if (!string.IsNullOrEmpty(item.FileApproveName))
                        {
                            obj.files = new List<DashboardFiles>();

                            obj.files.Add(files);
                        }



                        #region Condition for some status

                        var siteNetwork = tempSiteNetwork.Where(x => x.Id == item.SiteNetworkId).FirstOrDefault();

                        #region WAN1

                        if (siteNetwork.JobWan1 == true)
                        {
                            if (string.IsNullOrEmpty(item.Image3) && string.IsNullOrEmpty(item.Image23) && string.IsNullOrEmpty(item.Wan1SpeedTestDownload) && string.IsNullOrEmpty(item.Wan1SpeedTestUpload))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;

                                    obj.AssignWan1Status = 4;
                                    obj.AssignWan1StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan1Status).FirstOrDefault().NameTh;

                                    obj.AssignInternetStatus = 4;
                                    obj.AssignInternetStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInternetStatus).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignWan1Status = 2;
                                    obj.AssignWan1StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan1Status).FirstOrDefault().NameTh;

                                    obj.AssignInternetStatus = 2;
                                    obj.AssignInternetStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInternetStatus).FirstOrDefault().NameTh;
                                }
                            }

                            else if (string.IsNullOrEmpty(item.Image3) || string.IsNullOrEmpty(item.Image23) || string.IsNullOrEmpty(item.Wan1SpeedTestDownload) || string.IsNullOrEmpty(item.Wan1SpeedTestUpload))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;

                                    obj.AssignWan1Status = 4;
                                    obj.AssignWan1StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan1Status).FirstOrDefault().NameTh;

                                    obj.AssignInternetStatus = 4;
                                    obj.AssignInternetStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInternetStatus).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignWan1Status = 3;
                                    obj.AssignWan1StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan1Status).FirstOrDefault().NameTh;

                                    obj.AssignInternetStatus = 3;
                                    obj.AssignInternetStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInternetStatus).FirstOrDefault().NameTh;
                                }
                            }

                            else if (!string.IsNullOrEmpty(item.Image3) && !string.IsNullOrEmpty(item.Image23) && !string.IsNullOrEmpty(item.Wan1SpeedTestDownload) && !string.IsNullOrEmpty(item.Wan1SpeedTestUpload))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;

                                    obj.AssignWan1Status = 4;
                                    obj.AssignWan1StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan1Status).FirstOrDefault().NameTh;

                                    obj.AssignInternetStatus = 4;
                                    obj.AssignInternetStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInternetStatus).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignWan1Status = 4;
                                    obj.AssignWan1StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan1Status).FirstOrDefault().NameTh;

                                    obj.AssignInternetStatus = 4;
                                    obj.AssignInternetStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInternetStatus).FirstOrDefault().NameTh;
                                }
                            }
                        }
                        else
                        {
                            obj.AssignWan1Status = 0;
                            obj.AssignWan1StatusName = "N/A";
                        }

                        #endregion

                        #region WAN2


                        if (siteNetwork.JobWan2 == true)
                        {
                            if (string.IsNullOrEmpty(item.Image4) && string.IsNullOrEmpty(item.Image24) && string.IsNullOrEmpty(item.Wan2SpeedTestDownload) && string.IsNullOrEmpty(item.Wan2SpeedTestUpload))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;

                                    obj.AssignWan2Status = 4;
                                    obj.AssignWan2StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan2Status).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignWan2Status = 2;
                                    obj.AssignWan2StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan2Status).FirstOrDefault().NameTh;
                                }
                            }

                            else if (string.IsNullOrEmpty(item.Image4) || string.IsNullOrEmpty(item.Image24) || string.IsNullOrEmpty(item.Wan2SpeedTestDownload) || string.IsNullOrEmpty(item.Wan2SpeedTestUpload))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;

                                    obj.AssignWan2Status = 4;
                                    obj.AssignWan2StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan2Status).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignWan2Status = 3;
                                    obj.AssignWan2StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan2Status).FirstOrDefault().NameTh;
                                }
                            }

                            else if (!string.IsNullOrEmpty(item.Image4) && !string.IsNullOrEmpty(item.Image24) && !string.IsNullOrEmpty(item.Wan2SpeedTestDownload) && !string.IsNullOrEmpty(item.Wan2SpeedTestUpload))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;

                                    obj.AssignWan2Status = 4;
                                    obj.AssignWan2StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan2Status).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignWan2Status = 4;
                                    obj.AssignWan2StatusName = tempSysStatus.Where(x => x.Id == obj.AssignWan2Status).FirstOrDefault().NameTh;
                                }
                            }
                        }
                        else
                        {
                            obj.AssignWan2Status = 0;
                            obj.AssignWan2StatusName = "N/A";
                        }


                        #endregion

                        #region INSTALL DEVICE

                        if (siteNetwork.JobDevice == true)
                        {
                            if (string.IsNullOrEmpty(item.Image1) &&
                            string.IsNullOrEmpty(item.Image2) &&
                            string.IsNullOrEmpty(item.Image5) &&
                            string.IsNullOrEmpty(item.Image6) &&
                            string.IsNullOrEmpty(item.Image7) &&
                            string.IsNullOrEmpty(item.Image8) &&
                            string.IsNullOrEmpty(item.Image9) &&
                            string.IsNullOrEmpty(item.Image10) &&
                            string.IsNullOrEmpty(item.Image12) &&
                            string.IsNullOrEmpty(item.Image13) &&
                            string.IsNullOrEmpty(item.Image14) &&
                            string.IsNullOrEmpty(item.Image15) &&
                            string.IsNullOrEmpty(item.Image16) &&
                            string.IsNullOrEmpty(item.Image17) &&
                            string.IsNullOrEmpty(item.Image18) &&
                            string.IsNullOrEmpty(item.Image19) &&
                            string.IsNullOrEmpty(item.Image20) &&
                            string.IsNullOrEmpty(item.Image21) &&
                            string.IsNullOrEmpty(item.Image22) &&
                            string.IsNullOrEmpty(item.Image29) &&
                            string.IsNullOrEmpty(item.Image30) &&
                            string.IsNullOrEmpty(item.Image31) &&
                            string.IsNullOrEmpty(item.Image32) &&
                            string.IsNullOrEmpty(item.Image33) &&
                            string.IsNullOrEmpty(item.Image34) &&
                            string.IsNullOrEmpty(item.Image35) &&
                            string.IsNullOrEmpty(item.Image36) &&
                            string.IsNullOrEmpty(item.Image37) &&
                            string.IsNullOrEmpty(item.Image38) &&
                            string.IsNullOrEmpty(item.Image39) &&
                            string.IsNullOrEmpty(item.Image40) &&
                            string.IsNullOrEmpty(item.Image41) &&
                            string.IsNullOrEmpty(item.Image42) &&
                            string.IsNullOrEmpty(item.Image43) &&
                            string.IsNullOrEmpty(item.Image44) &&
                            string.IsNullOrEmpty(item.Image45) &&
                            string.IsNullOrEmpty(item.Image46) &&
                            string.IsNullOrEmpty(item.Image47) &&
                            string.IsNullOrEmpty(item.Image48) &&
                            string.IsNullOrEmpty(item.Image49) &&
                            string.IsNullOrEmpty(item.Image50) &&
                            string.IsNullOrEmpty(item.Image51) &&
                            string.IsNullOrEmpty(item.Image52) &&
                            string.IsNullOrEmpty(item.CircuitInternet100mbDownload) &&
                            string.IsNullOrEmpty(item.CircuitInternet100mbUpload) &&
                            string.IsNullOrEmpty(item.Circuit4g20mbDownload) &&
                            string.IsNullOrEmpty(item.Circuit4g20mbUpload) &&
                            string.IsNullOrEmpty(item.FileApproveName))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignInstallDeviceStatus = 2;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;
                                }
                            }

                            else if (string.IsNullOrEmpty(item.Image1) ||
                                string.IsNullOrEmpty(item.Image2) ||
                                string.IsNullOrEmpty(item.Image5) ||
                                string.IsNullOrEmpty(item.Image6) ||
                                string.IsNullOrEmpty(item.Image7) ||
                                string.IsNullOrEmpty(item.Image8) ||
                                string.IsNullOrEmpty(item.Image9) ||
                                string.IsNullOrEmpty(item.Image10) ||
                                string.IsNullOrEmpty(item.Image12) ||
                                string.IsNullOrEmpty(item.Image13) ||
                                string.IsNullOrEmpty(item.Image14) ||
                                string.IsNullOrEmpty(item.Image15) ||
                                string.IsNullOrEmpty(item.Image16) ||
                                string.IsNullOrEmpty(item.Image17) ||
                                string.IsNullOrEmpty(item.Image18) ||
                                string.IsNullOrEmpty(item.Image19) ||
                                string.IsNullOrEmpty(item.Image20) ||
                                string.IsNullOrEmpty(item.Image21) ||
                                string.IsNullOrEmpty(item.Image22) ||
                                string.IsNullOrEmpty(item.Image29) ||
                                string.IsNullOrEmpty(item.Image30) ||
                                string.IsNullOrEmpty(item.Image31) ||
                                string.IsNullOrEmpty(item.Image32) ||
                                string.IsNullOrEmpty(item.Image33) ||
                                string.IsNullOrEmpty(item.Image34) ||
                                string.IsNullOrEmpty(item.Image35) ||
                                string.IsNullOrEmpty(item.Image36) ||
                                string.IsNullOrEmpty(item.Image37) ||
                                string.IsNullOrEmpty(item.Image38) ||
                                string.IsNullOrEmpty(item.Image39) ||
                                string.IsNullOrEmpty(item.Image40) ||
                                string.IsNullOrEmpty(item.Image41) ||
                                string.IsNullOrEmpty(item.Image42) ||
                                string.IsNullOrEmpty(item.Image43) ||
                                string.IsNullOrEmpty(item.Image44) ||
                                string.IsNullOrEmpty(item.Image45) ||
                                string.IsNullOrEmpty(item.Image46) ||
                                string.IsNullOrEmpty(item.Image47) ||
                                string.IsNullOrEmpty(item.Image48) ||
                                string.IsNullOrEmpty(item.Image49) ||
                                string.IsNullOrEmpty(item.Image50) ||
                                string.IsNullOrEmpty(item.Image51) ||
                                string.IsNullOrEmpty(item.Image52) ||
                                string.IsNullOrEmpty(item.CircuitInternet100mbDownload) ||
                                string.IsNullOrEmpty(item.CircuitInternet100mbUpload) ||
                                string.IsNullOrEmpty(item.Circuit4g20mbDownload) ||
                                string.IsNullOrEmpty(item.Circuit4g20mbUpload) ||
                                string.IsNullOrEmpty(item.FileApproveName))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignInstallDeviceStatus = 3;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;
                                }

                            }

                            else if (!string.IsNullOrEmpty(item.Image1) &&
                                !string.IsNullOrEmpty(item.Image2) &&
                                !string.IsNullOrEmpty(item.Image5) &&
                                !string.IsNullOrEmpty(item.Image6) &&
                                !string.IsNullOrEmpty(item.Image7) &&
                                !string.IsNullOrEmpty(item.Image8) &&
                                !string.IsNullOrEmpty(item.Image9) &&
                                !string.IsNullOrEmpty(item.Image10) &&
                                !string.IsNullOrEmpty(item.Image12) &&
                                !string.IsNullOrEmpty(item.Image13) &&
                                !string.IsNullOrEmpty(item.Image14) &&
                                !string.IsNullOrEmpty(item.Image15) &&
                                !string.IsNullOrEmpty(item.Image16) &&
                                !string.IsNullOrEmpty(item.Image17) &&
                                !string.IsNullOrEmpty(item.Image18) &&
                                !string.IsNullOrEmpty(item.Image19) &&
                                !string.IsNullOrEmpty(item.Image20) &&
                                !string.IsNullOrEmpty(item.Image21) &&
                                !string.IsNullOrEmpty(item.Image22) &&
                                !string.IsNullOrEmpty(item.Image29) &&
                                !string.IsNullOrEmpty(item.Image30) &&
                                !string.IsNullOrEmpty(item.Image31) &&
                                !string.IsNullOrEmpty(item.Image32) &&
                                !string.IsNullOrEmpty(item.Image33) &&
                                !string.IsNullOrEmpty(item.Image34) &&
                                !string.IsNullOrEmpty(item.Image35) &&
                                !string.IsNullOrEmpty(item.Image36) &&
                                !string.IsNullOrEmpty(item.Image37) &&
                                !string.IsNullOrEmpty(item.Image38) &&
                                !string.IsNullOrEmpty(item.Image39) &&
                                !string.IsNullOrEmpty(item.Image40) &&
                                !string.IsNullOrEmpty(item.Image41) &&
                                !string.IsNullOrEmpty(item.Image42) &&
                                !string.IsNullOrEmpty(item.Image43) &&
                                !string.IsNullOrEmpty(item.Image44) &&
                                !string.IsNullOrEmpty(item.Image45) &&
                                !string.IsNullOrEmpty(item.Image46) &&
                                !string.IsNullOrEmpty(item.Image47) &&
                                !string.IsNullOrEmpty(item.Image48) &&
                                !string.IsNullOrEmpty(item.Image49) &&
                                !string.IsNullOrEmpty(item.Image50) &&
                                !string.IsNullOrEmpty(item.Image51) &&
                                !string.IsNullOrEmpty(item.Image52) &&
                                !string.IsNullOrEmpty(item.CircuitInternet100mbDownload) &&
                                !string.IsNullOrEmpty(item.CircuitInternet100mbUpload) &&
                                !string.IsNullOrEmpty(item.Circuit4g20mbDownload) &&
                                !string.IsNullOrEmpty(item.Circuit4g20mbUpload) &&
                                !string.IsNullOrEmpty(item.FileApproveName))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;
                                }


                            }
                        }
                        else
                        {
                            obj.AssignInstallDeviceStatus = 0;
                            obj.AssignInstallDeviceStatusName = "N/A";
                        }


                        #endregion

                        #region Cellular

                        if (siteNetwork.JobCellular == true)
                        {
                            if (string.IsNullOrEmpty(item.Image11))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignCellularStatus = 2;
                                    obj.AssignCellularStatusName = tempSysStatus.Where(x => x.Id == obj.AssignCellularStatus).FirstOrDefault().NameTh;
                                }
                            }

                            else if (!string.IsNullOrEmpty(item.Image11) ||
                                !string.IsNullOrEmpty(item.Circuit4g20mbDownload) ||
                                !string.IsNullOrEmpty(item.Circuit4g20mbUpload))
                            {
                                if (item.SysStatusId == 4)
                                {
                                    obj.AssignInstallDeviceStatus = 4;
                                    obj.AssignInstallDeviceStatusName = tempSysStatus.Where(x => x.Id == obj.AssignInstallDeviceStatus).FirstOrDefault().NameTh;
                                }
                                else
                                {
                                    obj.AssignCellularStatus = 4;
                                    obj.AssignCellularStatusName = tempSysStatus.Where(x => x.Id == obj.AssignCellularStatus).FirstOrDefault().NameTh;
                                }
                            }
                        }
                        else
                        {
                            obj.AssignCellularStatus = 0;
                            obj.AssignCellularStatusName = "N/A";

                            obj.AssignInternetStatus = 0;
                            obj.AssignInternetStatusName = "N/A";
                        }

                        #endregion

                        #endregion

                        #region Condition Status All

                        bool isJobAllPending = false;
                        bool isJobAllProcess = false;
                        bool isJobAllComplete = false;

                        if (siteNetwork.JobWan1 == true)
                        {
                            if (obj.AssignWan1Status == 2)
                            {
                                isJobAllPending = true;
                            }
                            else if (obj.AssignWan1Status == 3)
                            {
                                isJobAllProcess = true;
                            }
                            else if (obj.AssignWan1Status == 4)
                            {
                                isJobAllComplete = true;
                            }
                        }

                        if (siteNetwork.JobWan2 == true)
                        {
                            if (obj.AssignWan2Status == 2)
                            {
                                isJobAllPending = true;
                            }
                            else if (obj.AssignWan2Status == 3)
                            {
                                isJobAllProcess = true;
                            }
                            else if (obj.AssignWan2Status == 4)
                            {
                                isJobAllComplete = true;
                            }
                        }

                        if (siteNetwork.JobCellular == true)
                        {
                            if (obj.AssignCellularStatus == 2)
                            {
                                isJobAllPending = true;
                            }
                            else if (obj.AssignCellularStatus == 3)
                            {
                                isJobAllProcess = true;
                            }
                            else if (obj.AssignCellularStatus == 4)
                            {
                                isJobAllComplete = true;
                            }
                        }

                        if (siteNetwork.JobDevice == true)
                        {
                            if (obj.AssignInstallDeviceStatus == 2)
                            {
                                isJobAllPending = true;
                            }
                            else if (obj.AssignInstallDeviceStatus == 3)
                            {
                                isJobAllProcess = true;
                            }
                            else if (obj.AssignInstallDeviceStatus == 4)
                            {
                                isJobAllComplete = true;
                            }
                        }

                        if (isJobAllPending == false && isJobAllProcess == false && isJobAllComplete == true)
                        {
                            obj.StatusId = 4;
                            obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                        }
                        else if (isJobAllPending == true || isJobAllProcess == true)
                        {
                            if (isJobAllPending == true)
                            {
                                obj.StatusId = 2;
                                obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                            }

                            if (isJobAllProcess == true || isJobAllComplete == true)
                            {
                                obj.StatusId = 3;
                                obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                            }
                        }



                        #endregion

                        Dashboard.Add(obj);

                    }
                }

                if (param.SysStatusId != null)
                {
                    Dashboard = Dashboard.Where(x => x.StatusId == param.SysStatusId).ToList();
                }



                if (Dashboard != null && Dashboard.Count > 0)
                {
                    foreach (var item in Dashboard)
                    {
                        item.sumJobOnSite.JobAll = JobAll;
                        item.sumJobOnSite.JobPending = JobPending;
                        item.sumJobOnSite.JobOnProcess = JobOnProcess;
                        item.sumJobOnSite.JobComplete = JobComplete;

                        item.sumJobOnSite.JobPendingPercent = JobPendingPercent;
                        item.sumJobOnSite.JobOnProcessPercent = JobOnProcessPercent;
                        item.sumJobOnSite.JobCompletePercent = JobCompletePercent;
                    }
                }

                #endregion

                #region Sorting

                IQueryable<SiteInformationScheduleResponse> myQueryable = Dashboard.AsQueryable();

                myQueryable = SiteInformationScheduleResponse.ApplySorting(myQueryable, param.SortName, param.SortType);

                var result = myQueryable.ToList();

                if (param.isAll != null)
                {
                    if (param.isAll == true)
                    {
                        result = result.ToList();
                    }
                    else
                    {
                        result = result
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize)
                       .ToList();
                    }
                }
                else
                {
                    result = result
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }

                #endregion

                if (result != null && result.Count > 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = result;

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

        public async Task<Response> CardJobs(SiteInformationFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<JobOnsiteCardResponse> output = new List<JobOnsiteCardResponse>();

            try
            {
                var queryable = await Task.Run(() => _context.SiteInformations.AsQueryable());

                #region Sorting

                queryable = SiteInformationFilter.ApplySorting(queryable, param.SortName, param.SortType);

                #endregion

                #region Filter Data


                if (param.SysUserId != null)
                {
                    queryable = queryable.Where(x => x.SysUserId == param.SysUserId).AsQueryable();
                }

                List<SiteInformation> execute = new List<SiteInformation>();

                execute = queryable.AsNoTracking().ToList();

                if (!string.IsNullOrWhiteSpace(param.TextSearch))
                {
                    execute = execute.Where(x =>
                    x.ProvinceName.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.LocationName.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.Address.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.TelephoneNumber.ToLower().Contains(param.TextSearch.ToLower())
                    ).ToList();
                }

                execute = execute.OrderByDescending(x => x.SiteNetworkName).ThenBy(x => x.ProvinceName).ThenBy(x => x.SiteNetworkSeq).ToList();

                resp.effectRow = execute.Count();

                #endregion

                #region Tranform master data

                if (execute != null && execute.Count > 0)
                {
                    var tempSysStatus = await Task.Run(() => _context.SysStatuses.AsQueryable());

                    var tempSiteNetwork = await Task.Run(() => _context.SiteNetworks.AsQueryable());


                    foreach (var item in execute)
                    {
                        JobOnsiteCardResponse obj = new JobOnsiteCardResponse();

                        obj.Id = item.Id;
                        obj.NetworkName = item.SiteNetworkName;
                        obj.LocationName = item.LocationName;
                        obj.Address = item.Address;
                        obj.Tel = item.TelephoneNumber;

                        if (!string.IsNullOrEmpty(item.Latitude) && !string.IsNullOrEmpty(item.Longitude))
                        {
                            obj.GoogleMap = "https://www.google.com/maps/?q=" + item.Latitude + "," + item.Longitude;
                        }

                        var siteNetwork = tempSiteNetwork.Where(x => x.Id == item.SiteNetworkId).FirstOrDefault();


                        if (string.IsNullOrEmpty(item.Image1) &&
                                string.IsNullOrEmpty(item.Image2) &&
                                string.IsNullOrEmpty(item.Image3) &&
                                string.IsNullOrEmpty(item.Image4) &&
                                string.IsNullOrEmpty(item.Image5) &&
                                string.IsNullOrEmpty(item.Image6) &&
                                string.IsNullOrEmpty(item.Image7) &&
                                string.IsNullOrEmpty(item.Image8) &&
                                string.IsNullOrEmpty(item.Image9) &&
                                string.IsNullOrEmpty(item.Image10) &&
                                string.IsNullOrEmpty(item.Image11) &&
                                string.IsNullOrEmpty(item.Image12) &&
                                string.IsNullOrEmpty(item.Image13) &&
                                string.IsNullOrEmpty(item.Image14) &&
                                string.IsNullOrEmpty(item.Image15) &&
                                string.IsNullOrEmpty(item.Image16) &&
                                string.IsNullOrEmpty(item.Image17) &&
                                string.IsNullOrEmpty(item.Image18) &&
                                string.IsNullOrEmpty(item.Image19) &&
                                string.IsNullOrEmpty(item.Image20) &&
                                string.IsNullOrEmpty(item.Image21) &&
                                string.IsNullOrEmpty(item.Image22) &&
                                string.IsNullOrEmpty(item.Image23) &&
                                string.IsNullOrEmpty(item.Image24) &&
                                string.IsNullOrEmpty(item.Image29) &&
                                string.IsNullOrEmpty(item.Image30) &&
                                string.IsNullOrEmpty(item.Image31) &&
                                string.IsNullOrEmpty(item.Image32) &&
                                string.IsNullOrEmpty(item.Image33) &&
                                string.IsNullOrEmpty(item.Image34) &&
                                string.IsNullOrEmpty(item.Image35) &&
                                string.IsNullOrEmpty(item.Image36) &&
                                string.IsNullOrEmpty(item.Image37) &&
                                string.IsNullOrEmpty(item.Image38) &&
                                string.IsNullOrEmpty(item.Image39) &&
                                string.IsNullOrEmpty(item.Image40) &&
                                string.IsNullOrEmpty(item.Image41) &&
                                string.IsNullOrEmpty(item.Image42) &&
                                string.IsNullOrEmpty(item.Image43) &&
                                string.IsNullOrEmpty(item.Image44) &&
                                string.IsNullOrEmpty(item.Image45) &&
                                string.IsNullOrEmpty(item.Image46) &&
                                string.IsNullOrEmpty(item.Image47) &&
                                string.IsNullOrEmpty(item.Image48) &&
                                string.IsNullOrEmpty(item.Image49) &&
                                string.IsNullOrEmpty(item.Image50) &&
                                string.IsNullOrEmpty(item.Image51) &&
                                string.IsNullOrEmpty(item.Image52) &&
                                string.IsNullOrEmpty(item.CircuitInternet100mbDownload) &&
                                string.IsNullOrEmpty(item.CircuitInternet100mbUpload) &&
                                string.IsNullOrEmpty(item.Circuit4g20mbDownload) &&
                                string.IsNullOrEmpty(item.Circuit4g20mbUpload) &&
                                string.IsNullOrEmpty(item.FileApproveName))
                        {
                            if (item.SysStatusId == 4)
                            {
                                obj.StatusId = 4;
                                obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                            }
                            else
                            {
                                obj.StatusId = 2;
                                obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                            }
                        }


                        if (!string.IsNullOrEmpty(item.Image1) ||
                            !string.IsNullOrEmpty(item.Image2) ||
                            !string.IsNullOrEmpty(item.Image3) ||
                            !string.IsNullOrEmpty(item.Image4) ||
                            !string.IsNullOrEmpty(item.Image5) ||
                            !string.IsNullOrEmpty(item.Image6) ||
                            !string.IsNullOrEmpty(item.Image7) ||
                            !string.IsNullOrEmpty(item.Image8) ||
                            !string.IsNullOrEmpty(item.Image9) ||
                            !string.IsNullOrEmpty(item.Image10) ||
                            !string.IsNullOrEmpty(item.Image11) ||
                            !string.IsNullOrEmpty(item.Image12) ||
                            !string.IsNullOrEmpty(item.Image13) ||
                            !string.IsNullOrEmpty(item.Image14) ||
                            !string.IsNullOrEmpty(item.Image15) ||
                            !string.IsNullOrEmpty(item.Image16) ||
                            !string.IsNullOrEmpty(item.Image17) ||
                            !string.IsNullOrEmpty(item.Image18) ||
                            !string.IsNullOrEmpty(item.Image19) ||
                            !string.IsNullOrEmpty(item.Image20) ||
                            !string.IsNullOrEmpty(item.Image21) ||
                            !string.IsNullOrEmpty(item.Image22) ||
                            !string.IsNullOrEmpty(item.Image23) ||
                            !string.IsNullOrEmpty(item.Image24) ||
                            !string.IsNullOrEmpty(item.Image29) ||
                            !string.IsNullOrEmpty(item.Image30) ||
                            !string.IsNullOrEmpty(item.Image31) ||
                            !string.IsNullOrEmpty(item.Image32) ||
                            !string.IsNullOrEmpty(item.Image33) ||
                            !string.IsNullOrEmpty(item.Image34) ||
                            !string.IsNullOrEmpty(item.Image35) ||
                            !string.IsNullOrEmpty(item.Image36) ||
                            !string.IsNullOrEmpty(item.Image37) ||
                            !string.IsNullOrEmpty(item.Image38) ||
                            !string.IsNullOrEmpty(item.Image39) ||
                            !string.IsNullOrEmpty(item.Image40) ||
                            !string.IsNullOrEmpty(item.Image41) ||
                            !string.IsNullOrEmpty(item.Image42) ||
                            !string.IsNullOrEmpty(item.Image43) ||
                            !string.IsNullOrEmpty(item.Image44) ||
                            !string.IsNullOrEmpty(item.Image45) ||
                            !string.IsNullOrEmpty(item.Image46) ||
                            !string.IsNullOrEmpty(item.Image47) ||
                            !string.IsNullOrEmpty(item.Image48) ||
                            !string.IsNullOrEmpty(item.Image49) ||
                            !string.IsNullOrEmpty(item.Image50) ||
                            !string.IsNullOrEmpty(item.Image51) ||
                            !string.IsNullOrEmpty(item.Image52) ||
                            !string.IsNullOrEmpty(item.CircuitInternet100mbDownload) ||
                            !string.IsNullOrEmpty(item.CircuitInternet100mbUpload) ||
                            !string.IsNullOrEmpty(item.Circuit4g20mbDownload) ||
                            !string.IsNullOrEmpty(item.Circuit4g20mbUpload) ||
                            !string.IsNullOrEmpty(item.FileApproveName))
                        {
                            if (item.SysStatusId == 4)
                            {
                                obj.StatusId = 4;
                                obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                            }
                            else
                            {
                                obj.StatusId = 3;
                                obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                            }
                        }


                        if (!string.IsNullOrEmpty(item.Image1) &&
                            !string.IsNullOrEmpty(item.Image2) &&
                            !string.IsNullOrEmpty(item.Image3) &&
                            !string.IsNullOrEmpty(item.Image4) &&
                            !string.IsNullOrEmpty(item.Image5) &&
                            !string.IsNullOrEmpty(item.Image6) &&
                            !string.IsNullOrEmpty(item.Image7) &&
                            !string.IsNullOrEmpty(item.Image8) &&
                            !string.IsNullOrEmpty(item.Image9) &&
                            !string.IsNullOrEmpty(item.Image10) &&
                            !string.IsNullOrEmpty(item.Image11) &&
                            !string.IsNullOrEmpty(item.Image12) &&
                            !string.IsNullOrEmpty(item.Image13) &&
                            !string.IsNullOrEmpty(item.Image14) &&
                            !string.IsNullOrEmpty(item.Image15) &&
                            !string.IsNullOrEmpty(item.Image16) &&
                            !string.IsNullOrEmpty(item.Image17) &&
                            !string.IsNullOrEmpty(item.Image18) &&
                            !string.IsNullOrEmpty(item.Image19) &&
                            !string.IsNullOrEmpty(item.Image20) &&
                            !string.IsNullOrEmpty(item.Image21) &&
                            !string.IsNullOrEmpty(item.Image22) &&
                            !string.IsNullOrEmpty(item.Image23) &&
                            !string.IsNullOrEmpty(item.Image24) &&
                            !string.IsNullOrEmpty(item.Image29) &&
                            !string.IsNullOrEmpty(item.Image30) &&
                            !string.IsNullOrEmpty(item.Image31) &&
                            !string.IsNullOrEmpty(item.Image32) &&
                            !string.IsNullOrEmpty(item.Image33) &&
                            !string.IsNullOrEmpty(item.Image34) &&
                            !string.IsNullOrEmpty(item.Image35) &&
                            !string.IsNullOrEmpty(item.Image36) &&
                            !string.IsNullOrEmpty(item.Image37) &&
                            !string.IsNullOrEmpty(item.Image38) &&
                            !string.IsNullOrEmpty(item.Image39) &&
                            !string.IsNullOrEmpty(item.Image40) &&
                            !string.IsNullOrEmpty(item.Image41) &&
                            !string.IsNullOrEmpty(item.Image42) &&
                            !string.IsNullOrEmpty(item.Image43) &&
                            !string.IsNullOrEmpty(item.Image44) &&
                            !string.IsNullOrEmpty(item.Image45) &&
                            !string.IsNullOrEmpty(item.Image46) &&
                            !string.IsNullOrEmpty(item.Image47) &&
                            !string.IsNullOrEmpty(item.Image48) &&
                            !string.IsNullOrEmpty(item.Image49) &&
                            !string.IsNullOrEmpty(item.Image50) &&
                            !string.IsNullOrEmpty(item.Image51) &&
                            !string.IsNullOrEmpty(item.Image52) &&
                            !string.IsNullOrEmpty(item.CircuitInternet100mbDownload) &&
                            !string.IsNullOrEmpty(item.CircuitInternet100mbUpload) &&
                            !string.IsNullOrEmpty(item.Circuit4g20mbDownload) &&
                            !string.IsNullOrEmpty(item.Circuit4g20mbUpload) &&
                            !string.IsNullOrEmpty(item.FileApproveName))
                        {
                            if (item.SysStatusId == 4)
                            {
                                obj.StatusId = 4;
                                obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                            }
                            else
                            {
                                obj.StatusId = 3;
                                obj.StatusName = tempSysStatus.Where(x => x.Id == obj.StatusId).FirstOrDefault().NameTh;
                            }
                        }

                        output.Add(obj);
                    }
                }


                #endregion

                if (param.SysStatusId != null)
                {
                    output = output.Where(x => x.StatusId == param.SysStatusId).ToList();
                }

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
                resp.message = Constants.httpCode500Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return resp;
        }

        public async Task<Response> Get(SiteInformationFilter param) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();

            List<SiteInformationResponse> siteInformationResponses = new List<SiteInformationResponse>();

            string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiAppRootImageUrl"];

            string? folderUploadWWWroot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiDomain"];


            try
            {
                var queryable = await Task.Run(() => _context.SiteInformations.AsQueryable());

                #region Filter Data

                if (param.Id != null)
                {
                    queryable = queryable.Where(x => x.Id == param.Id).AsQueryable();
                }

                if (!string.IsNullOrWhiteSpace(param.TextSearch))
                {
                    queryable = queryable.Where(x =>
                    x.ProvinceName.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.LocationName.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.Address.ToLower().Contains(param.TextSearch.ToLower()) ||
                    x.StaffOrganize.ToLower().Contains(param.TextSearch.ToLower())
                    ).AsQueryable();
                }

                if (param.SiteNetworkId != null)
                {
                    queryable = queryable.Where(x => x.SiteNetworkId == param.SiteNetworkId).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.ProviceName))
                {
                    queryable = queryable.Where(x => x.ProvinceName.Contains(param.ProviceName)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.LocationName))
                {
                    queryable = queryable.Where(x => x.LocationName.Contains(param.LocationName)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.Address))
                {
                    queryable = queryable.Where(x => x.Address.Contains(param.Address)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.StaffOrganize))
                {
                    queryable = queryable.Where(x => x.StaffOrganize.Contains(param.StaffOrganize)).AsQueryable();
                }

                if (!string.IsNullOrEmpty(param.TelephoneNumber))
                {
                    queryable = queryable.Where(x => x.TelephoneNumber.Contains(param.TelephoneNumber)).AsQueryable();
                }

                if (param.SysUserId != null)
                {
                    queryable = queryable.Where(x => x.SysUserId == param.SysUserId).AsQueryable();
                }

                List<SiteInformation> execute = new List<SiteInformation>();

                execute = queryable.AsNoTracking().ToList();

                execute = execute.OrderByDescending(x => x.SiteNetworkName).ThenBy(x => x.ProvinceName).ThenBy(x => x.SiteNetworkSeq).ToList();

                resp.effectRow = execute.Count();

                #endregion


                #region tranform Data

                if (execute != null & execute.Count > 0)
                {

                    foreach (var item in execute)
                    {
                        item.Image1 = string.IsNullOrEmpty(item.Image1) ? null : folderUploadWWWroot + item.Image1;
                        item.Image2 = string.IsNullOrEmpty(item.Image2) ? null : folderUploadWWWroot + item.Image2;
                        item.Image3 = string.IsNullOrEmpty(item.Image3) ? null : folderUploadWWWroot + item.Image3;
                        item.Image4 = string.IsNullOrEmpty(item.Image4) ? null : folderUploadWWWroot + item.Image4;
                        item.Image5 = string.IsNullOrEmpty(item.Image5) ? null : folderUploadWWWroot + item.Image5;
                        item.Image6 = string.IsNullOrEmpty(item.Image6) ? null : folderUploadWWWroot + item.Image6;
                        item.Image7 = string.IsNullOrEmpty(item.Image7) ? null : folderUploadWWWroot + item.Image7;
                        item.Image8 = string.IsNullOrEmpty(item.Image8) ? null : folderUploadWWWroot + item.Image8;
                        item.Image9 = string.IsNullOrEmpty(item.Image9) ? null : folderUploadWWWroot + item.Image9;
                        item.Image10 = string.IsNullOrEmpty(item.Image10) ? null : folderUploadWWWroot + item.Image10;
                        item.Image11 = string.IsNullOrEmpty(item.Image11) ? null : folderUploadWWWroot + item.Image11;
                        item.Image12 = string.IsNullOrEmpty(item.Image12) ? null : folderUploadWWWroot + item.Image12;
                        item.Image13 = string.IsNullOrEmpty(item.Image13) ? null : folderUploadWWWroot + item.Image13;
                        item.Image14 = string.IsNullOrEmpty(item.Image14) ? null : folderUploadWWWroot + item.Image14;
                        item.Image15 = string.IsNullOrEmpty(item.Image15) ? null : folderUploadWWWroot + item.Image15;
                        item.Image16 = string.IsNullOrEmpty(item.Image16) ? null : folderUploadWWWroot + item.Image16;
                        item.Image17 = string.IsNullOrEmpty(item.Image17) ? null : folderUploadWWWroot + item.Image17;
                        item.Image18 = string.IsNullOrEmpty(item.Image18) ? null : folderUploadWWWroot + item.Image18;
                        item.Image19 = string.IsNullOrEmpty(item.Image19) ? null : folderUploadWWWroot + item.Image19;
                        item.Image20 = string.IsNullOrEmpty(item.Image20) ? null : folderUploadWWWroot + item.Image20;
                        item.Image21 = string.IsNullOrEmpty(item.Image21) ? null : folderUploadWWWroot + item.Image21;
                        item.Image22 = string.IsNullOrEmpty(item.Image22) ? null : folderUploadWWWroot + item.Image22;
                        item.Image23 = string.IsNullOrEmpty(item.Image23) ? null : folderUploadWWWroot + item.Image23;
                        item.Image24 = string.IsNullOrEmpty(item.Image24) ? null : folderUploadWWWroot + item.Image24;
                        item.Image25 = string.IsNullOrEmpty(item.Image25) ? null : folderUploadWWWroot + item.Image25;
                        item.Image26 = string.IsNullOrEmpty(item.Image26) ? null : folderUploadWWWroot + item.Image26;
                        item.Image27 = string.IsNullOrEmpty(item.Image27) ? null : folderUploadWWWroot + item.Image27;
                        item.Image28 = string.IsNullOrEmpty(item.Image28) ? null : folderUploadWWWroot + item.Image28;
                        item.Image29 = string.IsNullOrEmpty(item.Image29) ? null : folderUploadWWWroot + item.Image29;
                        item.Image30 = string.IsNullOrEmpty(item.Image30) ? null : folderUploadWWWroot + item.Image30;
                        item.Image31 = string.IsNullOrEmpty(item.Image31) ? null : folderUploadWWWroot + item.Image31;
                        item.Image32 = string.IsNullOrEmpty(item.Image32) ? null : folderUploadWWWroot + item.Image32;
                        item.Image33 = string.IsNullOrEmpty(item.Image33) ? null : folderUploadWWWroot + item.Image33;
                        item.Image34 = string.IsNullOrEmpty(item.Image34) ? null : folderUploadWWWroot + item.Image34;
                        item.Image35 = string.IsNullOrEmpty(item.Image35) ? null : folderUploadWWWroot + item.Image35;
                        item.Image36 = string.IsNullOrEmpty(item.Image36) ? null : folderUploadWWWroot + item.Image36;
                        item.Image37 = string.IsNullOrEmpty(item.Image37) ? null : folderUploadWWWroot + item.Image37;
                        item.Image38 = string.IsNullOrEmpty(item.Image38) ? null : folderUploadWWWroot + item.Image38;
                        item.Image39 = string.IsNullOrEmpty(item.Image39) ? null : folderUploadWWWroot + item.Image39;
                        item.Image40 = string.IsNullOrEmpty(item.Image40) ? null : folderUploadWWWroot + item.Image40;
                        item.Image41 = string.IsNullOrEmpty(item.Image41) ? null : folderUploadWWWroot + item.Image41;
                        item.Image42 = string.IsNullOrEmpty(item.Image42) ? null : folderUploadWWWroot + item.Image42;
                        item.Image43 = string.IsNullOrEmpty(item.Image43) ? null : folderUploadWWWroot + item.Image43;
                        item.Image44 = string.IsNullOrEmpty(item.Image44) ? null : folderUploadWWWroot + item.Image44;
                        item.Image45 = string.IsNullOrEmpty(item.Image45) ? null : folderUploadWWWroot + item.Image45;
                        item.Image46 = string.IsNullOrEmpty(item.Image46) ? null : folderUploadWWWroot + item.Image46;
                        item.Image47 = string.IsNullOrEmpty(item.Image47) ? null : folderUploadWWWroot + item.Image47;
                        item.Image48 = string.IsNullOrEmpty(item.Image48) ? null : folderUploadWWWroot + item.Image48;
                        item.Image49 = string.IsNullOrEmpty(item.Image49) ? null : folderUploadWWWroot + item.Image49;
                        item.Image50 = string.IsNullOrEmpty(item.Image50) ? null : folderUploadWWWroot + item.Image50;
                        item.Image51 = string.IsNullOrEmpty(item.Image51) ? null : folderUploadWWWroot + item.Image51;
                        item.Image52 = string.IsNullOrEmpty(item.Image52) ? null : folderUploadWWWroot + item.Image52;
                        item.Image53 = string.IsNullOrEmpty(item.Image53) ? null : folderUploadWWWroot + item.Image53;
                        item.Image54 = string.IsNullOrEmpty(item.Image54) ? null : folderUploadWWWroot + item.Image54;
                        item.Image55 = string.IsNullOrEmpty(item.Image55) ? null : folderUploadWWWroot + item.Image55;
                        item.Image56 = string.IsNullOrEmpty(item.Image56) ? null : folderUploadWWWroot + item.Image56;
                        item.Image57 = string.IsNullOrEmpty(item.Image57) ? null : folderUploadWWWroot + item.Image57;
                        item.Image58 = string.IsNullOrEmpty(item.Image58) ? null : folderUploadWWWroot + item.Image58;
                        item.Image59 = string.IsNullOrEmpty(item.Image59) ? null : folderUploadWWWroot + item.Image59;
                        item.Image60 = string.IsNullOrEmpty(item.Image60) ? null : folderUploadWWWroot + item.Image60;
                        item.Image61 = string.IsNullOrEmpty(item.Image61) ? null : folderUploadWWWroot + item.Image61;
                        item.Image62 = string.IsNullOrEmpty(item.Image62) ? null : folderUploadWWWroot + item.Image62;
                        item.Image63 = string.IsNullOrEmpty(item.Image63) ? null : folderUploadWWWroot + item.Image63;
                        item.Image64 = string.IsNullOrEmpty(item.Image64) ? null : folderUploadWWWroot + item.Image64;
                        item.Image65 = string.IsNullOrEmpty(item.Image65) ? null : folderUploadWWWroot + item.Image65;
                        item.Image66 = string.IsNullOrEmpty(item.Image66) ? null : folderUploadWWWroot + item.Image66;
                        item.Image67 = string.IsNullOrEmpty(item.Image67) ? null : folderUploadWWWroot + item.Image67;


                        item.FileApproveName = item.FileApproveName == null ? null : folderUploadWWWroot + item.FileApproveName;
                    }
                }


                #endregion


                #region Tranform master data

                if (execute != null && execute.Count > 0)
                {
                    var tempSiteNetwork = await Task.Run(() => _context.SiteNetworks.AsQueryable());
                    var tempSysStatus = await Task.Run(() => _context.SysStatuses.AsQueryable());
                    var tempSysUser = await Task.Run(() => _context.SysUsers.AsQueryable());


                    foreach (var item in execute)
                    {
                        SiteInformationResponse siteInformationResponse = new SiteInformationResponse();

                        AppHelper.TransferData_ClassA_to_ClassB<SiteInformation, SiteInformationResponse>(item, ref siteInformationResponse, new List<string>());

                        siteInformationResponse.SiteNetwork = item.SiteNetworkId == null ? null : tempSiteNetwork.Where(x => x.Id == item.SiteNetworkId).FirstOrDefault();

                        siteInformationResponse.SysStatus = item.SysStatusId == null ? null : tempSysStatus.Where(x => x.Id == item.SysStatusId).FirstOrDefault();

                        siteInformationResponse.SysUser = item.SysUserId == null ? null : tempSysUser.Where(x => x.Id == item.SysUserId).FirstOrDefault();

                        siteInformationResponses.Add(siteInformationResponse);
                    }
                }

                #endregion

                #region Sorting

                IQueryable<SiteInformationResponse> myQueryable = siteInformationResponses.AsQueryable();

                myQueryable = SiteInformationResponse.ApplySorting(myQueryable, param.SortName, param.SortType);

                var result = myQueryable.ToList();

                #endregion

                if (param.isAll != null)
                {
                    if (param.isAll == true)
                    {
                        result = result.ToList();
                    }
                    else
                    {
                        result = result
                       .Skip((param.PageNumber - 1) * param.PageSize)
                       .Take(param.PageSize)
                       .ToList();
                    }
                }
                else
                {
                    result = result
                   .Skip((param.PageNumber - 1) * param.PageSize)
                   .Take(param.PageSize)
                   .ToList();
                }


                if (result != null && result.Count > 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = result;

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

            SiteInformationResponse objData = new SiteInformationResponse();

            string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiAppRootImageUrl"];

            string? folderUploadWWWroot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiDomain"];


            try
            {
                var queryable = await Task.Run(() => _context.SiteInformations.Where(x => x.Id == id).AsQueryable());

                var execute = queryable.AsNoTracking().FirstOrDefault();

                if (execute != null)
                {
                    var tempSiteNetwork = await Task.Run(() => _context.SiteNetworks.AsQueryable());
                    var tempSysStatus = await Task.Run(() => _context.SysStatuses.AsQueryable());


                    AppHelper.TransferData_ClassA_to_ClassB<SiteInformation, SiteInformationResponse>(execute, ref objData, new List<string>());

                    objData.SiteNetwork = tempSiteNetwork.Where(x => x.Id == execute.SiteNetworkId).FirstOrDefault();

                    objData.SysStatus = tempSysStatus.Where(x => x.Id == execute.SysStatusId).FirstOrDefault();

                    if (objData != null)
                    {
                        for (int i = 1; i <= 67; i++)
                        {
                            string imageFieldName = "Image" + i;
                            string imagePath = objData.GetType().GetProperty(imageFieldName)?.GetValue(objData) as string;

                            if (!string.IsNullOrEmpty(imagePath))
                            {
                                objData.GetType().GetProperty(imageFieldName)?.SetValue(objData, folderUploadWWWroot + imagePath);
                            }
                            else
                            {

                            }
                        }

                        objData.FileApproveName = string.IsNullOrEmpty(objData.FileApproveName) ? null : folderUploadWWWroot + objData.FileApproveName;

                    }

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

        public async Task<Response> Update(SiteInformationRequest param)
        {
            Response resp = new Response();

            try
            {
                var update = await Task.Run(() => _context.SiteInformations.Where(x => x.Id == param.Id).FirstOrDefault());

                if (update != null)
                {

                    update.CreateBy = update.CreateBy != null ? update.CreateBy : param.CreateBy;
                    update.CreateDate = update.CreateDate != null ? update.CreateDate : DateTime.Now;
                    update.UpdateBy = update.UpdateBy != null ? update.UpdateBy : param.UpdateBy;
                    update.UpdateDate = DateTime.Now;
                    update.IsActive = true;

                    update.StaffOrganize = string.IsNullOrEmpty(param.StaffOrganize) ? "" : param.StaffOrganize;
                    update.TelephoneNumber = string.IsNullOrEmpty(param.TelephoneNumber) ? "" : param.TelephoneNumber;
                    update.Address = string.IsNullOrEmpty(param.Address) ? update.Address : param.Address;
                    update.Latitude = string.IsNullOrEmpty(param.Latitude) ? "" : param.Latitude;
                    update.Longitude = string.IsNullOrEmpty(param.Longitude) ? "" : param.Longitude;


                    update.InstallWan1Team = string.IsNullOrEmpty(param.InstallWan1Team) ? update.InstallWan1Team : param.InstallWan1Team;
                    update.InstallWan1Telephone = string.IsNullOrEmpty(param.InstallWan1Telephone) ? update.InstallWan1Telephone : param.InstallWan1Telephone;
                    update.InstallWan2Team = string.IsNullOrEmpty(param.InstallWan2Team) ? update.InstallWan2Team : param.InstallWan2Team;
                    update.InstallWan2Telephone = string.IsNullOrEmpty(param.InstallWan2Telephone) ? update.InstallWan2Telephone : param.InstallWan2Telephone;
                    update.InstallInternetTeam = string.IsNullOrEmpty(param.InstallInternetTeam) ? update.InstallInternetTeam : param.InstallInternetTeam;
                    update.InstallInternetTelephone = string.IsNullOrEmpty(param.InstallInternetTelephone) ? update.InstallInternetTelephone : param.InstallInternetTelephone;
                    update.InstallCellularTeam = string.IsNullOrEmpty(param.InstallCellularTeam) ? update.InstallCellularTeam : param.InstallCellularTeam;
                    update.InstallCellularTelephone = string.IsNullOrEmpty(param.InstallCellularTelephone) ? update.InstallCellularTelephone : param.InstallCellularTelephone;
                    update.InstallDeviceTeam = string.IsNullOrEmpty(param.InstallDeviceTeam) ? update.InstallDeviceTeam : param.InstallDeviceTeam;
                    update.InstallDeviceTelephone = string.IsNullOrEmpty(param.InstallDeviceTelephone) ? update.InstallDeviceTelephone : param.InstallDeviceTelephone;
                    update.Wan1Provider = string.IsNullOrEmpty(param.Wan1Provider) ? update.Wan1Provider : param.Wan1Provider;
                    update.Wan1Cid = string.IsNullOrEmpty(param.Wan1Cid) ? update.Wan1Cid : param.Wan1Cid;
                    update.Wan1Speed = string.IsNullOrEmpty(param.Wan1Speed) ? update.Wan1Speed : param.Wan1Speed;
                    update.Wan1AsNumber = string.IsNullOrEmpty(param.Wan1AsNumber) ? update.Wan1AsNumber : param.Wan1AsNumber;
                    update.Wan1IpWan1Pe = string.IsNullOrEmpty(param.Wan1IpWan1Pe) ? update.Wan1IpWan1Pe : param.Wan1IpWan1Pe;
                    update.Wan1IpWan1Ce = string.IsNullOrEmpty(param.Wan1IpWan1Ce) ? update.Wan1IpWan1Ce : param.Wan1IpWan1Ce;
                    update.Wan1Subnet = string.IsNullOrEmpty(param.Wan1Subnet) ? update.Wan1Subnet : param.Wan1Subnet;
                    update.Wan2Provider = string.IsNullOrEmpty(param.Wan2Provider) ? update.Wan2Provider : param.Wan2Provider;
                    update.Wan2Cid = string.IsNullOrEmpty(param.Wan2Cid) ? update.Wan2Cid : param.Wan2Cid;
                    update.Wan2Speed = string.IsNullOrEmpty(param.Wan2Speed) ? update.Wan2Speed : param.Wan2Speed;
                    update.Wan2AsNumber = string.IsNullOrEmpty(param.Wan2AsNumber) ? update.Wan2AsNumber : param.Wan2AsNumber;
                    update.Wan2IpWan1Pe = string.IsNullOrEmpty(param.Wan2IpWan1Pe) ? update.Wan2IpWan1Pe : param.Wan2IpWan1Pe;
                    update.Wan2IpWan1Ce = string.IsNullOrEmpty(param.Wan2IpWan1Ce) ? update.Wan2IpWan1Ce : param.Wan2IpWan1Ce;
                    update.Wan2Subnet = string.IsNullOrEmpty(param.Wan2Subnet) ? update.Wan2Subnet : param.Wan2Subnet;
                    update.InternetCid = string.IsNullOrEmpty(param.InternetCid) ? update.InternetCid : param.InternetCid;
                    update.InternetSpeed = string.IsNullOrEmpty(param.InternetSpeed) ? update.InternetSpeed : param.InternetSpeed;
                    update.InternetAsNumber = string.IsNullOrEmpty(param.InternetAsNumber) ? update.InternetAsNumber : param.InternetAsNumber;
                    update.InternetWanIpAddress = string.IsNullOrEmpty(param.InternetWanIpAddress) ? update.InternetWanIpAddress : param.InternetWanIpAddress;
                    update.InternetSubnet = string.IsNullOrEmpty(param.InternetSubnet) ? update.InternetSubnet : param.InternetSubnet;
                    update.CellularSim = string.IsNullOrEmpty(param.CellularSim) ? update.CellularSim : param.CellularSim;
                    update.CellularAr109 = string.IsNullOrEmpty(param.CellularAr109) ? update.CellularAr109 : param.CellularAr109;
                    update.IpLanGateway = string.IsNullOrEmpty(param.IpLanGateway) ? update.IpLanGateway : param.IpLanGateway;
                    update.IpLanSubnet = string.IsNullOrEmpty(param.IpLanSubnet) ? update.IpLanSubnet : param.IpLanSubnet;
                    update.EquipmentCpeSwitchMain = string.IsNullOrEmpty(param.EquipmentCpeSwitchMain) ? update.EquipmentCpeSwitchMain : param.EquipmentCpeSwitchMain;
                    update.EquipmentSnCpeSwitchMain = string.IsNullOrEmpty(param.EquipmentSnCpeSwitchMain) ? update.EquipmentSnCpeSwitchMain : param.EquipmentSnCpeSwitchMain;
                    update.EquipmentCpeSwitchSecondary = string.IsNullOrEmpty(param.EquipmentCpeSwitchSecondary) ? update.EquipmentCpeSwitchSecondary : param.EquipmentCpeSwitchSecondary;
                    update.EquipmentSnCpeSwitchSecondary = string.IsNullOrEmpty(param.EquipmentSnCpeSwitchSecondary) ? update.EquipmentSnCpeSwitchSecondary : param.EquipmentSnCpeSwitchSecondary;
                    update.EquipmentFirewall2Set = string.IsNullOrEmpty(param.EquipmentFirewall2Set) ? update.EquipmentFirewall2Set : param.EquipmentFirewall2Set;
                    update.EquipmentFirewall1Sn = string.IsNullOrEmpty(param.EquipmentFirewall1Sn) ? update.EquipmentFirewall1Sn : param.EquipmentFirewall1Sn;
                    update.EquipmentFirewall2Sn = string.IsNullOrEmpty(param.EquipmentFirewall2Sn) ? update.EquipmentFirewall2Sn : param.EquipmentFirewall2Sn;
                    update.EquipmentRouter2Set = string.IsNullOrEmpty(param.EquipmentRouter2Set) ? update.EquipmentRouter2Set : param.EquipmentRouter2Set;
                    update.EquipmentRouter1Sn = string.IsNullOrEmpty(param.EquipmentRouter1Sn) ? update.EquipmentRouter1Sn : param.EquipmentRouter1Sn;
                    update.EquipmentRouter2Sn = string.IsNullOrEmpty(param.EquipmentRouter2Sn) ? update.EquipmentRouter2Sn : param.EquipmentRouter2Sn;
                    update.EquipmentWifi1Set = string.IsNullOrEmpty(param.EquipmentWifi1Set) ? update.EquipmentWifi1Set : param.EquipmentWifi1Set;
                    update.EquipmentWifiSn = string.IsNullOrEmpty(param.EquipmentWifiSn) ? update.EquipmentWifiSn : param.EquipmentWifiSn;
                    update.EquipmentRouter4gSet = string.IsNullOrEmpty(param.EquipmentRouter4gSet) ? update.EquipmentRouter4gSet : param.EquipmentRouter4gSet;
                    update.EquipmentRouter4gSn = string.IsNullOrEmpty(param.EquipmentRouter4gSn) ? update.EquipmentRouter4gSn : param.EquipmentRouter4gSn;

                    update.TeamInstallContactName = string.IsNullOrEmpty(param.TeamInstallContactName) ? update.TeamInstallContactName : param.TeamInstallContactName;
                    update.TeamInstallContactTel = string.IsNullOrEmpty(param.TeamInstallContactTel) ? update.TeamInstallContactTel : param.TeamInstallContactTel;

                    update.Wan1SpeedTestDownload = string.IsNullOrEmpty(param.Wan1SpeedTestDownload) ? update.Wan1SpeedTestDownload : param.Wan1SpeedTestDownload;
                    update.Wan1SpeedTestUpload = string.IsNullOrEmpty(param.Wan1SpeedTestUpload) ? update.Wan1SpeedTestUpload : param.Wan1SpeedTestUpload;
                    update.Wan2SpeedTestDownload = string.IsNullOrEmpty(param.Wan2SpeedTestDownload) ? update.Wan2SpeedTestDownload : param.Wan2SpeedTestDownload;
                    update.Wan2SpeedTestUpload = string.IsNullOrEmpty(param.Wan2SpeedTestUpload) ? update.Wan2SpeedTestUpload : param.Wan2SpeedTestUpload;

                    update.CircuitInternet100mbDownload = string.IsNullOrEmpty(param.CircuitInternet100mbDownload) ? update.CircuitInternet100mbDownload : param.CircuitInternet100mbDownload;
                    update.CircuitInternet100mbUpload = string.IsNullOrEmpty(param.CircuitInternet100mbUpload) ? update.CircuitInternet100mbUpload : param.CircuitInternet100mbUpload;

                    update.Circuit4g20mbDownload = string.IsNullOrEmpty(param.Circuit4g20mbDownload) ? update.Circuit4g20mbDownload : param.Circuit4g20mbDownload;
                    update.Circuit4g20mbUpload = string.IsNullOrEmpty(param.Circuit4g20mbUpload) ? update.Circuit4g20mbUpload : param.Circuit4g20mbUpload;


                    update.SysStatusId = param.SysStatusId == null ? update.SysStatusId : param.SysStatusId;

                    string uploads = Directory.GetCurrentDirectory();

                    string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["FolderUploadFileOnsite"];

                    string? folderReadFile = "";

                    string pathUrl = System.IO.Path.Combine($"{uploads}/{folderUploadFile}");

                    string subFolder = "siteinformation";

                    //string physicalPath = $"{pathUrl}/{update.Id}";

                    string physicalPath = "siteinformation" + @"/" + update.Id + @"/";

                    update.Image1 = param.UploadImage1 != null ? SaveImageOnsite(param.UploadImage1, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image1;
                    update.Image2 = param.UploadImage2 != null ? SaveImageOnsite(param.UploadImage2, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image2;
                    update.Image3 = param.UploadImage3 != null ? SaveImageOnsite(param.UploadImage3, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image3;
                    update.Image4 = param.UploadImage4 != null ? SaveImageOnsite(param.UploadImage4, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image4;
                    update.Image5 = param.UploadImage5 != null ? SaveImageOnsite(param.UploadImage5, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image5;
                    update.Image6 = param.UploadImage6 != null ? SaveImageOnsite(param.UploadImage6, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image6;
                    update.Image7 = param.UploadImage7 != null ? SaveImageOnsite(param.UploadImage7, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image7;
                    update.Image8 = param.UploadImage8 != null ? SaveImageOnsite(param.UploadImage8, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image8;
                    update.Image9 = param.UploadImage9 != null ? SaveImageOnsite(param.UploadImage9, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image9;
                    update.Image10 = param.UploadImage10 != null ? SaveImageOnsite(param.UploadImage10, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image10;
                    update.Image11 = param.UploadImage11 != null ? SaveImageOnsite(param.UploadImage11, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image11;
                    update.Image12 = param.UploadImage12 != null ? SaveImageOnsite(param.UploadImage12, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image12;
                    update.Image13 = param.UploadImage13 != null ? SaveImageOnsite(param.UploadImage13, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image13;
                    update.Image14 = param.UploadImage14 != null ? SaveImageOnsite(param.UploadImage14, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image14;
                    update.Image15 = param.UploadImage15 != null ? SaveImageOnsite(param.UploadImage15, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image15;
                    update.Image16 = param.UploadImage16 != null ? SaveImageOnsite(param.UploadImage16, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image16;
                    update.Image17 = param.UploadImage17 != null ? SaveImageOnsite(param.UploadImage17, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image17;
                    update.Image18 = param.UploadImage18 != null ? SaveImageOnsite(param.UploadImage18, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image18;
                    update.Image19 = param.UploadImage19 != null ? SaveImageOnsite(param.UploadImage19, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image19;
                    update.Image20 = param.UploadImage20 != null ? SaveImageOnsite(param.UploadImage20, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image20;
                    update.Image21 = param.UploadImage21 != null ? SaveImageOnsite(param.UploadImage21, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image21;
                    update.Image22 = param.UploadImage22 != null ? SaveImageOnsite(param.UploadImage22, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image22;
                    update.Image23 = param.UploadImage23 != null ? SaveImageOnsite(param.UploadImage23, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image23;
                    update.Image24 = param.UploadImage24 != null ? SaveImageOnsite(param.UploadImage24, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image24;
                    update.Image25 = param.UploadImage25 != null ? SaveImageOnsite(param.UploadImage25, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image25;
                    update.Image26 = param.UploadImage26 != null ? SaveImageOnsite(param.UploadImage26, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image26;
                    update.Image27 = param.UploadImage27 != null ? SaveImageOnsite(param.UploadImage27, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image27;
                    update.Image28 = param.UploadImage28 != null ? SaveImageOnsite(param.UploadImage28, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image28;
                    update.Image29 = param.UploadImage29 != null ? SaveImageOnsite(param.UploadImage29, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image29;
                    update.Image30 = param.UploadImage30 != null ? SaveImageOnsite(param.UploadImage30, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image30;
                    update.Image31 = param.UploadImage31 != null ? SaveImageOnsite(param.UploadImage31, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image31;
                    update.Image32 = param.UploadImage32 != null ? SaveImageOnsite(param.UploadImage32, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image32;
                    update.Image33 = param.UploadImage33 != null ? SaveImageOnsite(param.UploadImage33, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image33;
                    update.Image34 = param.UploadImage34 != null ? SaveImageOnsite(param.UploadImage34, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image34;
                    update.Image35 = param.UploadImage35 != null ? SaveImageOnsite(param.UploadImage35, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image35;
                    update.Image36 = param.UploadImage36 != null ? SaveImageOnsite(param.UploadImage36, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image36;
                    update.Image37 = param.UploadImage37 != null ? SaveImageOnsite(param.UploadImage37, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image37;
                    update.Image38 = param.UploadImage38 != null ? SaveImageOnsite(param.UploadImage38, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image38;
                    update.Image39 = param.UploadImage39 != null ? SaveImageOnsite(param.UploadImage39, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image39;
                    update.Image40 = param.UploadImage40 != null ? SaveImageOnsite(param.UploadImage40, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image40;
                    update.Image41 = param.UploadImage41 != null ? SaveImageOnsite(param.UploadImage41, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image41;
                    update.Image42 = param.UploadImage42 != null ? SaveImageOnsite(param.UploadImage42, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image42;
                    update.Image43 = param.UploadImage43 != null ? SaveImageOnsite(param.UploadImage43, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image43;
                    update.Image44 = param.UploadImage44 != null ? SaveImageOnsite(param.UploadImage44, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image44;
                    update.Image45 = param.UploadImage45 != null ? SaveImageOnsite(param.UploadImage45, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image45;
                    update.Image46 = param.UploadImage46 != null ? SaveImageOnsite(param.UploadImage46, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image46;
                    update.Image47 = param.UploadImage47 != null ? SaveImageOnsite(param.UploadImage47, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image47;
                    update.Image48 = param.UploadImage48 != null ? SaveImageOnsite(param.UploadImage48, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image48;
                    update.Image49 = param.UploadImage49 != null ? SaveImageOnsite(param.UploadImage49, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image49;
                    update.Image50 = param.UploadImage50 != null ? SaveImageOnsite(param.UploadImage50, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image50;
                    update.Image51 = param.UploadImage51 != null ? SaveImageOnsite(param.UploadImage51, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image51;
                    update.Image52 = param.UploadImage52 != null ? SaveImageOnsite(param.UploadImage52, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image52;
                    update.Image53 = param.UploadImage53 != null ? SaveImageOnsite(param.UploadImage53, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image53;

                    update.Image54 = param.UploadImage54 != null ? SaveImageOnsite(param.UploadImage54, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image54;
                    update.Image55 = param.UploadImage55 != null ? SaveImageOnsite(param.UploadImage55, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image55;
                    update.Image56 = param.UploadImage56 != null ? SaveImageOnsite(param.UploadImage56, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image56;
                    update.Image57 = param.UploadImage57 != null ? SaveImageOnsite(param.UploadImage57, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image57;
                    update.Image58 = param.UploadImage58 != null ? SaveImageOnsite(param.UploadImage58, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image58;
                    update.Image59 = param.UploadImage59 != null ? SaveImageOnsite(param.UploadImage59, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image59;
                    update.Image60 = param.UploadImage60 != null ? SaveImageOnsite(param.UploadImage60, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image60;
                    update.Image61 = param.UploadImage61 != null ? SaveImageOnsite(param.UploadImage61, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image61;
                    update.Image62 = param.UploadImage62 != null ? SaveImageOnsite(param.UploadImage62, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image62;
                    update.Image63 = param.UploadImage63 != null ? SaveImageOnsite(param.UploadImage63, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image63;
                    update.Image64 = param.UploadImage64 != null ? SaveImageOnsite(param.UploadImage64, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image64;
                    update.Image65 = param.UploadImage65 != null ? SaveImageOnsite(param.UploadImage65, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image65;
                    update.Image66 = param.UploadImage66 != null ? SaveImageOnsite(param.UploadImage66, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image66;
                    update.Image67 = param.UploadImage67 != null ? SaveImageOnsite(param.UploadImage67, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.Image67;

                    update.FileApproveName = param.UploadFileApprove != null ? SaveImageOnsite(param.UploadFileApprove, physicalPath, folderReadFile, subFolder, Convert.ToString(update.Id)) : update.FileApproveName;

                    await _context.SaveChangesAsync();

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

        public async Task<Response> Province() // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();
            try
            {

                var queryable = await Task.Run(() => _context.SiteInformations
                                        .GroupBy(gb => gb.ProvinceName)
                                        .Select(group => group.OrderByDescending(x => x.SiteNetworkName)
                                        .FirstOrDefault())
                                        .AsQueryable());

                #region Filter Data


                List<SiteInformation> execute = new List<SiteInformation>();

                execute = queryable.AsNoTracking().ToList();

                resp.effectRow = execute.Count();

                #endregion

                if (execute != null && execute.Count > 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = execute.Select(x => x.ProvinceName).ToList();
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

        public async Task<Response> Location(string provinceName) // Additional models are imported from GlobalFilter and PaginationModel
        {
            Response resp = new Response();
            try
            {

                var queryable = await Task.Run(() => _context.SiteInformations
                .Where(x => x.ProvinceName.ToLower() == provinceName.ToLower())
                .OrderBy(x => x.SiteNetworkSeq)
                .AsQueryable());

                #region Filter Data


                List<SiteInformation> execute = new List<SiteInformation>();

                execute = queryable.AsNoTracking().ToList();

                resp.effectRow = execute.Count();

                #endregion

                if (execute != null && execute.Count > 0)
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.data = execute;
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

        public async Task<Response> DeleteImage(DeleteImageRequest param)
        {
            Response resp = new Response();

            try
            {
                Response validation = new Response();

                Func<Response>[] validationFunctions = new Func<Response>[]
                {
                    () => JobRepairValidation.JobCreatedByMaster(param.Username,_context),
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

                if ((bool)(validation.status = true))
                {
                    var update = await Task.Run(() => _context.SiteInformations.Where(x => x.Id == param.Id).FirstOrDefault());

                    if (update != null)
                    {
                        update.Image1 = param.Flag.ToLower() == "image1" ? "" : update.Image1;
                        update.Image2 = param.Flag.ToLower() == "image2" ? "" : update.Image2;
                        update.Image3 = param.Flag.ToLower() == "image3" ? "" : update.Image3;
                        update.Image4 = param.Flag.ToLower() == "image4" ? "" : update.Image4;
                        update.Image5 = param.Flag.ToLower() == "image5" ? "" : update.Image5;
                        update.Image6 = param.Flag.ToLower() == "image6" ? "" : update.Image6;
                        update.Image7 = param.Flag.ToLower() == "image7" ? "" : update.Image7;
                        update.Image8 = param.Flag.ToLower() == "image8" ? "" : update.Image8;
                        update.Image9 = param.Flag.ToLower() == "image9" ? "" : update.Image9;
                        update.Image10 = param.Flag.ToLower() == "image10" ? "" : update.Image10;
                        update.Image11 = param.Flag.ToLower() == "image11" ? "" : update.Image11;
                        update.Image12 = param.Flag.ToLower() == "image12" ? "" : update.Image12;
                        update.Image13 = param.Flag.ToLower() == "image13" ? "" : update.Image13;
                        update.Image14 = param.Flag.ToLower() == "image14" ? "" : update.Image14;
                        update.Image15 = param.Flag.ToLower() == "image15" ? "" : update.Image15;
                        update.Image16 = param.Flag.ToLower() == "image16" ? "" : update.Image16;
                        update.Image17 = param.Flag.ToLower() == "image17" ? "" : update.Image17;
                        update.Image18 = param.Flag.ToLower() == "image18" ? "" : update.Image18;
                        update.Image19 = param.Flag.ToLower() == "image19" ? "" : update.Image19;
                        update.Image20 = param.Flag.ToLower() == "image20" ? "" : update.Image20;
                        update.Image21 = param.Flag.ToLower() == "image21" ? "" : update.Image21;
                        update.Image22 = param.Flag.ToLower() == "image22" ? "" : update.Image22;
                        update.Image23 = param.Flag.ToLower() == "image23" ? "" : update.Image23;
                        update.Image24 = param.Flag.ToLower() == "image24" ? "" : update.Image24;
                        update.Image25 = param.Flag.ToLower() == "image25" ? "" : update.Image25;
                        update.Image26 = param.Flag.ToLower() == "image26" ? "" : update.Image26;
                        update.Image27 = param.Flag.ToLower() == "image27" ? "" : update.Image27;
                        update.Image28 = param.Flag.ToLower() == "image28" ? "" : update.Image28;
                        update.Image29 = param.Flag.ToLower() == "image29" ? "" : update.Image29;
                        update.Image30 = param.Flag.ToLower() == "image30" ? "" : update.Image30;
                        update.Image31 = param.Flag.ToLower() == "image31" ? "" : update.Image31;
                        update.Image32 = param.Flag.ToLower() == "image32" ? "" : update.Image32;
                        update.Image33 = param.Flag.ToLower() == "image33" ? "" : update.Image33;
                        update.Image34 = param.Flag.ToLower() == "image34" ? "" : update.Image34;
                        update.Image35 = param.Flag.ToLower() == "image35" ? "" : update.Image35;
                        update.Image36 = param.Flag.ToLower() == "image36" ? "" : update.Image36;
                        update.Image37 = param.Flag.ToLower() == "image37" ? "" : update.Image37;
                        update.Image38 = param.Flag.ToLower() == "image38" ? "" : update.Image38;
                        update.Image39 = param.Flag.ToLower() == "image39" ? "" : update.Image39;
                        update.Image40 = param.Flag.ToLower() == "image40" ? "" : update.Image40;
                        update.Image41 = param.Flag.ToLower() == "image41" ? "" : update.Image41;
                        update.Image42 = param.Flag.ToLower() == "image42" ? "" : update.Image42;
                        update.Image43 = param.Flag.ToLower() == "image43" ? "" : update.Image43;
                        update.Image44 = param.Flag.ToLower() == "image44" ? "" : update.Image44;
                        update.Image45 = param.Flag.ToLower() == "image45" ? "" : update.Image45;
                        update.Image46 = param.Flag.ToLower() == "image46" ? "" : update.Image46;
                        update.Image47 = param.Flag.ToLower() == "image47" ? "" : update.Image47;
                        update.Image48 = param.Flag.ToLower() == "image48" ? "" : update.Image48;
                        update.Image49 = param.Flag.ToLower() == "image49" ? "" : update.Image49;
                        update.Image50 = param.Flag.ToLower() == "image50" ? "" : update.Image50;
                        update.Image51 = param.Flag.ToLower() == "image51" ? "" : update.Image51;
                        update.Image52 = param.Flag.ToLower() == "image52" ? "" : update.Image52;
                        update.Image53 = param.Flag.ToLower() == "image53" ? "" : update.Image53;

                        update.Image54 = param.Flag.ToLower() == "image54" ? "" : update.Image54;
                        update.Image55 = param.Flag.ToLower() == "image55" ? "" : update.Image55;
                        update.Image56 = param.Flag.ToLower() == "image56" ? "" : update.Image56;
                        update.Image57 = param.Flag.ToLower() == "image57" ? "" : update.Image57;
                        update.Image58 = param.Flag.ToLower() == "image58" ? "" : update.Image58;
                        update.Image59 = param.Flag.ToLower() == "image59" ? "" : update.Image59;
                        update.Image60 = param.Flag.ToLower() == "image60" ? "" : update.Image60;
                        update.Image61 = param.Flag.ToLower() == "image61" ? "" : update.Image61;
                        update.Image62 = param.Flag.ToLower() == "image62" ? "" : update.Image62;
                        update.Image63 = param.Flag.ToLower() == "image63" ? "" : update.Image63;
                        update.Image64 = param.Flag.ToLower() == "image64" ? "" : update.Image64;
                        update.Image65 = param.Flag.ToLower() == "image65" ? "" : update.Image65;
                        update.Image66 = param.Flag.ToLower() == "image66" ? "" : update.Image66;
                        update.Image67 = param.Flag.ToLower() == "image67" ? "" : update.Image67;

                        update.FileApproveName = param.Flag.ToLower() == "approve" ? "" : update.FileApproveName;

                        update.IsActive = true;
                        update.CreateDate = DateTime.Now;
                        update.UpdateDate = DateTime.Now;

                        await _context.SaveChangesAsync();

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

        public async Task<Response> UpdateImageName(UpdateImageNameRequest param)
        {
            Response resp = new Response();

            try
            {
                string? folderUploadWWWroot = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiDomain"];

                var update = await Task.Run(() => _context.SiteInformations.Where(x => x.Id == param.Id).FirstOrDefault());

                if (update != null)
                {
                    update.Image1 = param.Flag.ToLower() == "image1" ? param.Filename : update.Image1;
                    update.Image2 = param.Flag.ToLower() == "image2" ? param.Filename : update.Image2;
                    update.Image3 = param.Flag.ToLower() == "image3" ? param.Filename : update.Image3;
                    update.Image4 = param.Flag.ToLower() == "image4" ? param.Filename : update.Image4;
                    update.Image5 = param.Flag.ToLower() == "image5" ? param.Filename : update.Image5;
                    update.Image6 = param.Flag.ToLower() == "image6" ? param.Filename : update.Image6;
                    update.Image7 = param.Flag.ToLower() == "image7" ? param.Filename : update.Image7;
                    update.Image8 = param.Flag.ToLower() == "image8" ? param.Filename : update.Image8;
                    update.Image9 = param.Flag.ToLower() == "image9" ? param.Filename : update.Image9;
                    update.Image10 = param.Flag.ToLower() == "image10" ? param.Filename : update.Image10;
                    update.Image11 = param.Flag.ToLower() == "image11" ? param.Filename : update.Image11;
                    update.Image12 = param.Flag.ToLower() == "image12" ? param.Filename : update.Image12;
                    update.Image13 = param.Flag.ToLower() == "image13" ? param.Filename : update.Image13;
                    update.Image14 = param.Flag.ToLower() == "image14" ? param.Filename : update.Image14;
                    update.Image15 = param.Flag.ToLower() == "image15" ? param.Filename : update.Image15;
                    update.Image16 = param.Flag.ToLower() == "image16" ? param.Filename : update.Image16;
                    update.Image17 = param.Flag.ToLower() == "image17" ? param.Filename : update.Image17;
                    update.Image18 = param.Flag.ToLower() == "image18" ? param.Filename : update.Image18;
                    update.Image19 = param.Flag.ToLower() == "image19" ? param.Filename : update.Image19;
                    update.Image20 = param.Flag.ToLower() == "image20" ? param.Filename : update.Image20;
                    update.Image21 = param.Flag.ToLower() == "image21" ? param.Filename : update.Image21;
                    update.Image22 = param.Flag.ToLower() == "image22" ? param.Filename : update.Image22;
                    update.Image23 = param.Flag.ToLower() == "image23" ? param.Filename : update.Image23;
                    update.Image24 = param.Flag.ToLower() == "image24" ? param.Filename : update.Image24;
                    update.Image25 = param.Flag.ToLower() == "image25" ? param.Filename : update.Image25;
                    update.Image26 = param.Flag.ToLower() == "image26" ? param.Filename : update.Image26;
                    update.Image27 = param.Flag.ToLower() == "image27" ? param.Filename : update.Image27;
                    update.Image28 = param.Flag.ToLower() == "image28" ? param.Filename : update.Image28;
                    update.Image29 = param.Flag.ToLower() == "image29" ? param.Filename : update.Image29;
                    update.Image30 = param.Flag.ToLower() == "image30" ? param.Filename : update.Image30;
                    update.Image31 = param.Flag.ToLower() == "image31" ? param.Filename : update.Image31;
                    update.Image32 = param.Flag.ToLower() == "image32" ? param.Filename : update.Image32;
                    update.Image33 = param.Flag.ToLower() == "image33" ? param.Filename : update.Image33;
                    update.Image34 = param.Flag.ToLower() == "image34" ? param.Filename : update.Image34;
                    update.Image35 = param.Flag.ToLower() == "image35" ? param.Filename : update.Image35;
                    update.Image36 = param.Flag.ToLower() == "image36" ? param.Filename : update.Image36;
                    update.Image37 = param.Flag.ToLower() == "image37" ? param.Filename : update.Image37;
                    update.Image38 = param.Flag.ToLower() == "image38" ? param.Filename : update.Image38;
                    update.Image39 = param.Flag.ToLower() == "image39" ? param.Filename : update.Image39;
                    update.Image40 = param.Flag.ToLower() == "image40" ? param.Filename : update.Image40;
                    update.Image41 = param.Flag.ToLower() == "image41" ? param.Filename : update.Image41;
                    update.Image42 = param.Flag.ToLower() == "image42" ? param.Filename : update.Image42;
                    update.Image43 = param.Flag.ToLower() == "image43" ? param.Filename : update.Image43;
                    update.Image44 = param.Flag.ToLower() == "image44" ? param.Filename : update.Image44;
                    update.Image45 = param.Flag.ToLower() == "image45" ? param.Filename : update.Image45;
                    update.Image46 = param.Flag.ToLower() == "image46" ? param.Filename : update.Image46;
                    update.Image47 = param.Flag.ToLower() == "image47" ? param.Filename : update.Image47;
                    update.Image48 = param.Flag.ToLower() == "image48" ? param.Filename : update.Image48;
                    update.Image49 = param.Flag.ToLower() == "image49" ? param.Filename : update.Image49;
                    update.Image50 = param.Flag.ToLower() == "image50" ? param.Filename : update.Image50;
                    update.Image51 = param.Flag.ToLower() == "image51" ? param.Filename : update.Image51;
                    update.Image52 = param.Flag.ToLower() == "image52" ? param.Filename : update.Image52;
                    update.Image53 = param.Flag.ToLower() == "image53" ? param.Filename : update.Image53;

                    update.Image54 = param.Flag.ToLower() == "image54" ? param.Filename : update.Image54;
                    update.Image55 = param.Flag.ToLower() == "image55" ? param.Filename : update.Image55;
                    update.Image56 = param.Flag.ToLower() == "image56" ? param.Filename : update.Image56;
                    update.Image57 = param.Flag.ToLower() == "image57" ? param.Filename : update.Image57;
                    update.Image58 = param.Flag.ToLower() == "image58" ? param.Filename : update.Image58;
                    update.Image59 = param.Flag.ToLower() == "image59" ? param.Filename : update.Image59;
                    update.Image60 = param.Flag.ToLower() == "image60" ? param.Filename : update.Image60;
                    update.Image61 = param.Flag.ToLower() == "image61" ? param.Filename : update.Image61;
                    update.Image62 = param.Flag.ToLower() == "image62" ? param.Filename : update.Image62;
                    update.Image63 = param.Flag.ToLower() == "image63" ? param.Filename : update.Image63;
                    update.Image64 = param.Flag.ToLower() == "image64" ? param.Filename : update.Image64;
                    update.Image65 = param.Flag.ToLower() == "image65" ? param.Filename : update.Image65;
                    update.Image66 = param.Flag.ToLower() == "image66" ? param.Filename : update.Image66;
                    update.Image67 = param.Flag.ToLower() == "image67" ? param.Filename : update.Image67;

                    update.FileApproveName = param.Flag.ToLower() == "approve" ? param.Filename : update.FileApproveName;

                    update.IsActive = true;
                    update.CreateDate = DateTime.Now;
                    update.UpdateDate = DateTime.Now;

                    await _context.SaveChangesAsync();

                    update.Image1 = string.IsNullOrEmpty(update.Image1) ? null : folderUploadWWWroot + update.Image1;
                    update.Image2 = string.IsNullOrEmpty(update.Image2) ? null : folderUploadWWWroot + update.Image2;
                    update.Image3 = string.IsNullOrEmpty(update.Image3) ? null : folderUploadWWWroot + update.Image3;
                    update.Image4 = string.IsNullOrEmpty(update.Image4) ? null : folderUploadWWWroot + update.Image4;
                    update.Image5 = string.IsNullOrEmpty(update.Image5) ? null : folderUploadWWWroot + update.Image5;
                    update.Image6 = string.IsNullOrEmpty(update.Image6) ? null : folderUploadWWWroot + update.Image6;
                    update.Image7 = string.IsNullOrEmpty(update.Image7) ? null : folderUploadWWWroot + update.Image7;
                    update.Image8 = string.IsNullOrEmpty(update.Image8) ? null : folderUploadWWWroot + update.Image8;
                    update.Image9 = string.IsNullOrEmpty(update.Image9) ? null : folderUploadWWWroot + update.Image9;
                    update.Image10 = string.IsNullOrEmpty(update.Image10) ? null : folderUploadWWWroot + update.Image10;
                    update.Image11 = string.IsNullOrEmpty(update.Image11) ? null : folderUploadWWWroot + update.Image11;
                    update.Image12 = string.IsNullOrEmpty(update.Image12) ? null : folderUploadWWWroot + update.Image12;
                    update.Image13 = string.IsNullOrEmpty(update.Image13) ? null : folderUploadWWWroot + update.Image13;
                    update.Image14 = string.IsNullOrEmpty(update.Image14) ? null : folderUploadWWWroot + update.Image14;
                    update.Image15 = string.IsNullOrEmpty(update.Image15) ? null : folderUploadWWWroot + update.Image15;
                    update.Image16 = string.IsNullOrEmpty(update.Image16) ? null : folderUploadWWWroot + update.Image16;
                    update.Image17 = string.IsNullOrEmpty(update.Image17) ? null : folderUploadWWWroot + update.Image17;
                    update.Image18 = string.IsNullOrEmpty(update.Image18) ? null : folderUploadWWWroot + update.Image18;
                    update.Image19 = string.IsNullOrEmpty(update.Image19) ? null : folderUploadWWWroot + update.Image19;
                    update.Image20 = string.IsNullOrEmpty(update.Image20) ? null : folderUploadWWWroot + update.Image20;
                    update.Image21 = string.IsNullOrEmpty(update.Image21) ? null : folderUploadWWWroot + update.Image21;
                    update.Image22 = string.IsNullOrEmpty(update.Image22) ? null : folderUploadWWWroot + update.Image22;
                    update.Image23 = string.IsNullOrEmpty(update.Image23) ? null : folderUploadWWWroot + update.Image23;
                    update.Image24 = string.IsNullOrEmpty(update.Image24) ? null : folderUploadWWWroot + update.Image24;
                    update.Image25 = string.IsNullOrEmpty(update.Image25) ? null : folderUploadWWWroot + update.Image25;
                    update.Image26 = string.IsNullOrEmpty(update.Image26) ? null : folderUploadWWWroot + update.Image26;
                    update.Image27 = string.IsNullOrEmpty(update.Image27) ? null : folderUploadWWWroot + update.Image27;
                    update.Image28 = string.IsNullOrEmpty(update.Image28) ? null : folderUploadWWWroot + update.Image28;
                    update.Image29 = string.IsNullOrEmpty(update.Image29) ? null : folderUploadWWWroot + update.Image29;
                    update.Image30 = string.IsNullOrEmpty(update.Image30) ? null : folderUploadWWWroot + update.Image30;
                    update.Image31 = string.IsNullOrEmpty(update.Image31) ? null : folderUploadWWWroot + update.Image31;
                    update.Image32 = string.IsNullOrEmpty(update.Image32) ? null : folderUploadWWWroot + update.Image32;
                    update.Image33 = string.IsNullOrEmpty(update.Image33) ? null : folderUploadWWWroot + update.Image33;
                    update.Image34 = string.IsNullOrEmpty(update.Image34) ? null : folderUploadWWWroot + update.Image34;
                    update.Image35 = string.IsNullOrEmpty(update.Image35) ? null : folderUploadWWWroot + update.Image35;
                    update.Image36 = string.IsNullOrEmpty(update.Image36) ? null : folderUploadWWWroot + update.Image36;
                    update.Image37 = string.IsNullOrEmpty(update.Image37) ? null : folderUploadWWWroot + update.Image37;
                    update.Image38 = string.IsNullOrEmpty(update.Image38) ? null : folderUploadWWWroot + update.Image38;
                    update.Image39 = string.IsNullOrEmpty(update.Image39) ? null : folderUploadWWWroot + update.Image39;
                    update.Image40 = string.IsNullOrEmpty(update.Image40) ? null : folderUploadWWWroot + update.Image40;
                    update.Image41 = string.IsNullOrEmpty(update.Image41) ? null : folderUploadWWWroot + update.Image41;
                    update.Image42 = string.IsNullOrEmpty(update.Image42) ? null : folderUploadWWWroot + update.Image42;
                    update.Image43 = string.IsNullOrEmpty(update.Image43) ? null : folderUploadWWWroot + update.Image43;
                    update.Image44 = string.IsNullOrEmpty(update.Image44) ? null : folderUploadWWWroot + update.Image44;
                    update.Image45 = string.IsNullOrEmpty(update.Image45) ? null : folderUploadWWWroot + update.Image45;
                    update.Image46 = string.IsNullOrEmpty(update.Image46) ? null : folderUploadWWWroot + update.Image46;
                    update.Image47 = string.IsNullOrEmpty(update.Image47) ? null : folderUploadWWWroot + update.Image47;
                    update.Image48 = string.IsNullOrEmpty(update.Image48) ? null : folderUploadWWWroot + update.Image48;
                    update.Image49 = string.IsNullOrEmpty(update.Image49) ? null : folderUploadWWWroot + update.Image49;
                    update.Image50 = string.IsNullOrEmpty(update.Image50) ? null : folderUploadWWWroot + update.Image50;
                    update.Image51 = string.IsNullOrEmpty(update.Image51) ? null : folderUploadWWWroot + update.Image51;
                    update.Image52 = string.IsNullOrEmpty(update.Image52) ? null : folderUploadWWWroot + update.Image52;
                    update.Image53 = string.IsNullOrEmpty(update.Image53) ? null : folderUploadWWWroot + update.Image53;

                    update.Image54 = string.IsNullOrEmpty(update.Image54) ? null : folderUploadWWWroot + update.Image54;
                    update.Image55 = string.IsNullOrEmpty(update.Image55) ? null : folderUploadWWWroot + update.Image55;
                    update.Image56 = string.IsNullOrEmpty(update.Image56) ? null : folderUploadWWWroot + update.Image56;
                    update.Image57 = string.IsNullOrEmpty(update.Image57) ? null : folderUploadWWWroot + update.Image57;
                    update.Image58 = string.IsNullOrEmpty(update.Image58) ? null : folderUploadWWWroot + update.Image58;
                    update.Image59 = string.IsNullOrEmpty(update.Image59) ? null : folderUploadWWWroot + update.Image59;
                    update.Image60 = string.IsNullOrEmpty(update.Image60) ? null : folderUploadWWWroot + update.Image60;
                    update.Image61 = string.IsNullOrEmpty(update.Image61) ? null : folderUploadWWWroot + update.Image61;
                    update.Image62 = string.IsNullOrEmpty(update.Image62) ? null : folderUploadWWWroot + update.Image62;
                    update.Image63 = string.IsNullOrEmpty(update.Image63) ? null : folderUploadWWWroot + update.Image63;
                    update.Image64 = string.IsNullOrEmpty(update.Image64) ? null : folderUploadWWWroot + update.Image64;
                    update.Image65 = string.IsNullOrEmpty(update.Image65) ? null : folderUploadWWWroot + update.Image65;
                    update.Image66 = string.IsNullOrEmpty(update.Image66) ? null : folderUploadWWWroot + update.Image66;
                    update.Image67 = string.IsNullOrEmpty(update.Image67) ? null : folderUploadWWWroot + update.Image67;

                    update.FileApproveName = string.IsNullOrEmpty(update.FileApproveName) ? null : folderUploadWWWroot + update.FileApproveName;

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


        private string SaveImageOnsite(IFormFile image, string physicalPath, string folderReadFile, string subFolder, string id)
        {
            if (image != null && image.Length > 0)
            {
                if (!Directory.Exists(physicalPath))
                {
                    Directory.CreateDirectory(physicalPath);
                }

                Guid guid = Guid.NewGuid();
                string fileType = "";

                if (image.ContentType == "image/jpeg")
                {
                    fileType = ".jpg";
                }
                else if (image.ContentType == "image/png")
                {
                    fileType = ".png";
                }
                else
                {
                    if (fileType == "")
                    {
                        string extension = image.FileName.Substring(image.FileName.Length - 4);

                        if (extension == ".jpg")
                        {
                            fileType = ".jpg";
                        }
                        else if (extension == ".jpeg")
                        {
                            fileType = ".jpeg";
                        }
                        else if (extension == ".png")
                        {
                            fileType = ".png";
                        }
                        else
                        {
                            return null;
                        }
                    }
                }

                string fileName = $"{guid}{fileType}";
                //string fullPathUrl = Path.Combine(pathUrl, fileName);

                using (var stream = new FileStream(Path.Combine(physicalPath, fileName), FileMode.Create))
                {
                    image.CopyTo(stream);
                }

                return $"{folderReadFile}{subFolder}/{id}/{fileName}";
            }

            return null;
        }


    }
}

