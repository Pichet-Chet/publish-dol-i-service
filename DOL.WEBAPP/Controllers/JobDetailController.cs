using System.Reflection.Metadata;
using DOL.WEBAPP.Extension;
using DOL.WEBAPP.Models.Request;
using DOL.WEBAPP.Models.Response;
using DOL.WEBAPP.Repository.JobOnsiteRepository;
using iTextSharp.text.pdf;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using iTextSharp.text;
using iTextSharp.text.pdf;
using Document = iTextSharp.text.Document;
using System.IO;
using System.Text;
using static Org.BouncyCastle.Bcpg.Attr.ImageAttrib;

namespace DOL.WEBAPP.Controllers
{
    public class JobDetailController : Controller
    {
        IConfiguration _configuration;

        private IWebHostEnvironment _hostingEnvironment;

        private readonly IJobOnsiteRepository _repoCollection;

        public JobDetailController(IConfiguration configuration, IWebHostEnvironment hostingEnvironment)
        {
            _configuration = configuration;
            _hostingEnvironment = hostingEnvironment;
            _repoCollection = new JobOnsiteRepository(configuration);

        }
        // GET: /<controller>/

        public async Task<IActionResult> Index(int id)
        {
            UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

            Response resp = new Response();

            SiteInformationResponse result = new SiteInformationResponse();

            if (userInfo == null)
            {
                return RedirectToAction("Index", "SignIn");
            }
            else
            {
                resp = await Task.Run(() => _repoCollection.Detail(id));

                if (resp.status == true)
                {
                    var aaa = JsonConvert.SerializeObject(resp.data);

                    result = JsonConvert.DeserializeObject<SiteInformationResponse>(aaa);

                    if (result.SysUserId != userInfo.Id)
                    {
                        return RedirectToAction("Error403", "Error");
                    }
                }
            }

            return View(result);
        }


        // GET: /<controller>/
        [HttpPost]
        public async Task<IActionResult> Update([FromBody] SiteInformationRequest param)
        {
            UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

            Response resp = new Response();

            SiteInformationResponse result = new SiteInformationResponse();

            if (userInfo == null)
            {
                return RedirectToAction("Index", "SignIn");
            }
            else
            {
                resp = await Task.Run(() => _repoCollection.Update(param));

                if (resp.status == true)
                {
                    var aaa = JsonConvert.SerializeObject(resp.data);

                    result = JsonConvert.DeserializeObject<SiteInformationResponse>(aaa);
                }


            }

            return new JsonResult(resp);
        }


        [HttpPost]
        public async Task<IActionResult> UpdateImage(SiteInformationRequest param)
        {
            UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

            string? folderUploadFile = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build().GetSection("Settings")["ApiRootUploadSiteinformation"];


            Response resp = new Response();

            //UpdateImageRequest objImage = new UpdateImageRequest();

            UpdateImageNameRequest objImage = new UpdateImageNameRequest();

            try
            {
                if (userInfo == null)
                {
                    return RedirectToAction("Index", "SignIn");
                }
                else
                {
                    if (Request.Form.Files.Count >= 1)
                    {
                        foreach (var fileUpload in Request.Form.Files)
                        {
                            switch (fileUpload.Name)
                            {
                                case "UploadImage1":
                                    param.TypeOnsiteValue = "image1";
                                    break;
                                case "UploadImage2":
                                    param.TypeOnsiteValue = "image2";
                                    break;
                                case "UploadImage3":
                                    param.TypeOnsiteValue = "image3";
                                    break;
                                case "UploadImage4":
                                    param.TypeOnsiteValue = "image4";
                                    break;
                                case "UploadImage5":
                                    param.TypeOnsiteValue = "image5";
                                    break;
                                case "UploadImage6":
                                    param.TypeOnsiteValue = "image6";
                                    break;
                                case "UploadImage7":
                                    param.TypeOnsiteValue = "image7";
                                    break;
                                case "UploadImage8":
                                    param.TypeOnsiteValue = "image8";
                                    break;
                                case "UploadImage9":
                                    param.TypeOnsiteValue = "image9";
                                    break;
                                case "UploadImage10":
                                    param.TypeOnsiteValue = "image10";
                                    break;
                                case "UploadImage11":
                                    param.TypeOnsiteValue = "image11";
                                    break;
                                case "UploadImage12":
                                    param.TypeOnsiteValue = "image12";
                                    break;
                                case "UploadImage13":
                                    param.TypeOnsiteValue = "image13";
                                    break;
                                case "UploadImage14":
                                    param.TypeOnsiteValue = "image14";
                                    break;
                                case "UploadImage15":
                                    param.TypeOnsiteValue = "image15";
                                    break;
                                case "UploadImage16":
                                    param.TypeOnsiteValue = "image16";
                                    break;
                                case "UploadImage17":
                                    param.TypeOnsiteValue = "image17";
                                    break;
                                case "UploadImage18":
                                    param.TypeOnsiteValue = "image18";
                                    break;
                                case "UploadImage19":
                                    param.TypeOnsiteValue = "image19";
                                    break;
                                case "UploadImage20":
                                    param.TypeOnsiteValue = "image20";
                                    break;
                                case "UploadImage21":
                                    param.TypeOnsiteValue = "image21";
                                    break;
                                case "UploadImage22":
                                    param.TypeOnsiteValue = "image22";
                                    break;
                                case "UploadImage23":
                                    param.TypeOnsiteValue = "image23";
                                    break;
                                case "UploadImage24":
                                    param.TypeOnsiteValue = "image24";
                                    break;
                                //case "UploadImage25":
                                //    param.TypeOnsiteValue = "image25";
                                //    break;
                                case "UploadImage26":
                                    param.TypeOnsiteValue = "image26";
                                    break;
                                case "UploadImage27":
                                    param.TypeOnsiteValue = "image27";
                                    break;
                                case "UploadImage28":
                                    param.TypeOnsiteValue = "image28";
                                    break;
                                case "UploadImage29":
                                    param.TypeOnsiteValue = "image29";
                                    break;
                                case "UploadImage30":
                                    param.TypeOnsiteValue = "image30";
                                    break;
                                case "UploadImage31":
                                    param.TypeOnsiteValue = "image31";
                                    break;
                                case "UploadImage32":
                                    param.TypeOnsiteValue = "image32";
                                    break;
                                case "UploadImage33":
                                    param.TypeOnsiteValue = "image33";
                                    break;
                                case "UploadImage34":
                                    param.TypeOnsiteValue = "image34";
                                    break;
                                case "UploadImage35":
                                    param.TypeOnsiteValue = "image35";
                                    break;
                                case "UploadImage36":
                                    param.TypeOnsiteValue = "image36";
                                    break;
                                case "UploadImage37":
                                    param.TypeOnsiteValue = "image37";
                                    break;
                                case "UploadImage38":
                                    param.TypeOnsiteValue = "image38";
                                    break;
                                case "UploadImage39":
                                    param.TypeOnsiteValue = "image39";
                                    break;
                                case "UploadImage40":
                                    param.TypeOnsiteValue = "image40";
                                    break;
                                case "UploadImage41":
                                    param.TypeOnsiteValue = "image41";
                                    break;
                                case "UploadImage42":
                                    param.TypeOnsiteValue = "image42";
                                    break;
                                case "UploadImage43":
                                    param.TypeOnsiteValue = "image43";
                                    break;
                                case "UploadImage44":
                                    param.TypeOnsiteValue = "image44";
                                    break;
                                case "UploadImage45":
                                    param.TypeOnsiteValue = "image45";
                                    break;
                                case "UploadImage46":
                                    param.TypeOnsiteValue = "image46";
                                    break;
                                case "UploadImage47":
                                    param.TypeOnsiteValue = "image47";
                                    break;
                                case "UploadImage48":
                                    param.TypeOnsiteValue = "image48";
                                    break;
                                case "UploadImage49":
                                    param.TypeOnsiteValue = "image49";
                                    break;
                                case "UploadImage50":
                                    param.TypeOnsiteValue = "image50";
                                    break;
                                case "UploadImage51":
                                    param.TypeOnsiteValue = "image51";
                                    break;
                                case "UploadImage52":
                                    param.TypeOnsiteValue = "image52";
                                    break;
                                case "UploadImage53":
                                    param.TypeOnsiteValue = "image53";
                                    break;

                                case "UploadImage25":
                                    // Handle the "UploadFileApprove" case if needed
                                    param.TypeOnsiteValue = "approve";
                                    break;
                                default:
                                    // Handle other cases if needed
                                    break;
                            }

                            var setLocationPath = "siteinformation" + @"/" + param.Id + @"/";

                            //string uploads = Path.Combine(_hostingEnvironment.WebRootPath, setLocationPath);
                            string uploads = folderUploadFile + setLocationPath;

                            string fileName = "";
                            string fileType = "";

                            if (!System.IO.Directory.Exists(uploads))
                            {
                                System.IO.Directory.CreateDirectory(uploads);
                            }

                            if (fileUpload.ContentType == "image/png")
                            {
                                fileType = ".png";
                            }
                            else if (fileUpload.ContentType == "image/jpeg")
                            {
                                fileType = ".jpg";

                            }

                            fileName = Guid.NewGuid().ToString() + fileType;

                            if (fileUpload.Length > 0)
                            {
                                await using (FileStream fileStream = new FileStream(Path.Combine(uploads, fileName), FileMode.Create))
                                {
                                    await fileUpload.CopyToAsync(fileStream);
                                }
                            }

                            #region Set Image

                            switch (fileUpload.Name)
                            {
                                case "UploadImage1":
                                    param.Image1 = uploads + @"/" + fileName;

                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image1";
                                    objImage.Filename = setLocationPath + @"/" + fileName;

                                    break;
                                case "UploadImage2":
                                    param.Image2 = uploads + @"/" + fileName;

                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image2";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage3":
                                    param.Image3 = uploads + @"/" + fileName;

                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image3";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage4":
                                    param.Image4 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image4";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage5":
                                    param.Image5 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image5";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage6":
                                    param.Image6 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image6";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage7":
                                    param.Image7 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image7";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage8":
                                    param.Image8 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image8";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage9":
                                    param.Image9 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image9";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage10":
                                    param.Image10 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image10";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage11":
                                    param.Image11 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image11";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage12":
                                    param.Image12 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image12";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage13":
                                    param.Image13 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image13";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage14":
                                    param.Image14 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image14";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage15":
                                    param.Image15 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image15";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage16":
                                    param.Image16 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image16";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage17":
                                    param.Image17 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image17";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage18":
                                    param.Image18 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image18";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage19":
                                    param.Image19 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image19";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage20":
                                    param.Image20 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image20";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage21":
                                    param.Image21 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image21";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage22":
                                    param.Image22 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image22";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage23":
                                    param.Image23 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image23";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage24":
                                    param.Image24 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image24";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                //case "UploadImage25":
                                //    param.Image25 = uploads + @"/" + fileName;
                                //    objImage.Id = param.Id;
                                //    objImage.Username = userInfo.Username;
                                //    objImage.Flag = "image25";
                                //    objImage.Filename = setLocationPath + @"/" + fileName;
                                //    break;
                                case "UploadImage26":
                                    param.Image26 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image26";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage27":
                                    param.Image27 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image27";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage28":
                                    param.Image28 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image28";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;

                                case "UploadImage29":
                                    param.Image29 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image29";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage30":
                                    param.Image30 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image30";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage31":
                                    param.Image31 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image31";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage32":
                                    param.Image32 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image32";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage33":
                                    param.Image33 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image33";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage34":
                                    param.Image34 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image34";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage35":
                                    param.Image35 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image35";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage36":
                                    param.Image36 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image36";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage37":
                                    param.Image37 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image37";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage38":
                                    param.Image38 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image38";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage39":
                                    param.Image39 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image39";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage40":
                                    param.Image40 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image40";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage41":
                                    param.Image41 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image41";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage42":
                                    param.Image42 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image42";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage43":
                                    param.Image43 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image43";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage44":
                                    param.Image44 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image44";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage45":
                                    param.Image45 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image45";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage46":
                                    param.Image46 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image46";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage47":
                                    param.Image47 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image47";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage48":
                                    param.Image48 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image48";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage49":
                                    param.Image49 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image49";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage50":
                                    param.Image50 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image50";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage51":
                                    param.Image51 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image51";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage52":
                                    param.Image52 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image52";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                case "UploadImage53":
                                    param.Image53 = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "image53";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;

                                case "UploadImage25":
                                    param.FileApproveName = uploads + @"/" + fileName;
                                    objImage.Id = param.Id;
                                    objImage.Username = userInfo.Username;
                                    objImage.Flag = "approve";
                                    objImage.Filename = setLocationPath + @"/" + fileName;
                                    break;
                                default:
                                    // Handle other cases if needed
                                    break;
                            }

                            #endregion
                        }


                        //param.IsActive = true;
                        //param.CreateDate = DateTime.Now;
                        //param.UpdateDate = DateTime.Now;



                        resp = await Task.Run(() => _repoCollection.UpdateImageName(objImage));
                    }

                }
            }
            catch (Exception ex)
            {
                resp.message = ex.Message;
            }



            return new JsonResult(resp);
        }

        public async Task<IActionResult> GeneratePdfInstall(int id)
        {
            UserInfo userInfo = SessionHelper.GetObjectFromJson<UserInfo>(this.HttpContext.Session, "UserInfo");

            Response resp = new Response();

            SiteInformationResponse item = new SiteInformationResponse();

            try
            {
                if (userInfo == null)
                {
                    return RedirectToAction("Index", "SignIn");
                }
                else
                {
                    string fBase = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "fonts", "THSarabunNew", "THSarabunNew.ttf");
                    string fBold = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "fonts", "THSarabunNew", "THSarabunNew Bold.ttf");
                    string fItalic = Path.Combine(Directory.GetCurrentDirectory(), "Resource", "fonts", "THSarabunNew", "THSarabunNew Italic.ttf");

                    resp = await Task.Run(() => _repoCollection.Detail(id));

                    if (resp.status == true)
                    {
                        var jsonData = JsonConvert.SerializeObject(resp.data);

                        item = JsonConvert.DeserializeObject<SiteInformationResponse>(jsonData);

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


                        #region Create Document

                        int imageCount = 0;

                        for (int i = 1; i <= 38; i++)
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
                                

                                if (!string.IsNullOrEmpty(item.Image1))
                                {
                                    PdfPCell cell3 = new PdfPCell(new Phrase($"ภาพหน้าสำนักงาน", Bold));
                                    cell3.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell3.Border = Rectangle.NO_BORDER;
                                    cell3.PaddingTop = 15f;

                                    PdfPCell cell4 = new PdfPCell();
                                    cell4.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell4.Border = Rectangle.NO_BORDER;
                                    cell4.PaddingTop = 10f;

                                    string imagePath = item.Image1; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(300f, 300f); // Adjust image size as needed

                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}

                                    image.Alignment = Element.ALIGN_CENTER;


                                    cell4.AddElement(image);

                                    table.AddCell(cell3);
                                    table.AddCell(cell4);

                                    document.Add(table);

                                    document.NewPage();

                                    continue;
                                }

                                
                            }

                            if (i == 2)
                            {
                                if (!string.IsNullOrEmpty(item.Image2))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพตู้ RACK Network ที่สำนักงาน", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;
                                    //cell1.PaddingTop = 280f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image2; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed

                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}

                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 3)
                            {
                                if (!string.IsNullOrEmpty(item.Image3))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"CPE switch วงจรหลัก", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image3; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 4)
                            {
                                if (!string.IsNullOrEmpty(item.Image4))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"CPE switch วงจรรอง", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image4; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 5)
                            {
                                if (!string.IsNullOrEmpty(item.Image5))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"อุปกรณ์ Palo alto PA-415 ชุดที่ 1", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image5; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 6)
                            {
                                if (!string.IsNullOrEmpty(item.Image6))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"อุปกรณ์ Palo alto PA-415 ชุดที่ 2", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image6; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 7)
                            {
                                if (!string.IsNullOrEmpty(item.Image7))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"อุปกรณ์ Router ชุดที่ 1", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image7; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 8)
                            {
                                if (!string.IsNullOrEmpty(item.Image8))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"อุปกรณ์ Router ชุดที่ 2", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image8; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 9)
                            {
                                if (!string.IsNullOrEmpty(item.Image9))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"WiFi (Aruba AP 303)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image9; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 10)
                            {
                                if (!string.IsNullOrEmpty(item.Image11))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"Router 4G (AR109)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image11; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 11)
                            {
                                if (!string.IsNullOrEmpty(item.Image51))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพการทดสอบรับ IP Address ", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image51; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
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
                                    var textHeader = "";

                                    if (item.SiteNetworkId == 2)
                                    {
                                        textHeader = "WAN1 วงจรหลัก (40Mbps)";
                                    }
                                    else if (item.SiteNetworkId == 3)
                                    {
                                        textHeader = "WAN1 วงจรหลัก (20Mbps)";
                                    }
                                    else if (item.SiteNetworkId == 4)
                                    {
                                        textHeader = "WAN1 วงจรหลัก (20Mbps)";
                                    }
                                    else if (item.SiteNetworkId == 5)
                                    {
                                        textHeader = "WAN1 วงจรหลัก (20Mbps)";
                                    }

                                    PdfPCell cell1 = new PdfPCell(new Phrase(textHeader, Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image23; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 13)
                            {
                                if (!string.IsNullOrEmpty(item.Image24))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"WAN2 วงจรรอง (20Mbps) โดยปลดสาย WAN1 วงจรหลักออก", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image24; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 14)
                            {
                                if (!string.IsNullOrEmpty(item.Image52))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"วงจร Internet (100Mbps) โดยปลดสาย  WAN1 วงจรหลัก และ WAN2 วงจรรอง", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image52; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 15)
                            {
                                if (!string.IsNullOrEmpty(item.Image53))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"วงจร 4G (20Mbps) โดยปลดสาย  WAN1 วงจรหลัก, WAN2 วงจรรอง และ วงจร Internet", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image53; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 16)
                            {
                                if (!string.IsNullOrEmpty(item.Image29))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบ การใช้งาน Wifi : Network Connection", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image29; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 17)
                            {
                                if (!string.IsNullOrEmpty(item.Image30))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"การใช้งาน LAN ทดสอบ Authentication ", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image30; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 18)
                            {
                                if (!string.IsNullOrEmpty(item.Image31))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping www.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image31; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 19)
                            {
                                if (!string.IsNullOrEmpty(item.Image32))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert www.dol.go.th (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image32; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 20)
                            {
                                if (!string.IsNullOrEmpty(item.Image33))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image33; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 21)
                            {
                                if (!string.IsNullOrEmpty(item.Image34))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping ilands.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image34; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 22)
                            {
                                if (!string.IsNullOrEmpty(item.Image35))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert ilands.dol.go.th (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image35; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 23)
                            {
                                if (!string.IsNullOrEmpty(item.Image36))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image36; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 24)
                            {
                                if (!string.IsNullOrEmpty(item.Image37))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping 10.200.30.247 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image37; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 25)
                            {
                                if (!string.IsNullOrEmpty(item.Image38))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert 10.200.30.247 (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image38; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 26)
                            {
                                if (!string.IsNullOrEmpty(item.Image39))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping 8.8.8.8 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image39; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 27)
                            {
                                if (!string.IsNullOrEmpty(item.Image40))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert 8.8.8.8 (วงจรหลัก)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image40; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 28)
                            {
                                if (!string.IsNullOrEmpty(item.Image41))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping www.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image41; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 29)
                            {
                                if (!string.IsNullOrEmpty(item.Image42))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert www.dol.go.th (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image42; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 30)
                            {
                                if (!string.IsNullOrEmpty(item.Image43))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบการใช้งาน เปิด Web www.dol.go.th (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image43; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 31)
                            {
                                if (!string.IsNullOrEmpty(item.Image44))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping ilands.dol.go.th -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image44; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 32)
                            {
                                if (!string.IsNullOrEmpty(item.Image45))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert ilands.dol.go.th (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image45; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 33)
                            {
                                if (!string.IsNullOrEmpty(item.Image46))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ทดสอบการใช้งาน เปิด Web ilands.dol.go.th (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image46; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 34)
                            {
                                if (!string.IsNullOrEmpty(item.Image47))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping 10.200.30.247 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image47; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 35)
                            {
                                if (!string.IsNullOrEmpty(item.Image48))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert 10.200.30.247 (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image48; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 36)
                            {
                                if (!string.IsNullOrEmpty(item.Image49))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ ping 8.8.8.8 -n 30 (response time <100 ms, ต้องไม่มี Time Out) (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image49; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 37)
                            {
                                if (!string.IsNullOrEmpty(item.Image50))
                                {
                                    PdfPCell cell1 = new PdfPCell(new Phrase($"ภาพ tracert 8.8.8.8 (วงจรรอง)", Bold));
                                    cell1.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell1.Border = Rectangle.NO_BORDER;
                                    cell1.PaddingTop = 20f;

                                    PdfPCell cell2 = new PdfPCell();
                                    cell2.HorizontalAlignment = Element.ALIGN_CENTER; // Align cell content to center
                                    cell2.Border = Rectangle.NO_BORDER;
                                    cell2.PaddingTop = 10f;

                                    string imagePath = item.Image50; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
                                    image.Alignment = Element.ALIGN_CENTER;
                                    cell2.AddElement(image);

                                    table.AddCell(cell1);
                                    table.AddCell(cell2);

                                    imageCount++;
                                }
                            }

                            if (i == 38)
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

                                    string imagePath = item.FileApproveName; // Path to your image file
                                    Image image = Image.GetInstance(imagePath);
                                    image.ScaleAbsolute(250f, 250f); // Adjust image size as needed
                                    //if (image.Right > image.Top)
                                    //{
                                    //    image.RotationDegrees = -90; // Set rotation to 0 degrees (no rotation)
                                    //}
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

                        #endregion


                        document.Close();

                        byte[] bytes = memoryStream.ToArray();

                        memoryStream.Close();

                        string fileName = "รายงานผลการติดตั้งและทดสอบเครือข่ายสื่อสาร" + item.LocationName + ".pdf";

                        string filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "onsite", fileName);

                        System.IO.File.WriteAllBytes(filePath, bytes);

                        resp.status = true;
                        resp.data = "/onsite/" + fileName;
                    }
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine($"Unexpected error: {ex.Message}");
                // Log additional details if needed
            }

            return new JsonResult(resp);
        }


    }
}

