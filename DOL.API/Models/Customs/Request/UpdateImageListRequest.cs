using System;
namespace DOL.API.Models.Customs.Request
{
	public class UpdateImageListRequest
	{
		public UpdateImageListRequest()
		{
		}

        public int Id { get; set; }

        public string Username { get; set; }

        public List<IFormFile?> FileUpload { get; set; }
    }
}

