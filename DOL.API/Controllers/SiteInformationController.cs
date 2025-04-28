using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DOL.API.Extension.Helper;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs.Request;
using DOL.API.Models.Customs.Response;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories;
using DOL.API.Repositories.Interface;
using DOL.API.Services.GenerateDocument;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Document = iTextSharp.text.Document;
using System.IO;
using System.Text;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;
using iTextSharp.text;
using MySqlX.XDevAPI.Common;
using DOL.API.Services.Helper;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DOL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class SiteInformationController : Controller
    {
        private readonly ISiteInformationRepo repoCollection;

        public SiteInformationController()
        {
            this.repoCollection = new SiteInformationRepo();
        }


        [HttpGet]
        [Route("Schedule")]
        public async Task<IActionResult> Schedule([FromQuery] SiteInformationFilter param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Schedule(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = ex.InnerException == null ? ex.Message : ex.InnerException.Message.ToString();
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        }

        [HttpGet]
        [Route("Overview")]
        public async Task<IActionResult> Overview([FromQuery] SiteInformationFilter param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Overview(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        }

        [HttpGet]
        [Route("CardJobs")]
        public async Task<IActionResult> CardJobs([FromQuery] SiteInformationFilter param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await repoCollection.CardJobs(param);

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] SiteInformationFilter param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Get(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        }

        // GET api/values/5
        [HttpGet]
        [Route("{id}")]
        public async Task<IActionResult> Detail(int id)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Detail(id));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        }

        [HttpGet]
        [Route("Province")]
        public async Task<IActionResult> Province()
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Province());

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        }

        [HttpGet]
        [Route("Location")]
        public async Task<IActionResult> Location([FromQuery] string province)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Location(province));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));

        }

        // PUT api/values/5
        [HttpPatch]
        public async Task<IActionResult> Update([FromBody] SiteInformationRequest param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Update(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        }


        [HttpPatch]
        [Route("UpdateImage")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImage([FromForm] UpdateImageRequest param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.UpdateImage(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        }


        [HttpPatch]
        [Route("UpdateImages")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> UpdateImages([FromForm] UpdateImageListRequest param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.UpdateImages(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        }


        [HttpPatch]
        [Route("DeleteImage")]
        public async Task<IActionResult> DeleteImage([FromBody] DeleteImageRequest param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.DeleteImage(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        }

        [HttpPatch]
        [Route("UpdateImageName")]
        public async Task<IActionResult> UpdateImageName([FromBody] UpdateImageNameRequest param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.UpdateImageName(param));

                watch.Stop();

                result.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
            }
            catch (Exception ex)
            {
                result.httpCode = Constants.httpCode500;
                result.status = Constants.statusError;
                result.statusCode = Constants.statusCodeException;
                result.message = Constants.httpCode500Message;
            }

            return StatusCode(result.httpCode, AppHelper.GetResponseController(result));
        }

        [HttpGet]
        [Route("GeneratePdf")]
        public async Task<IActionResult> GeneratePdf(int id)
        {
            Response resp = new Response();

            SiteInformationResponse result = new SiteInformationResponse();

            resp = await Task.Run(() => repoCollection.Detail(id));

            if (resp.status == true)
            {
                var jsonData = JsonConvert.SerializeObject(resp.data);

                result = JsonConvert.DeserializeObject<SiteInformationResponse>(jsonData);

                if (result != null)
                {
                    SiteNetworkPaper generateLicense = new SiteNetworkPaper();

                    var document = await Task.Run(() => generateLicense.GetnerateDocument(result));

                    if (document.httpCode == Constants.httpCode200)
                    {
                        return File((byte[])document.data, "image/jpeg");
                    }
                    else
                    {
                        return StatusCode(document.httpCode, AppHelper.GetResponseController(document));
                    }

                }
            }

            resp.httpCode = Constants.httpCode500;
            resp.status = Constants.statusError;
            resp.statusCode = Constants.statusCodeException;
            resp.message = Constants.httpCode500Message;

            return StatusCode(resp.httpCode, AppHelper.GetResponseController(resp));

        }


        [HttpGet]
        [Route("GeneratePdfByUser")]
        public async Task<IActionResult> GeneratePdfByUser(int userId)
        {
            Response resp = new Response();

            List<SiteInformationResponse> result = new List<SiteInformationResponse>();

            SiteInformationFilter filter = new SiteInformationFilter();

            filter.SysUserId = userId;
            filter.isAll = true;

            resp = await Task.Run(() => repoCollection.Get(filter));

            if (resp.status == true)
            {
                var jsonData = JsonConvert.SerializeObject(resp.data);

                result = JsonConvert.DeserializeObject<List<SiteInformationResponse>>(jsonData);

                if (result != null && result.Any())
                {
                    using (MemoryStream zipStream = new MemoryStream())
                    {
                        var zipName = "";

                        using (ZipArchive archive = new ZipArchive(zipStream, ZipArchiveMode.Create, true))
                        {

                            foreach (var item in result)
                            {
                                zipName = $"รายงานผลการติดตั้งและทดสอบเครือข่ายสื่อสาร-{item.SysUser.Username.ToUpper()}";

                                SiteNetworkPaper generateLicense = new SiteNetworkPaper();
                                var document = await Task.Run(() => generateLicense.GetnerateDocument(item));

                                if (document.httpCode == Constants.httpCode200)
                                {
                                    string fileName = $"รายงานผลการติดตั้งและทดสอบเครือข่ายสื่อสาร-{item.SysUser.Username.ToUpper()}_{item.LocationName}.jpeg"; // Ensure unique filename
                                    ZipArchiveEntry entry = archive.CreateEntry(fileName);
                                    using (Stream entryStream = entry.Open())
                                    {
                                        entryStream.Write((byte[])document.data, 0, ((byte[])document.data).Length);
                                    }
                                }
                                else
                                {

                                }
                            }

                        }

                        // Return the zip file as a response
                        return File(zipStream.ToArray(), "application/zip", zipName);
                    }
                }

                // Return something if there's no result
                return NotFound();

            }


            resp.httpCode = Constants.httpCode500;
            resp.status = Constants.statusError;
            resp.statusCode = Constants.statusCodeException;
            resp.message = Constants.httpCode500Message;

            return StatusCode(resp.httpCode, AppHelper.GetResponseController(resp));


        }

        [HttpGet]
        [Route("GenerateOnsitePdf")]
        public async Task<IActionResult> GenerateOnsitePdf(int id)
        {
            Response resp = new Response();

            SiteInformationResponse item = new SiteInformationResponse();

            try
            {
                resp = await Task.Run(() => repoCollection.Detail(id));

                if (resp.status == true)
                {
                    string? folderReadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiAppRootExportUrl"];

                    var jsonData = JsonConvert.SerializeObject(resp.data);

                    item = JsonConvert.DeserializeObject<SiteInformationResponse>(jsonData);

                    if (item != null)
                    {
                        if (item.SysStatusId != 4)
                        {
                            resp.httpCode = Constants.httpCode200;
                            resp.status = Constants.statusError;
                            resp.statusCode = Constants.statusCodeParamInvalid;
                            resp.message = "สถานะเอกสารไม่พร้อมใช้งาน กรุณาตรวจสอบสถานะงาน Onsite ก่อนทำรายการใหม่อีกครั้ง";
                            resp.data = null;
                            resp.effectRow = null;

                            return StatusCode(resp.httpCode, AppHelper.GetResponseController(resp));
                        }

                        string fBase = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "fonts", "THSarabunNew", "THSarabunNew.ttf");
                        string fBold = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "fonts", "THSarabunNew", "THSarabunNew Bold.ttf");
                        string fItalic = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "fonts", "THSarabunNew", "THSarabunNew Italic.ttf");

                        System.Text.EncodingProvider encodingProvider = System.Text.CodePagesEncodingProvider.Instance;
                        Encoding.RegisterProvider(encodingProvider);

                        BaseFont fontDefault = BaseFont.CreateFont(fBase, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        BaseFont fontBold = BaseFont.CreateFont(fBold, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);
                        BaseFont fontitalic = BaseFont.CreateFont(fItalic, BaseFont.IDENTITY_H, BaseFont.EMBEDDED);

                        Font Default = new Font(fontDefault, 14);
                        Font Bold = new Font(fontBold, 16);
                        Font Italic = new Font(fontitalic, 14);

                        Document document = new Document(PageSize.A4, 30, 30, 20, 20);
                        MemoryStream memoryStream = new MemoryStream();
                        PdfWriter writer = PdfWriter.GetInstance(document, memoryStream);

                        document.Open();

                        #region Image name

                        string imageName1 = "รูปหน้าสำนักงาน";
                        string imageName2 = "Rack ที่สำนักงาน";
                        string imageName3 = "CPE switch วงจรหลัก";
                        string imageName4 = "CPE switch วงจรรอง";
                        string imageName5 = "Firewall1";
                        string imageName6 = "Firewall2";
                        string imageName7 = "Router1";
                        string imageName8 = "Router2";
                        string imageName9 = "Wifi 1 ชุด";
                        string imageName10 = "Router 4G 1 ชุด";
                        string imageName11 = "AR109";
                        string imageName12 = "Ping Test 1";
                        string imageName13 = "Ping Test 2";
                        string imageName14 = "Ping Test 3";
                        string imageName15 = "Ping Test 4";
                        string imageName16 = "เข้าเว็บไซต์อินเตอร์เน็ตกรมที่ดิน";
                        string imageName17 = "เข้า FTP กรมที่ดิน";
                        string imageName18 = "เข้าระบบผู้รับมอบอำนาจ";
                        string imageName19 = "เข้าระบบ MIS";
                        string imageName20 = "เข้าระบบควบคุมการจัดเก็บหลักฐาน";
                        string imageName21 = "เข้าโปรแกรมบริการกรมที่ดิน";
                        string imageName22 = "ระบบพิสูจน์ตัวตนในการใช้งานเครือข่าย";
                        string imageName23 = "WAN1 (วงจรหลัก)";
                        string imageName24 = "WAN2 (วงจรรอง)";
                        string imageName25 = "";
                        string imageName26 = "";
                        string imageName27 = "";
                        string imageName28 = "";
                        string imageName29 = "ทดสอบ การใช้งาน Wifi : Network Connection";
                        string imageName30 = "ทดสอบ Authentication";
                        string imageName31 = "ภาพ ping www.dol.go.th -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)";
                        string imageName32 = "ภาพ tracert www.dol.go.th (วงจรหลัก)";
                        string imageName33 = "ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรหลัก)";
                        string imageName34 = "ภาพ ping ilands.dol.go.th -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)";
                        string imageName35 = "ภาพ tracert ilands.dol.go.th (วงจรหลัก)";
                        string imageName36 = "ภาพ ping ilands.dol.go.th -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)";
                        string imageName37 = "ภาพ ping 10.200.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)";
                        string imageName38 = "ภาพ tracert 10.200.30.247 (วงจรหลัก)";
                        string imageName39 = "ภาพ ping 8.8.8.8 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)";
                        string imageName40 = "ภาพ tracert 8.8.8.8 (วงจรหลัก)";
                        string imageName41 = "ภาพ ping www.dol.go.th -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรรอง)";
                        string imageName42 = "ภาพ tracert www.dol.go.th (วงจรรอง)";
                        string imageName43 = "ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรรอง)";
                        string imageName44 = "ภาพ ping ilands.dol.go.th -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรรอง)";
                        string imageName45 = "ภาพ tracert ilands.dol.go.th (วงจรรอง)";
                        string imageName46 = "ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรรอง)";
                        string imageName47 = "ภาพ ping 10.200.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรรอง)";
                        string imageName48 = "ภาพ tracert 10.200.30.247 (วงจรรอง)";
                        string imageName49 = "ภาพ ping 8.8.8.8 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรรอง)";
                        string imageName50 = "ภาพ tracert 8.8.8.8 (วงจรรอง)";
                        string imageName51 = "ภาพการทดสอบรับ IP Address";
                        string imageName52 = "วงจร Internet";
                        string imageName53 = "วงจร 4G (20Mbps) โดยปลดสาย  WAN1 วงจรหลัก, WAN2 วงจรรอง และ วงจร Internet";
                        string imageName54 = "ภาพสถานะวงจรที่จะทดสอบ Nagios ";
                        string imageName55 = "ภาพเริ่มทดสอบวงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร";
                        string imageName56 = "ภาพ ping 8.8.8.8 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)";
                        string imageName57 = "ภาพ tracert 8.8.8.8 (วงจรสื่อสารอินเทอร์เน็ตประเภทองค์กร)";
                        string imageName58 = "ภาพเริ่มทดสอบวงจรหลัก";
                        string imageName59 = "วงจรหลัก Tunnel Destination DC1";
                        string imageName60 = "วงจรหลัก Tunnel Destination DC2";
                        string imageName61 = "ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรหลัก)";
                        string imageName62 = "ภาพ tracert 10.100.30.247 (วงจรหลัก)";
                        string imageName63 = "ภาพเริ่มทดสอบวงจรรอง";
                        string imageName64 = "วงจรสำรอง Tunnel Destination DC1";
                        string imageName65 = "วงจรสำรอง Tunnel Destination DC2";
                        string imageName66 = "ภาพ ping 10.100.30.247 -t 30 (response time 100 ms, ต้องไม่มี Time Out) (วงจรสำรอง)";
                        string imageName67 = "ภาพ tracert 10.100.30.247 (วงจรสำรอง)";

                        #endregion

                        #region Create Document
                        int imageCount = 0;

                        if (item.SiteNetworkId == 2)
                        {
                            for (int i = 0; i <= 41; i++)
                            {
                                try
                                {
                                    PdfPTable table = new PdfPTable(1);
                                    table.WidthPercentage = 100;
                                    table.DefaultCell.Border = Rectangle.NO_BORDER;

                                    if (i == 1)
                                    {
                                        PdfPCell cellNumber = new PdfPCell(new Phrase($"{item.SiteNetworkSeq}", Bold));
                                        cellNumber.HorizontalAlignment = Element.ALIGN_RIGHT; // Align cell content to center
                                        cellNumber.Border = Rectangle.NO_BORDER;



                                        PdfPCell cell1 = new PdfPCell(new Phrase($"รายงานผลการติดตั้ง และทดสอบเครือข่ายสื่อสาร", Bold));
                                        cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                        cell1.Border = Rectangle.NO_BORDER;
                                        //cell1.PaddingTop = 10f;

                                        PdfPCell cell2 = new PdfPCell(new Phrase($"โครงการเช่าใช้บริการเครือข่ายการสื่อสารกรมที่ดิน", Bold));
                                        cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                        cell2.Border = Rectangle.NO_BORDER;
                                        cell2.PaddingTop = 5f;



                                        PdfPTable innerTable = new PdfPTable(6);
                                        innerTable.WidthPercentage = 100;
                                        float[] columnWidths = { 0.15f, 0.4f, 0.15f, 0.1f, 0.1f, 0.2f };
                                        innerTable.SetWidths(columnWidths);

                                        innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                                        innerTable.DefaultCell.BorderWidth = 0;
                                        innerTable.HorizontalAlignment = 0;
                                        innerTable.SpacingAfter = 10;
                                        innerTable.PaddingTop = 20f;


                                        PdfPCell locationHeader = new PdfPCell(new Phrase($"ชื่อหน่วยงาน : ", Bold));
                                        locationHeader.Border = Rectangle.NO_BORDER;

                                        PdfPCell locationName = new PdfPCell(new Phrase($"{item.LocationName}", Default));
                                        locationName.PaddingTop = 5f;
                                        locationName.Border = Rectangle.NO_BORDER;

                                        PdfPCell siteNetworkHeader = new PdfPCell(new Phrase($"ภาคผนวก : ", Bold));
                                        siteNetworkHeader.Border = Rectangle.NO_BORDER;

                                        PdfPCell siteNetworkName = new PdfPCell(new Phrase($"{item.SiteNetworkName}", Default));
                                        siteNetworkName.PaddingTop = 5f;
                                        siteNetworkName.Border = Rectangle.NO_BORDER;

                                        PdfPCell provinceHeader = new PdfPCell(new Phrase($"จังหวัด : ", Bold));
                                        provinceHeader.Border = Rectangle.NO_BORDER;

                                        PdfPCell provinceName = new PdfPCell(new Phrase($"{item.ProvinceName}", Default));
                                        provinceName.PaddingTop = 5f;
                                        provinceName.Border = Rectangle.NO_BORDER;

                                        // Add a row
                                        innerTable.AddCell(locationHeader);
                                        innerTable.AddCell(locationName);

                                        innerTable.AddCell(siteNetworkHeader);
                                        innerTable.AddCell(siteNetworkName);

                                        innerTable.AddCell(provinceHeader);
                                        innerTable.AddCell(provinceName);


                                        table.AddCell(cellNumber);
                                        table.AddCell(cell1);
                                        table.AddCell(cell2);
                                        table.AddCell(innerTable);



                                    }

                                    if (i == 2)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image54))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName54, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image54;
                                            Image image = Image.GetInstance(imagePath);


                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 3)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image51))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName51, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image51;
                                            Image image = Image.GetInstance(imagePath);


                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 4)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image30))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName30, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image30;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 5)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image55))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName55, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image55;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 6)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image52))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName52, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image52;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 7)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image56))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName56, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image56;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 8)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image57))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName57, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image57;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 9)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image58))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName58, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image58;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 10)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image59))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName59, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image59;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 11)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image60))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName60, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image60;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 12)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image23))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName23, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image23;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 13)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image33))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName33, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image33;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 14)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image31))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName31, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image31;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 15)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image32))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName32, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image32;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 16)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image36))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName36, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image36;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 17)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image34))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName34, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image34;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 18)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image35))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName35, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image35;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 19)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image61))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName61, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image61;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 20)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image62))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName62, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image62;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 21)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image37))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName37, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image37;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 22)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image38))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName38, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image38;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 23)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image39))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName39, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image39;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 24)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image40))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName40, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image40;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 25)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image63))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName63, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image63;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 26)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image64))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName64, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image64;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 27)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image65))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName65, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image65;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 28)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image24))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName24, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image24;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 29)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image43))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName43, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image43;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 30)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image41))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName41, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image41;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 31)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image42))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName42, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image42;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 32)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image46))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName46, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image46;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 33)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image44))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName44, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image44;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 34)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image45))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName45, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image45;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 35)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image66))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName66, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image66;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 36)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image67))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName67, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image67;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 37)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image47))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName47, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image47;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 38)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image48))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName48, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image48;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 39)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image49))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName49, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image49;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 40)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image50))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName50, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image50;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 41)
                                    {
                                        if (!string.IsNullOrEmpty(item.FileApproveName))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase($"เอกสารรับมอบการติดตั้ง", Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.FileApproveName;
                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }
                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    document.Add(table);

                                    if (i != 1)
                                    {
                                        if (imageCount == 2)
                                        {
                                            document.NewPage();

                                            imageCount = 0;
                                        }
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }
                        else
                        {
                            for (int i = 0; i <= 33; i++)
                            {
                                try
                                {
                                    PdfPTable table = new PdfPTable(1);
                                    table.WidthPercentage = 100;
                                    table.DefaultCell.Border = Rectangle.NO_BORDER;


                                    if (i == 1)
                                    {
                                        PdfPCell cellNumber = new PdfPCell(new Phrase($"{item.SiteNetworkSeq}", Bold));
                                        cellNumber.HorizontalAlignment = Element.ALIGN_RIGHT; // Align cell content to center
                                        cellNumber.Border = Rectangle.NO_BORDER;



                                        PdfPCell cell1 = new PdfPCell(new Phrase($"รายงานผลการติดตั้ง และทดสอบเครือข่ายสื่อสาร", Bold));
                                        cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                        cell1.Border = Rectangle.NO_BORDER;
                                        //cell1.PaddingTop = 10f;

                                        PdfPCell cell2 = new PdfPCell(new Phrase($"โครงการเช่าใช้บริการเครือข่ายการสื่อสารกรมที่ดิน", Bold));
                                        cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                        cell2.Border = Rectangle.NO_BORDER;
                                        cell2.PaddingTop = 5f;



                                        PdfPTable innerTable = new PdfPTable(6);
                                        innerTable.WidthPercentage = 100;
                                        float[] columnWidths = { 0.15f, 0.4f, 0.15f, 0.1f, 0.1f, 0.2f };
                                        innerTable.SetWidths(columnWidths);

                                        innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                                        innerTable.DefaultCell.BorderWidth = 0;
                                        innerTable.HorizontalAlignment = 0;
                                        innerTable.SpacingAfter = 10;
                                        innerTable.PaddingTop = 20f;


                                        PdfPCell locationHeader = new PdfPCell(new Phrase($"ชื่อหน่วยงาน : ", Bold));
                                        locationHeader.Border = Rectangle.NO_BORDER;

                                        PdfPCell locationName = new PdfPCell(new Phrase($"{item.LocationName}", Default));
                                        locationName.PaddingTop = 5f;
                                        locationName.Border = Rectangle.NO_BORDER;

                                        PdfPCell siteNetworkHeader = new PdfPCell(new Phrase($"ภาคผนวก : ", Bold));
                                        siteNetworkHeader.Border = Rectangle.NO_BORDER;

                                        PdfPCell siteNetworkName = new PdfPCell(new Phrase($"{item.SiteNetworkName}", Default));
                                        siteNetworkName.PaddingTop = 5f;
                                        siteNetworkName.Border = Rectangle.NO_BORDER;

                                        PdfPCell provinceHeader = new PdfPCell(new Phrase($"จังหวัด : ", Bold));
                                        provinceHeader.Border = Rectangle.NO_BORDER;

                                        PdfPCell provinceName = new PdfPCell(new Phrase($"{item.ProvinceName}", Default));
                                        provinceName.PaddingTop = 5f;
                                        provinceName.Border = Rectangle.NO_BORDER;

                                        // Add a row
                                        innerTable.AddCell(locationHeader);
                                        innerTable.AddCell(locationName);

                                        innerTable.AddCell(siteNetworkHeader);
                                        innerTable.AddCell(siteNetworkName);

                                        innerTable.AddCell(provinceHeader);
                                        innerTable.AddCell(provinceName);


                                        table.AddCell(cellNumber);
                                        table.AddCell(cell1);
                                        table.AddCell(cell2);
                                        table.AddCell(innerTable);



                                    }

                                    if (i == 2)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image54))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName54, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image54;
                                            Image image = Image.GetInstance(imagePath);


                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 3)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image51))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName51, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image51;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 4)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image30))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName30, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image30;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 5)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image58))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName58, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image58;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 6)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image23))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName23, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image23;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 7)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image33))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName33, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image33;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 8)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image31))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName31, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image31;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 9)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image32))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName32, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image32;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 10)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image36))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName36, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image36;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 11)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image34))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName34, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image34;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 12)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image35))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName35, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image35;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 13)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image61))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName61, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image61;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 14)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image62))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName62, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image62;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 15)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image37))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName37, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image37;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 16)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image38))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName38, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image38;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 17)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image39))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName39, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image39;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 18)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image40))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName40, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image40;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 19)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image63))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName63, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image63;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 20)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image24))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName24, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image24;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 21)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image43))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName43, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image43;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 22)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image41))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName41, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image41;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 23)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image42))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName42, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image42;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 24)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image46))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName46, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image46;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 25)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image44))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName44, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image44;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 26)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image45))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName45, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image45;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 27)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image66))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName66, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image66;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 28)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image67))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName67, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image67;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 29)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image47))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName47, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image47;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 30)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image48))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName48, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image48;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 31)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image49))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName49, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image49;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 32)
                                    {
                                        if (!string.IsNullOrEmpty(item.Image50))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase(imageName50, Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;
                                            //cell1.PaddingTop = 280f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.Image50;

                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }

                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    if (i == 33)
                                    {
                                        if (!string.IsNullOrEmpty(item.FileApproveName))
                                        {
                                            PdfPCell cell1 = new PdfPCell(new Phrase($"เอกสารรับมอบการติดตั้ง", Bold));
                                            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell1.Border = Rectangle.NO_BORDER;
                                            cell1.PaddingTop = 20f;

                                            PdfPCell cell2 = new PdfPCell();
                                            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                            cell2.Border = Rectangle.NO_BORDER;
                                            cell2.PaddingTop = 10f;

                                            string imagePath = item.FileApproveName;
                                            Image image = Image.GetInstance(imagePath);

                                            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                                            if (imageInfo.Height > imageInfo.Width)
                                            {
                                                if (image.Width > image.Height)
                                                {
                                                    image.RotationDegrees = -90;
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                                else
                                                {
                                                    image.ScaleAbsolute(250f, 250f);
                                                }
                                            }
                                            else
                                            {
                                                image.ScaleAbsolute(400f, 400f);
                                            }
                                            image.Alignment = Element.ALIGN_CENTER;
                                            cell2.AddElement(image);

                                            table.AddCell(cell1);
                                            table.AddCell(cell2);

                                            imageCount++;
                                        }
                                    }

                                    document.Add(table);

                                    if (i != 1)
                                    {
                                        if (imageCount == 2)
                                        {
                                            document.NewPage();

                                            imageCount = 0;
                                        }
                                    }
                                }
                                catch
                                {
                                    continue;
                                }
                            }
                        }

                        //for (int i = 1; i <= 38; i++)
                        //{
                        //    PdfPTable table = new PdfPTable(1);
                        //    table.WidthPercentage = 100;
                        //    table.DefaultCell.Border = Rectangle.NO_BORDER;

                        //    if (i == 1)
                        //    {
                        //        PdfPCell cellNumber = new PdfPCell(new Phrase($"{item.SiteNetworkSeq}", Bold));
                        //        cellNumber.HorizontalAlignment = Element.ALIGN_RIGHT; // Align cell content to center
                        //        cellNumber.Border = Rectangle.NO_BORDER;



                        //        PdfPCell cell1 = new PdfPCell(new Phrase($"รายงานผลการติดตั้ง และทดสอบเครือข่ายสื่อสาร", Bold));
                        //        cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //        cell1.Border = Rectangle.NO_BORDER;
                        //        //cell1.PaddingTop = 10f;

                        //        PdfPCell cell2 = new PdfPCell(new Phrase($"โครงการเช่าใช้บริการเครือข่ายการสื่อสารกรมที่ดิน", Bold));
                        //        cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //        cell2.Border = Rectangle.NO_BORDER;
                        //        cell2.PaddingTop = 5f;



                        //        PdfPTable innerTable = new PdfPTable(6);
                        //        innerTable.WidthPercentage = 100;
                        //        float[] columnWidths = { 0.15f, 0.4f, 0.15f, 0.1f, 0.1f, 0.2f };
                        //        innerTable.SetWidths(columnWidths);

                        //        innerTable.DefaultCell.Border = Rectangle.NO_BORDER;
                        //        innerTable.DefaultCell.BorderWidth = 0;
                        //        innerTable.HorizontalAlignment = 0;
                        //        innerTable.SpacingAfter = 10;
                        //        innerTable.PaddingTop = 20f;


                        //        PdfPCell locationHeader = new PdfPCell(new Phrase($"ชื่อหน่วยงาน : ", Bold));
                        //        locationHeader.Border = Rectangle.NO_BORDER;

                        //        PdfPCell locationName = new PdfPCell(new Phrase($"{item.LocationName}", Default));
                        //        locationName.PaddingTop = 5f;
                        //        locationName.Border = Rectangle.NO_BORDER;

                        //        PdfPCell siteNetworkHeader = new PdfPCell(new Phrase($"ภาคผนวก : ", Bold));
                        //        siteNetworkHeader.Border = Rectangle.NO_BORDER;

                        //        PdfPCell siteNetworkName = new PdfPCell(new Phrase($"{item.SiteNetworkName}", Default));
                        //        siteNetworkName.PaddingTop = 5f;
                        //        siteNetworkName.Border = Rectangle.NO_BORDER;

                        //        PdfPCell provinceHeader = new PdfPCell(new Phrase($"จังหวัด : ", Bold));
                        //        provinceHeader.Border = Rectangle.NO_BORDER;

                        //        PdfPCell provinceName = new PdfPCell(new Phrase($"{item.ProvinceName}", Default));
                        //        provinceName.PaddingTop = 5f;
                        //        provinceName.Border = Rectangle.NO_BORDER;

                        //        // Add a row
                        //        innerTable.AddCell(locationHeader);
                        //        innerTable.AddCell(locationName);

                        //        innerTable.AddCell(siteNetworkHeader);
                        //        innerTable.AddCell(siteNetworkName);

                        //        innerTable.AddCell(provinceHeader);
                        //        innerTable.AddCell(provinceName);


                        //        table.AddCell(cellNumber);
                        //        table.AddCell(cell1);
                        //        table.AddCell(cell2);
                        //        table.AddCell(innerTable);


                        //        if (!string.IsNullOrEmpty(item.Image1))
                        //        {
                        //            PdfPCell cell3 = new PdfPCell(new Phrase($"ภาพหน้าสำนักงาน", Bold));
                        //            cell3.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell3.Border = Rectangle.NO_BORDER;
                        //            cell3.PaddingTop = 15f;

                        //            PdfPCell cell4 = new PdfPCell();
                        //            cell4.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell4.Border = Rectangle.NO_BORDER;
                        //            cell4.PaddingTop = 10f;

                        //            string imagePath = item.Image1;

                        //            Image image = Image.GetInstance(imagePath);



                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(300f, 300f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(300f, 300f);
                        //                }
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }

                        //            image.Alignment = Element.ALIGN_CENTER;


                        //            cell4.AddElement(image);

                        //            table.AddCell(cell3);
                        //            table.AddCell(cell4);

                        //            document.Add(table);

                        //            document.NewPage();

                        //            continue;
                        //        }


                        //    }

                        //    if (i == 2)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image2))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพตู้ RACK Network ที่สำนักงาน", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;
                        //            //cell1.PaddingTop = 280f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image2;
                        //            Image image = Image.GetInstance(imagePath);


                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }

                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 3)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image3))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"CPE switch วงจรหลัก", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image3;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 4)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image4))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"CPE switch วงจรรอง", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image4;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 5)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image5))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"อุปกรณ์ Palo alto PA-415 ชุดที่ 1", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image5;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 6)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image6))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"อุปกรณ์ Palo alto PA-415 ชุดที่ 2", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image6;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 7)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image7))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"อุปกรณ์ Router ชุดที่ 1", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image7;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 8)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image8))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"อุปกรณ์ Router ชุดที่ 2", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image8;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 9)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image9))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"WiFi (Aruba AP 303)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image9;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 10)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image11))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"Router 4G (AR109)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image11;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 11)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image51))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพการทดสอบรับ IP Address ", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image51;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 12)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image23))
                        //        {
                        //            var textHeader = "";

                        //            if (item.SiteNetworkId == 2)
                        //            {
                        //                textHeader = "WAN1 วงจรหลัก (40Mbps)";
                        //            }
                        //            else if (item.SiteNetworkId == 3)
                        //            {
                        //                textHeader = "WAN1 วงจรหลัก (20Mbps)";
                        //            }
                        //            else if (item.SiteNetworkId == 4)
                        //            {
                        //                textHeader = "WAN1 วงจรหลัก (20Mbps)";
                        //            }
                        //            else if (item.SiteNetworkId == 5)
                        //            {
                        //                textHeader = "WAN1 วงจรหลัก (20Mbps)";
                        //            }

                        //            PdfPCell cell1 = new PdfPCell(new Phrase(textHeader, Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image23;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 13)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image24))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"WAN2 วงจรรอง (20Mbps) โดยปลดสาย WAN1 วงจรหลักออก", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image24;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 14)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image52))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"วงจร Internet (100Mbps) โดยปลดสาย  WAN1 วงจรหลัก และ WAN2 วงจรรอง", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image52;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 15)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image53))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"วงจร 4G (20Mbps) โดยปลดสาย  WAN1 วงจรหลัก, WAN2 วงจรรอง และ วงจร Internet", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image53;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 16)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image29))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบ การใช้งาน Wifi : Network Connection", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image29;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 17)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image30))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"การใช้งาน LAN ทดสอบ Authentication ", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image30;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 18)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image31))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping www.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image31;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 19)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image32))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert www.dol.go.th (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image32;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 20)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image33))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image33;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 21)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image34))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping ilands.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image34;

                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }


                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 22)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image35))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert ilands.dol.go.th (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image35;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 23)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image36))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image36;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 24)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image37))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping 10.200.30.247 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image37;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 25)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image38))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert 10.200.30.247 (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image38;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 26)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image39))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping 8.8.8.8 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image39;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 27)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image40))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert 8.8.8.8 (วงจรหลัก)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image40;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 28)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image41))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping www.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image41;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 29)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image42))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert www.dol.go.th (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image42;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 30)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image43))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image43;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 31)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image44))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping ilands.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image44;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 32)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image45))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert ilands.dol.go.th (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image45;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 33)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image46))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image46;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 34)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image47))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping 10.200.30.247 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image47;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 35)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image48))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert 10.200.30.247 (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image48;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 36)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image49))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping 8.8.8.8 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image49;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 37)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.Image50))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert 8.8.8.8 (วงจรรอง)", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.Image50;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }                                    	
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    if (i == 38)
                        //    {
                        //        if (!string.IsNullOrEmpty(item.FileApproveName))
                        //        {
                        //            PdfPCell cell1 = new PdfPCell(new Phrase($"เอกสารรับมอบการติดตั้ง", Bold));
                        //            cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell1.Border = Rectangle.NO_BORDER;
                        //            cell1.PaddingTop = 20f;

                        //            PdfPCell cell2 = new PdfPCell();
                        //            cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                        //            cell2.Border = Rectangle.NO_BORDER;
                        //            cell2.PaddingTop = 10f;

                        //            string imagePath = item.FileApproveName;
                        //            Image image = Image.GetInstance(imagePath);

                        //            var imageInfo = ServiceHelper.ExtractMetadataFromUrl(imagePath);

                        //            if (imageInfo.Height > imageInfo.Width)
                        //            {
                        //                if (image.Width > image.Height)
                        //                {
                        //                    image.RotationDegrees = -90;
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //                else
                        //                {
                        //                    image.ScaleAbsolute(250f, 250f);
                        //                }
                        //            }
                        //            else
                        //            {
                        //                image.ScaleAbsolute(400f, 400f);
                        //            }
                        //            image.Alignment = Element.ALIGN_CENTER;
                        //            cell2.AddElement(image);

                        //            table.AddCell(cell1);
                        //            table.AddCell(cell2);

                        //            imageCount++;
                        //        }
                        //    }

                        //    document.Add(table);

                        //    if (i != 1)
                        //    {
                        //        if (imageCount == 2)
                        //        {
                        //            document.NewPage();

                        //            imageCount = 0;
                        //        }
                        //    }
                        //}

                        #endregion

                        document.Close();

                        byte[] bytes = memoryStream.ToArray();

                        memoryStream.Close();

                        string fileName = "รายงานผลการติดตั้งและทดสอบเครือข่ายสื่อสาร" + item.LocationName + ".pdf";

                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "upload", "onsite", fileName);

                        System.IO.File.WriteAllBytes(filePath, bytes);

                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusSuccess;
                        resp.statusCode = Constants.statusCodeOK;
                        resp.data = "https://api.uihservices.com/upload/" + fileName; ;
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
            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = Constants.httpCode500Message;
            }



            return StatusCode(resp.httpCode, AppHelper.GetResponseController(resp));

        }

    }
}

