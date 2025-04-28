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
using Microsoft.Extensions.Hosting;
using System.Data;
using Microsoft.IdentityModel.Tokens;

namespace DOL.API.Services
{
    public class MigrateDataService
    {
        private readonly DolContext _context;

        public MigrateDataService(DolContext context)
        {
            _context = context;
        }

        public async Task<Response> SiteInformation(IFormFile? fileUpload)
        {
            Response resp = new Response();

            try
            {
                string uploads = Directory.GetCurrentDirectory() + "/" + "upload/migrate/";

                string pathUpload = "";

                if (!Directory.Exists(uploads))
                {
                    Directory.CreateDirectory(uploads);
                }

                if (fileUpload.Length > 0)
                {
                    var batchNumber = $"I{DateTime.Now.ToString("yyyyMMddHHmmss")}";
                    var fileName = fileUpload.FileName;
                    string filePath = Path.Combine(uploads, fileName);
                    using (Stream fileStream = new FileStream(filePath, FileMode.Create))
                    {
                        await fileUpload.CopyToAsync(fileStream);
                    }
                    pathUpload = filePath;
                }

                if (!string.IsNullOrEmpty(pathUpload))
                {
                    var dtData = AppHelper.ReadExcelFlie(pathUpload);
                    bool isData = false;

                    int idx = 1;

                    var tempQuerySiteNetwork = await Task.Run(() => _context.SiteNetworks.AsQueryable());
                    var tempQuerySiteInformation = await Task.Run(() => _context.SiteInformations.AsQueryable());


                    foreach (DataRow dr in dtData.Rows)
                    {
                        SiteInformation siteInformation = new SiteInformation();

                        if (idx >= 3)
                        {
                            siteInformation.SiteNetworkId = tempQuerySiteNetwork.Where(x => x.Name.Trim().ToLower() == dr["column1"].ToString().Trim()).FirstOrDefault().Id;
                            siteInformation.SiteNetworkName = dr["column1"].ToString();
                            siteInformation.SiteNetworkSeq = Convert.ToInt32(dr["column3"]);
                            siteInformation.ProvinceName = dr["column4"].ToString();
                            siteInformation.LocationName = dr["column5"].ToString();

                            siteInformation.Address = dr["column6"].ToString();
                            siteInformation.StaffOrganize = dr["column7"].ToString();
                            siteInformation.TelephoneNumber = dr["column8"].ToString();
                            siteInformation.Latitude = dr["column9"].ToString();
                            siteInformation.Longitude = dr["column10"].ToString();

                            siteInformation.Wan1Provider = dr["column11"].ToString();
                            siteInformation.Wan1Cid = dr["column12"].ToString();
                            siteInformation.Wan1Speed = dr["column13"].ToString();
                            siteInformation.Wan1AsNumber = dr["column19"].ToString();
                            siteInformation.Wan1IpWan1Pe = dr["column20"].ToString();
                            siteInformation.Wan1IpWan1Ce = dr["column21"].ToString();
                            siteInformation.Wan1Subnet = dr["column22"].ToString();


                            siteInformation.Wan2Provider = dr["column14"].ToString();
                            siteInformation.Wan2Cid = dr["column15"].ToString();
                            siteInformation.Wan2Speed = dr["column16"].ToString();
                            siteInformation.Wan2AsNumber = dr["column23"].ToString();
                            siteInformation.Wan2IpWan1Pe = dr["column24"].ToString();
                            siteInformation.Wan2IpWan1Ce = dr["column25"].ToString();
                            siteInformation.Wan2Subnet = dr["column26"].ToString();


                            siteInformation.InternetCid = dr["column17"].ToString();
                            siteInformation.InternetSpeed = dr["column18"].ToString();
                            siteInformation.InternetAsNumber = dr["column27"].ToString();
                            siteInformation.InternetWanIpAddress = dr["column28"].ToString();
                            siteInformation.InternetSubnet = dr["column29"].ToString();

                            siteInformation.CellularSim = dr["column30"].ToString();
                            siteInformation.CellularAr109 = dr["column31"].ToString();

                            siteInformation.IpLanGateway = dr["column32"].ToString();
                            siteInformation.IpLanSubnet = dr["column33"].ToString();


                            siteInformation.EquipmentCpeSwitchMain = dr["column34"].ToString();
                            siteInformation.EquipmentSnCpeSwitchMain = dr["column35"].ToString();
                            siteInformation.EquipmentCpeSwitchSecondary = dr["column36"].ToString();
                            siteInformation.EquipmentSnCpeSwitchSecondary = dr["column37"].ToString();
                            siteInformation.EquipmentFirewall2Set = dr["column38"].ToString();
                            siteInformation.EquipmentFirewall1Sn = dr["column39"].ToString();
                            siteInformation.EquipmentFirewall2Sn = dr["column40"].ToString();
                            siteInformation.EquipmentRouter2Set = dr["column41"].ToString();
                            siteInformation.EquipmentRouter1Sn = dr["column42"].ToString();
                            siteInformation.EquipmentRouter2Sn = dr["column43"].ToString();
                            siteInformation.EquipmentWifi1Set = dr["column44"].ToString();
                            siteInformation.EquipmentWifiSn = dr["column45"].ToString();
                            siteInformation.EquipmentRouter4gSet = dr["column46"].ToString();
                            siteInformation.EquipmentRouter4gSn = dr["column47"].ToString();


                            siteInformation.MainMaxHour = dr["column48"].ToString() == "-" ? 0 : dr["column48"].ToString() == null ? 0 : dr["column48"].ToString() == "" ? 0 : Convert.ToDouble(dr["column48"].ToString());
                            siteInformation.MainMonthlyServiceFee = dr["column49"].ToString() == "-" ? 0 : dr["column49"].ToString() == null ? 0 : dr["column49"].ToString() == "" ? 0 : Convert.ToDouble(dr["column49"].ToString());
                            siteInformation.MainFineRateHourPercent = dr["column50"].ToString() == "-" ? 0 : dr["column50"].ToString() == null ? 0 : dr["column50"].ToString() == "" ? 0 : Convert.ToDouble(dr["column50"].ToString());
                            siteInformation.MainFineRateHourAmount = dr["column51"].ToString() == "-" ? 0 : dr["column51"].ToString() == null ? 0 : dr["column51"].ToString() == "" ? 0 : Convert.ToDouble(dr["column51"].ToString());
                            siteInformation.MainFineRateMinimum = dr["column52"].ToString() == "-" ? 0 : dr["column52"].ToString() == null ? 0 : dr["column52"].ToString() == "" ? 0 : Convert.ToDouble(dr["column52"].ToString());


                            siteInformation.SecondaryMaxHour = dr["column53"].ToString() == "-" ? 0 : dr["column53"].ToString() == null ? 0 : dr["column53"].ToString() == "" ? 0 : Convert.ToDouble(dr["column53"].ToString());
                            siteInformation.SecondaryMonthlyServiceFee = dr["column54"].ToString() == "-" ? 0 : dr["column54"].ToString() == null ? 0 : dr["column54"].ToString() == "" ? 0 : Convert.ToDouble(dr["column54"].ToString());
                            siteInformation.SecondaryFineRateHourPercent = dr["column55"].ToString() == "-" ? 0 : dr["column55"].ToString() == null ? 0 : dr["column55"].ToString() == "" ? 0 : Convert.ToDouble(dr["column55"].ToString());
                            siteInformation.SecondaryFineRateHourAmount = dr["column56"].ToString() == "-" ? 0 : dr["column56"].ToString() == null ? 0 : dr["column56"].ToString() == "" ? 0 : Convert.ToDouble(dr["column56"].ToString());
                            siteInformation.SecondaryFineRateMinimum = dr["column57"].ToString() == "-" ? 0 : dr["column57"].ToString() == null ? 0 : dr["column57"].ToString() == "" ? 0 : Convert.ToDouble(dr["column57"].ToString());


                            siteInformation.InternetMaxHour = dr["column58"].ToString() == "-" ? 0 : dr["column58"].ToString() == null ? 0 : dr["column58"].ToString() == "" ? 0 : Convert.ToDouble(dr["column58"].ToString());
                            siteInformation.InternetMonthlyServiceFee = dr["column59"].ToString() == "-" ? 0 : dr["column59"].ToString() == null ? 0 : dr["column59"].ToString() == "" ? 0 : Convert.ToDouble(dr["column59"].ToString());
                            siteInformation.InternetFineRateHourPercent = dr["column60"].ToString() == "-" ? 0 : dr["column60"].ToString() == null ? 0 : dr["column60"].ToString() == "" ? 0 : Convert.ToDouble(dr["column60"].ToString());
                            siteInformation.InternetFineRateHourAmount = dr["column61"].ToString() == "-" ? 0 : dr["column61"].ToString() == null ? 0 : dr["column61"].ToString() == "" ? 0 : Convert.ToDouble(dr["column61"].ToString());
                            siteInformation.InternetFineRateMinimum = dr["column62"].ToString() == "-" ? 0 : dr["column62"].ToString() == null ? 0 : dr["column62"].ToString() == "" ? 0 : Convert.ToDouble(dr["column62"].ToString());


                            siteInformation.CorpnetMaxHour = dr["column63"].ToString() == "-" ? 0 : dr["column63"].ToString() == null ? 0 : dr["column63"].ToString() == "" ? 0 : Convert.ToDouble(dr["column63"].ToString());
                            siteInformation.CorpnetMonthlyServiceFee = dr["column64"].ToString() == "-" ? 0 : dr["column64"].ToString() == null ? 0 : dr["column64"].ToString() == "" ? 0 : Convert.ToDouble(dr["column64"].ToString());
                            siteInformation.CorpnetFineRateHourPercent = dr["column65"].ToString() == "-" ? 0 : dr["column65"].ToString() == null ? 0 : dr["column65"].ToString() == "" ? 0 : Convert.ToDouble(dr["column65"].ToString());
                            siteInformation.CorpnetFineRateHourAmount = dr["column66"].ToString() == "-" ? 0 : dr["column66"].ToString() == null ? 0 : dr["column66"].ToString() == "" ? 0 : Convert.ToDouble(dr["column66"].ToString());
                            siteInformation.CorpnetFineRateMinimum = dr["column67"].ToString() == "-" ? 0 : dr["column67"].ToString() == null ? 0 : dr["column67"].ToString() == "" ? 0 : Convert.ToDouble(dr["column67"].ToString());


                            siteInformation.CellularMaxHour = dr["column68"].ToString() == "-" ? 0 : dr["column68"].ToString() == null ? 0 : dr["column68"].ToString() == "" ? 0 : Convert.ToDouble(dr["column68"].ToString());
                            siteInformation.CellularMonthlyServiceFee = dr["column69"].ToString() == "-" ? 0 : dr["column69"].ToString() == null ? 0 : dr["column69"].ToString() == "" ? 0 : Convert.ToDouble(dr["column69"].ToString());
                            siteInformation.CellularFineRateHourPercent = dr["column70"].ToString() == "-" ? 0 : dr["column70"].ToString() == null ? 0 : dr["column70"].ToString() == "" ? 0 : Convert.ToDouble(dr["column70"].ToString());
                            siteInformation.CellularFineRateHourAmount = dr["column71"].ToString() == "-" ? 0 : dr["column71"].ToString() == null ? 0 : dr["column71"].ToString() == "" ? 0 : Convert.ToDouble(dr["column71"].ToString());
                            siteInformation.CellularFineRateMinimum = dr["column72"].ToString() == "-" ? 0 : dr["column72"].ToString() == null ? 0 : dr["column72"].ToString() == "" ? 0 : Convert.ToDouble(dr["column72"].ToString());

                            siteInformation.Guid = dr["column79"].ToString();


                            //siteInformation.SumFineRatePercent = dr["column73"].ToString() == null ? 0 : dr["column73"].ToString() == "" ? 0 : Convert.ToDouble(dr["column73"].ToString());
                            //siteInformation.SumPrincipalAmount = dr["column74"].ToString() == null ? 0 : dr["column74"].ToString() == "" ? 0 : Convert.ToDouble(dr["column74"].ToString());
                            //siteInformation.SumDate = dr["column75"].ToString() == null ? 0 : dr["column75"].ToString() == "" ? 0 : Convert.ToDouble(dr["column75"].ToString());
                            //siteInformation.SumAmount = dr["column76"].ToString() == null ? 0 : dr["column76"].ToString() == "" ? 0 : Convert.ToDouble(dr["column76"].ToString());

                            var getTeamName = dr["column73"].ToString();

                            siteInformation.SysUserId = string.IsNullOrEmpty(getTeamName) ? null : _context.SysUsers.Where(x => x.Name.ToLower().Trim().Contains(getTeamName.ToLower().Trim())).FirstOrDefault().Id;

                            siteInformation.CreateBy = "chet";
                            siteInformation.CreateDate = DateTime.Now;
                            siteInformation.UpdateBy = "chet";
                            siteInformation.UpdateDate = DateTime.Now;
                            siteInformation.IsActive = true;

                            //var findOldRecord = _context.SiteInformations.Where(x =>
                            //x.SiteNetworkId == siteInformation.SiteNetworkId &&
                            //x.SiteNetworkName == siteInformation.SiteNetworkName &&
                            //x.SiteNetworkSeq == siteInformation.SiteNetworkSeq &&
                            //x.ProvinceName == siteInformation.ProvinceName &&
                            //x.LocationName == siteInformation.LocationName
                            //).FirstOrDefault();

                            var findOldRecord = _context.SiteInformations.Where(x => x.Guid == siteInformation.Guid).FirstOrDefault();

                            if (findOldRecord == null)
                            {
                                // Create

                                siteInformation.Id = 0;

                                await Task.Run(() => _context.SiteInformations.Add(siteInformation));

                                _context.SaveChanges();

                            }
                            else
                            {
                                // Update

                                var update = _context.SiteInformations.Where(x => x.Id == findOldRecord.Id).FirstOrDefault();

                                List<string> columnNotUpdate = new List<string>();

                                columnNotUpdate.Add("Id");
                                //columnNotUpdate.Add("StaffOrganize");
                                //columnNotUpdate.Add("TelephoneNumber");

                                for (int i = 1; i <= 28; i++)
                                {
                                    columnNotUpdate.Add("Image" + i);
                                }

                                columnNotUpdate.Add("TeamInstallContactName");
                                columnNotUpdate.Add("TeamInstallContactTel");
                                columnNotUpdate.Add("FileApproveName");
                                columnNotUpdate.Add("SysStatusId");

                                columnNotUpdate.Add("Wan1SpeedTestDownload");
                                columnNotUpdate.Add("Wan1SpeedTestUpload");
                                columnNotUpdate.Add("Wan2SpeedTestDownload");
                                columnNotUpdate.Add("Wan2SpeedTestUpload");


                                AppHelper.TransferData_ClassA_to_ClassB<SiteInformation, SiteInformation>(siteInformation, ref update, columnNotUpdate);


                                _context.SaveChanges();
                            }

                        }

                        idx++;
                    }

                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusSuccess;
                    resp.statusCode = Constants.statusCodeOK;
                    resp.effectRow = idx;

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
    }
}


