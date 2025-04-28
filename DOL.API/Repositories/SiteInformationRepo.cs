using System;
using System.Text.RegularExpressions;
using DOL.API.Models;
using DOL.API.Models.Customs.Request;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;
using Microsoft.AspNetCore.Mvc;

namespace DOL.API.Repositories
{
    public class SiteInformationRepo : ISiteInformationRepo
    {
        private readonly DolContext _chmContext;

        private readonly SiteInformationService service;

        public SiteInformationRepo()
        {
            _chmContext = new DolContext();

            service = new SiteInformationService(_chmContext);
        }

        public async Task<Response> Schedule(SiteInformationFilter param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Schedule(param));

            return resp;
        }

        public async Task<Response> Overview(SiteInformationFilter param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Overview(param));

            return resp;
        }

        public async Task<Response> CardJobs(SiteInformationFilter param)
        {
            Response resp = new Response();

            resp = await service.CardJobs(param);

            return resp;
        }

        public async Task<Response> Get(SiteInformationFilter param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Get(param));

            return resp;
        }

        public async Task<Response> Detail(int id)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Detail(id));

            return resp;
        }

        public async Task<Response> Update(SiteInformationRequest param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Update(param));

            return resp;
        }

        public async Task<Response> UpdateImage(UpdateImageRequest param)
        {
            Response resp = new Response();

            SiteInformationRequest siteInformationRequest = new SiteInformationRequest();

            siteInformationRequest.Id = param.Id;

            siteInformationRequest.UploadImage1 = param.Flag.ToLower() == "image1" ? param.FileUpload : null;
            siteInformationRequest.UploadImage2 = param.Flag.ToLower() == "image2" ? param.FileUpload : null;
            siteInformationRequest.UploadImage3 = param.Flag.ToLower() == "image3" ? param.FileUpload : null;
            siteInformationRequest.UploadImage4 = param.Flag.ToLower() == "image4" ? param.FileUpload : null;
            siteInformationRequest.UploadImage5 = param.Flag.ToLower() == "image5" ? param.FileUpload : null;
            siteInformationRequest.UploadImage6 = param.Flag.ToLower() == "image6" ? param.FileUpload : null;
            siteInformationRequest.UploadImage7 = param.Flag.ToLower() == "image7" ? param.FileUpload : null;
            siteInformationRequest.UploadImage8 = param.Flag.ToLower() == "image8" ? param.FileUpload : null;
            siteInformationRequest.UploadImage9 = param.Flag.ToLower() == "image9" ? param.FileUpload : null;
            siteInformationRequest.UploadImage10 = param.Flag.ToLower() == "image10" ? param.FileUpload : null;
            siteInformationRequest.UploadImage11 = param.Flag.ToLower() == "image11" ? param.FileUpload : null;
            siteInformationRequest.UploadImage12 = param.Flag.ToLower() == "image12" ? param.FileUpload : null;
            siteInformationRequest.UploadImage13 = param.Flag.ToLower() == "image13" ? param.FileUpload : null;
            siteInformationRequest.UploadImage14 = param.Flag.ToLower() == "image14" ? param.FileUpload : null;
            siteInformationRequest.UploadImage15 = param.Flag.ToLower() == "image15" ? param.FileUpload : null;
            siteInformationRequest.UploadImage16 = param.Flag.ToLower() == "image16" ? param.FileUpload : null;
            siteInformationRequest.UploadImage17 = param.Flag.ToLower() == "image17" ? param.FileUpload : null;
            siteInformationRequest.UploadImage18 = param.Flag.ToLower() == "image18" ? param.FileUpload : null;
            siteInformationRequest.UploadImage19 = param.Flag.ToLower() == "image19" ? param.FileUpload : null;
            siteInformationRequest.UploadImage20 = param.Flag.ToLower() == "image20" ? param.FileUpload : null;
            siteInformationRequest.UploadImage21 = param.Flag.ToLower() == "image21" ? param.FileUpload : null;
            siteInformationRequest.UploadImage22 = param.Flag.ToLower() == "image22" ? param.FileUpload : null;
            siteInformationRequest.UploadImage23 = param.Flag.ToLower() == "image23" ? param.FileUpload : null;
            siteInformationRequest.UploadImage24 = param.Flag.ToLower() == "image24" ? param.FileUpload : null;
            siteInformationRequest.UploadImage25 = param.Flag.ToLower() == "image25" ? param.FileUpload : null;
            siteInformationRequest.UploadImage26 = param.Flag.ToLower() == "image26" ? param.FileUpload : null;
            siteInformationRequest.UploadImage27 = param.Flag.ToLower() == "image27" ? param.FileUpload : null;
            siteInformationRequest.UploadImage28 = param.Flag.ToLower() == "image28" ? param.FileUpload : null;
            siteInformationRequest.UploadImage29 = param.Flag.ToLower() == "image29" ? param.FileUpload : null;
            siteInformationRequest.UploadImage30 = param.Flag.ToLower() == "image30" ? param.FileUpload : null;
            siteInformationRequest.UploadImage31 = param.Flag.ToLower() == "image31" ? param.FileUpload : null;
            siteInformationRequest.UploadImage32 = param.Flag.ToLower() == "image32" ? param.FileUpload : null;
            siteInformationRequest.UploadImage33 = param.Flag.ToLower() == "image33" ? param.FileUpload : null;
            siteInformationRequest.UploadImage34 = param.Flag.ToLower() == "image34" ? param.FileUpload : null;
            siteInformationRequest.UploadImage35 = param.Flag.ToLower() == "image35" ? param.FileUpload : null;
            siteInformationRequest.UploadImage36 = param.Flag.ToLower() == "image36" ? param.FileUpload : null;
            siteInformationRequest.UploadImage37 = param.Flag.ToLower() == "image37" ? param.FileUpload : null;
            siteInformationRequest.UploadImage38 = param.Flag.ToLower() == "image38" ? param.FileUpload : null;
            siteInformationRequest.UploadImage39 = param.Flag.ToLower() == "image39" ? param.FileUpload : null;
            siteInformationRequest.UploadImage40 = param.Flag.ToLower() == "image40" ? param.FileUpload : null;
            siteInformationRequest.UploadImage41 = param.Flag.ToLower() == "image41" ? param.FileUpload : null;
            siteInformationRequest.UploadImage42 = param.Flag.ToLower() == "image42" ? param.FileUpload : null;
            siteInformationRequest.UploadImage43 = param.Flag.ToLower() == "image43" ? param.FileUpload : null;
            siteInformationRequest.UploadImage44 = param.Flag.ToLower() == "image44" ? param.FileUpload : null;
            siteInformationRequest.UploadImage45 = param.Flag.ToLower() == "image45" ? param.FileUpload : null;
            siteInformationRequest.UploadImage46 = param.Flag.ToLower() == "image46" ? param.FileUpload : null;
            siteInformationRequest.UploadImage47 = param.Flag.ToLower() == "image47" ? param.FileUpload : null;
            siteInformationRequest.UploadImage48 = param.Flag.ToLower() == "image48" ? param.FileUpload : null;
            siteInformationRequest.UploadImage49 = param.Flag.ToLower() == "image49" ? param.FileUpload : null;
            siteInformationRequest.UploadImage50 = param.Flag.ToLower() == "image50" ? param.FileUpload : null;
            siteInformationRequest.UploadImage51 = param.Flag.ToLower() == "image51" ? param.FileUpload : null;
            siteInformationRequest.UploadImage52 = param.Flag.ToLower() == "image52" ? param.FileUpload : null;
            siteInformationRequest.UploadImage53 = param.Flag.ToLower() == "image53" ? param.FileUpload : null;

            siteInformationRequest.UploadImage54 = param.Flag.ToLower() == "image54" ? param.FileUpload : null;
            siteInformationRequest.UploadImage55 = param.Flag.ToLower() == "image55" ? param.FileUpload : null;
            siteInformationRequest.UploadImage56 = param.Flag.ToLower() == "image56" ? param.FileUpload : null;
            siteInformationRequest.UploadImage57 = param.Flag.ToLower() == "image57" ? param.FileUpload : null;
            siteInformationRequest.UploadImage58 = param.Flag.ToLower() == "image58" ? param.FileUpload : null;
            siteInformationRequest.UploadImage59 = param.Flag.ToLower() == "image59" ? param.FileUpload : null;
            siteInformationRequest.UploadImage60 = param.Flag.ToLower() == "image60" ? param.FileUpload : null;
            siteInformationRequest.UploadImage61 = param.Flag.ToLower() == "image61" ? param.FileUpload : null;
            siteInformationRequest.UploadImage62 = param.Flag.ToLower() == "image62" ? param.FileUpload : null;
            siteInformationRequest.UploadImage63 = param.Flag.ToLower() == "image63" ? param.FileUpload : null;
            siteInformationRequest.UploadImage64 = param.Flag.ToLower() == "image64" ? param.FileUpload : null;
            siteInformationRequest.UploadImage65 = param.Flag.ToLower() == "image65" ? param.FileUpload : null;
            siteInformationRequest.UploadImage66 = param.Flag.ToLower() == "image66" ? param.FileUpload : null;
            siteInformationRequest.UploadImage67 = param.Flag.ToLower() == "image67" ? param.FileUpload : null;

            siteInformationRequest.UploadFileApprove = param.Flag.ToLower() == "approve" ? param.FileUpload : null;

            siteInformationRequest.CreateBy = param.Username;
            siteInformationRequest.UpdateBy = param.Username;

            resp = await Task.Run(() => service.Update(siteInformationRequest));

            return resp;
        }

        public async Task<Response> UpdateImages(UpdateImageListRequest param)
        {
            Response resp = new Response();

            string imageName1 = "";
            string imageName2 = "";
            string imageName3 = "";
            string imageName4 = "";
            string imageName5 = "";
            string imageName6 = "";
            string imageName7 = "";
            string imageName8 = "";
            string imageName9 = "";
            string imageName10 = "";
            string imageName11 = "";
            string imageName12 = "";
            string imageName13 = "";
            string imageName14 = "";
            string imageName15 = "";
            string imageName16 = "";
            string imageName17 = "";
            string imageName18 = "";
            string imageName19 = "";
            string imageName20 = "";
            string imageName21 = "";
            string imageName22 = "";
            string imageName23 = "WAN1 (วงจรหลัก)";
            string imageName24 = "WAN2 (วงจรรอง)";
            string imageName25 = "";
            string imageName26 = "";
            string imageName27 = "";
            string imageName28 = "";
            string imageName29 = "";
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
            string imageName53 = "";
            string imageName54 = "ภาพสถานะวงจรที่จะทดสอบ Nagios";
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

            SiteInformationRequest siteInformation = new SiteInformationRequest();

            siteInformation.Id = param.Id;
            siteInformation.CreateBy = param.Username;
            siteInformation.UpdateBy = param.Username;

            foreach (var item in param.FileUpload)
            {

                if (item != null)
                {
                    var setFileName = item.FileName;

                    setFileName = Regex.Replace(Regex.Replace(item.FileName, @"^\d+\.", ""), @"\.[^\.]*$", "").ToString().Trim();

                    siteInformation.UploadImage1 = siteInformation.UploadImage1 == null ? setFileName == imageName1 ? item : null : siteInformation.UploadImage1;
                    siteInformation.UploadImage2 = siteInformation.UploadImage2 == null ? setFileName == imageName2 ? item : null : siteInformation.UploadImage2;
                    siteInformation.UploadImage3 = siteInformation.UploadImage3 == null ? setFileName == imageName3 ? item : null : siteInformation.UploadImage3;
                    siteInformation.UploadImage4 = siteInformation.UploadImage4 == null ? setFileName == imageName4 ? item : null : siteInformation.UploadImage4;
                    siteInformation.UploadImage5 = siteInformation.UploadImage5 == null ? setFileName == imageName5 ? item : null : siteInformation.UploadImage5;
                    siteInformation.UploadImage6 = siteInformation.UploadImage6 == null ? setFileName == imageName6 ? item : null : siteInformation.UploadImage6;
                    siteInformation.UploadImage7 = siteInformation.UploadImage7 == null ? setFileName == imageName7 ? item : null : siteInformation.UploadImage7;
                    siteInformation.UploadImage8 = siteInformation.UploadImage8 == null ? setFileName == imageName8 ? item : null : siteInformation.UploadImage8;
                    siteInformation.UploadImage9 = siteInformation.UploadImage9 == null ? setFileName == imageName9 ? item : null : siteInformation.UploadImage9;
                    siteInformation.UploadImage10 = siteInformation.UploadImage10 == null ? setFileName == imageName10 ? item : null : siteInformation.UploadImage10;
                    siteInformation.UploadImage11 = siteInformation.UploadImage11 == null ? setFileName == imageName11 ? item : null : siteInformation.UploadImage11;
                    siteInformation.UploadImage12 = siteInformation.UploadImage12 == null ? setFileName == imageName12 ? item : null : siteInformation.UploadImage12;
                    siteInformation.UploadImage13 = siteInformation.UploadImage13 == null ? setFileName == imageName13 ? item : null : siteInformation.UploadImage13;
                    siteInformation.UploadImage14 = siteInformation.UploadImage14 == null ? setFileName == imageName14 ? item : null : siteInformation.UploadImage14;
                    siteInformation.UploadImage15 = siteInformation.UploadImage15 == null ? setFileName == imageName15 ? item : null : siteInformation.UploadImage15;
                    siteInformation.UploadImage16 = siteInformation.UploadImage16 == null ? setFileName == imageName16 ? item : null : siteInformation.UploadImage16;
                    siteInformation.UploadImage17 = siteInformation.UploadImage17 == null ? setFileName == imageName17 ? item : null : siteInformation.UploadImage17;
                    siteInformation.UploadImage18 = siteInformation.UploadImage18 == null ? setFileName == imageName18 ? item : null : siteInformation.UploadImage18;
                    siteInformation.UploadImage19 = siteInformation.UploadImage19 == null ? setFileName == imageName19 ? item : null : siteInformation.UploadImage19;
                    siteInformation.UploadImage20 = siteInformation.UploadImage20 == null ? setFileName == imageName20 ? item : null : siteInformation.UploadImage20;
                    siteInformation.UploadImage21 = siteInformation.UploadImage21 == null ? setFileName == imageName21 ? item : null : siteInformation.UploadImage21;
                    siteInformation.UploadImage22 = siteInformation.UploadImage22 == null ? setFileName == imageName22 ? item : null : siteInformation.UploadImage22;
                    siteInformation.UploadImage23 = siteInformation.UploadImage23 == null ? setFileName == imageName23 ? item : null : siteInformation.UploadImage23;
                    siteInformation.UploadImage24 = siteInformation.UploadImage24 == null ? setFileName == imageName24 ? item : null : siteInformation.UploadImage24;
                    siteInformation.UploadImage25 = siteInformation.UploadImage25 == null ? setFileName == imageName25 ? item : null : siteInformation.UploadImage25;
                    siteInformation.UploadImage26 = siteInformation.UploadImage26 == null ? setFileName == imageName26 ? item : null : siteInformation.UploadImage26;
                    siteInformation.UploadImage27 = siteInformation.UploadImage27 == null ? setFileName == imageName27 ? item : null : siteInformation.UploadImage27;
                    siteInformation.UploadImage28 = siteInformation.UploadImage28 == null ? setFileName == imageName28 ? item : null : siteInformation.UploadImage28;
                    siteInformation.UploadImage29 = siteInformation.UploadImage29 == null ? setFileName == imageName29 ? item : null : siteInformation.UploadImage29;
                    siteInformation.UploadImage30 = siteInformation.UploadImage30 == null ? setFileName == imageName30 ? item : null : siteInformation.UploadImage30;
                    siteInformation.UploadImage31 = siteInformation.UploadImage31 == null ? setFileName == imageName31 ? item : null : siteInformation.UploadImage31;
                    siteInformation.UploadImage32 = siteInformation.UploadImage32 == null ? setFileName == imageName32 ? item : null : siteInformation.UploadImage32;
                    siteInformation.UploadImage33 = siteInformation.UploadImage33 == null ? setFileName == imageName33 ? item : null : siteInformation.UploadImage33;
                    siteInformation.UploadImage34 = siteInformation.UploadImage34 == null ? setFileName == imageName34 ? item : null : siteInformation.UploadImage34;
                    siteInformation.UploadImage35 = siteInformation.UploadImage35 == null ? setFileName == imageName35 ? item : null : siteInformation.UploadImage35;
                    siteInformation.UploadImage36 = siteInformation.UploadImage36 == null ? setFileName == imageName36 ? item : null : siteInformation.UploadImage36;
                    siteInformation.UploadImage37 = siteInformation.UploadImage37 == null ? setFileName == imageName37 ? item : null : siteInformation.UploadImage37;
                    siteInformation.UploadImage38 = siteInformation.UploadImage38 == null ? setFileName == imageName38 ? item : null : siteInformation.UploadImage38;
                    siteInformation.UploadImage39 = siteInformation.UploadImage39 == null ? setFileName == imageName39 ? item : null : siteInformation.UploadImage39;
                    siteInformation.UploadImage40 = siteInformation.UploadImage40 == null ? setFileName == imageName40 ? item : null : siteInformation.UploadImage40;
                    siteInformation.UploadImage41 = siteInformation.UploadImage41 == null ? setFileName == imageName41 ? item : null : siteInformation.UploadImage41;
                    siteInformation.UploadImage42 = siteInformation.UploadImage42 == null ? setFileName == imageName42 ? item : null : siteInformation.UploadImage42;
                    siteInformation.UploadImage43 = siteInformation.UploadImage43 == null ? setFileName == imageName43 ? item : null : siteInformation.UploadImage43;
                    siteInformation.UploadImage44 = siteInformation.UploadImage44 == null ? setFileName == imageName44 ? item : null : siteInformation.UploadImage44;
                    siteInformation.UploadImage45 = siteInformation.UploadImage45 == null ? setFileName == imageName45 ? item : null : siteInformation.UploadImage45;
                    siteInformation.UploadImage46 = siteInformation.UploadImage46 == null ? setFileName == imageName46 ? item : null : siteInformation.UploadImage46;
                    siteInformation.UploadImage47 = siteInformation.UploadImage47 == null ? setFileName == imageName47 ? item : null : siteInformation.UploadImage47;
                    siteInformation.UploadImage48 = siteInformation.UploadImage48 == null ? setFileName == imageName48 ? item : null : siteInformation.UploadImage48;
                    siteInformation.UploadImage49 = siteInformation.UploadImage49 == null ? setFileName == imageName49 ? item : null : siteInformation.UploadImage49;
                    siteInformation.UploadImage50 = siteInformation.UploadImage50 == null ? setFileName == imageName50 ? item : null : siteInformation.UploadImage50;
                    siteInformation.UploadImage51 = siteInformation.UploadImage51 == null ? setFileName == imageName51 ? item : null : siteInformation.UploadImage51;
                    siteInformation.UploadImage52 = siteInformation.UploadImage52 == null ? setFileName == imageName52 ? item : null : siteInformation.UploadImage52;
                    siteInformation.UploadImage53 = siteInformation.UploadImage53 == null ? setFileName == imageName53 ? item : null : siteInformation.UploadImage53;
                    siteInformation.UploadImage54 = siteInformation.UploadImage54 == null ? setFileName == imageName54 ? item : null : siteInformation.UploadImage54;
                    siteInformation.UploadImage55 = siteInformation.UploadImage55 == null ? setFileName == imageName55 ? item : null : siteInformation.UploadImage55;
                    siteInformation.UploadImage56 = siteInformation.UploadImage56 == null ? setFileName == imageName56 ? item : null : siteInformation.UploadImage56;
                    siteInformation.UploadImage57 = siteInformation.UploadImage57 == null ? setFileName == imageName57 ? item : null : siteInformation.UploadImage57;
                    siteInformation.UploadImage58 = siteInformation.UploadImage58 == null ? setFileName == imageName58 ? item : null : siteInformation.UploadImage58;
                    siteInformation.UploadImage59 = siteInformation.UploadImage59 == null ? setFileName == imageName59 ? item : null : siteInformation.UploadImage59;
                    siteInformation.UploadImage60 = siteInformation.UploadImage60 == null ? setFileName == imageName60 ? item : null : siteInformation.UploadImage60;
                    siteInformation.UploadImage61 = siteInformation.UploadImage61 == null ? setFileName == imageName61 ? item : null : siteInformation.UploadImage61;
                    siteInformation.UploadImage62 = siteInformation.UploadImage62 == null ? setFileName == imageName62 ? item : null : siteInformation.UploadImage62;
                    siteInformation.UploadImage63 = siteInformation.UploadImage63 == null ? setFileName == imageName63 ? item : null : siteInformation.UploadImage63;
                    siteInformation.UploadImage64 = siteInformation.UploadImage64 == null ? setFileName == imageName64 ? item : null : siteInformation.UploadImage64;
                    siteInformation.UploadImage65 = siteInformation.UploadImage65 == null ? setFileName == imageName65 ? item : null : siteInformation.UploadImage65;
                    siteInformation.UploadImage66 = siteInformation.UploadImage66 == null ? setFileName == imageName66 ? item : null : siteInformation.UploadImage66;
                    siteInformation.UploadImage67 = siteInformation.UploadImage67 == null ? setFileName == imageName67 ? item : null : siteInformation.UploadImage67;
                }
            }

            resp = await Task.Run(() => service.Update(siteInformation));

            return resp;
        }


        public async Task<Response> DeleteImage(DeleteImageRequest param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.DeleteImage(param));

            return resp;
        }

        public async Task<Response> UpdateImageName(UpdateImageNameRequest param)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.UpdateImageName(param));

            return resp;
        }



        public async Task<Response> Province()
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Province());

            return resp;
        }

        public async Task<Response> Location(string provinceName)
        {
            Response resp = new Response();

            resp = await Task.Run(() => service.Location(provinceName));

            return resp;
        }


    }
}

