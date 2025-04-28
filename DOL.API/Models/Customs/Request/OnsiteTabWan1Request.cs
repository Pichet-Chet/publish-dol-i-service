using System;
namespace DOL.API.Models.Customs.Request
{
	public class OnsiteTabWan1Request
    {
		public OnsiteTabWan1Request()
		{
            fileUpload = new FileUpload();
        }

        public int Id { get; set; }

        public int? SiteNetworkId { get; set; }

        public string? SiteNetworkName { get; set; }

        public int? SiteNetworkSeq { get; set; }

        public string? ProvinceName { get; set; }

        public string? LocationName { get; set; }

        public string? Address { get; set; }

        public string? StaffOrganize { get; set; }

        public string? TelephoneNumber { get; set; }

        public string? Latitude { get; set; }

        public string? Longitude { get; set; }

        public FileUpload? fileUpload { get; set; }





        public int JobOnsiteId { get; set; }

        #region Team Install

        public string? TeamInstallContactName { get; set; }

        public string? TeamInstallContactTel { get; set; }

        public string? TeamInstallComment { get; set; }

        #endregion


        #region Team Install

        public string? AcceptSign { get; set; }

        public string? AcceptBy { get; set; }

        public string? AcceptPosition { get; set; }

        #endregion

    }

    public partial class FileUpload
    {
        public IFormFile? UploadImage3 { get; set; } //image3
        public IFormFile? UploadImage22 { get; set; } //image22
    }
}

