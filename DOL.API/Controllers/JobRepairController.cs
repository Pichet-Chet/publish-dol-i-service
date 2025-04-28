using System.Diagnostics;
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
using DOL.API.Services.Helper;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using MySqlX.XDevAPI.Common;
using Newtonsoft.Json;

namespace DOL.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    //[Authorize]
    public class JobRepairController : ControllerBase
    {
        private readonly IJobRepairRepo repoCollection;

        public JobRepairController()
        {
            this.repoCollection = new JobRepairRepo();
        }

        // GET: api/values
        [HttpGet]
        public async Task<IActionResult> Get([FromQuery] JobRepairFilter param)
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



        // POST api/values
        [HttpPost]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Create([FromForm] JobRepair param, IFormFile? FileUpload1, IFormFile? FileUpload2, IFormFile? FileUpload3, IFormFile? FileUpload4)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Create(param, FileUpload1, FileUpload2, FileUpload3, FileUpload4));

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

        [HttpPost]
        [Route("Update")]
        [Consumes("multipart/form-data")]
        public async Task<IActionResult> Update([FromForm] JobRepair param, IFormFile? FileUpload1, IFormFile? FileUpload2, IFormFile? FileUpload3, IFormFile? FileUpload4)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.Update(param, FileUpload1, FileUpload2, FileUpload3, FileUpload4));

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
        [Route("UpdateTime")]
        public async Task<IActionResult> UpdateTime([FromForm] JobRepairUpdateTimeRequest param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.UpdateTime(param));

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
        [Route("ReportRepairAdmin")]
        public async Task<IActionResult> ReportRepairAdmin([FromQuery] ExportJobRepairFilter param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.ReportRepairAdmin(param));

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
        [Route("ExportJobRepair")]
        public async Task<IActionResult> ExportJobRepair([FromQuery] ExportJobRepairRequest param)
        {
            Response result = new Response();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                result = await Task.Run(() => repoCollection.ExportJobRepair(param));

                if (result.status == true)
                {
                    byte[] excelFileBytes = (byte[])result.data;

                    return File(excelFileBytes, "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet", "File-" + param.Type + "-" + DateTime.Now.ToString("dd/MM/yyyy HH:mm:ss") + ".xlsx");
                }
                else
                {
                    result.httpCode = Constants.httpCode500;
                    result.status = Constants.statusError;
                    result.statusCode = Constants.statusCodeException;
                    result.message = Constants.httpCode500Message;
                }

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
        [Route("ExportJobRepairMonth")]
        public async Task<IActionResult> ExportJobRepairMonth([FromQuery] ExportJobRepairMonthRequest param)
        {
            Response resp = new Response();

            ExportJobRepairMonthReponse result = new ExportJobRepairMonthReponse();

            try
            {
                var watch = new Stopwatch();

                watch.Start();

                resp = await Task.Run(() => repoCollection.ExportJobRepairMonth(param));

                if (resp.status == true)
                {
                    string? folderReadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiAppRootImageUrl"];

                    var jsonData = JsonConvert.SerializeObject(resp.data);

                    result = JsonConvert.DeserializeObject<ExportJobRepairMonthReponse>(jsonData);

                    if (result != null)
                    {
                        RepairMonthPaper generateLicense = new RepairMonthPaper();

                        Response page1 = new Response();
                        Response page2 = new Response();
                        Response page3 = new Response();

                        byte[] pdfOut1 = null;
                        byte[] pdfOut2 = null;
                        byte[] pdfOut3 = null;

                        for (int i = 1; i <= 3; i++)
                        {
                            if (i == 1)
                            {
                                result.page = 1;

                                page1 = await Task.Run(() => generateLicense.GetnerateDocument(result));

                                pdfOut1 = ServiceHelper.ConvertImageToPdf((byte[])page1.data);
                            }
                            else if (i == 2)
                            {
                                result.page = 2;

                                page2 = await Task.Run(() => generateLicense.GetnerateDocument(result));

                                pdfOut2 = ServiceHelper.ConvertImageToPdf((byte[])page2.data);
                            }
                            else if (i == 3)
                            {
                                result.page = 3;

                                page3 = await Task.Run(() => generateLicense.GetnerateDocument(result));

                                pdfOut3 = ServiceHelper.ConvertImageToPdf((byte[])page3.data);
                            }
                        }



                        if (page1.httpCode == Constants.httpCode200)
                        {
                            string fileName = "รายงานแจ้งซ่อมประจำเดือน" + result.MonthName + ".pdf";

                            string filePath = Path.Combine(Directory.GetCurrentDirectory(), "upload", "onsite", fileName);

                            byte[] mergedPdf = MergePdfs(new byte[][] { pdfOut1, pdfOut2, pdfOut3 });


                            System.IO.File.WriteAllBytes(filePath, mergedPdf);
                            //System.IO.File.WriteAllBytes(filePath, pdfOut2);
                            //System.IO.File.WriteAllBytes(filePath, pdfOut3);

                            resp.httpCode = Constants.httpCode200;
                            resp.status = Constants.statusSuccess;
                            resp.statusCode = Constants.statusCodeOK;
                            resp.data = folderReadFile + "/onsite/" + fileName; ;
                        }
                        else
                        {
                            return StatusCode(page1.httpCode, AppHelper.GetResponseController(page1));
                        }

                    }
                }


                watch.Stop();

                resp.responseTime = watch.Elapsed.Milliseconds + " " + Constants.unitOfTime;
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


        public static byte[] MergePdfs(byte[][] pdfs)
        {
            using (MemoryStream ms = new MemoryStream())
            {
                Document document = new Document();
                PdfCopy copy = new PdfCopy(document, ms);
                document.Open();

                foreach (var pdf in pdfs)
                {
                    if (pdf != null)
                    {
                        using (PdfReader reader = new PdfReader(pdf))
                        {
                            for (int i = 1; i <= reader.NumberOfPages; i++)
                            {
                                copy.AddPage(copy.GetImportedPage(reader, i));
                            }
                        }
                    }
                }

                document.Close();
                return ms.ToArray();
            }
        }

    }
}

