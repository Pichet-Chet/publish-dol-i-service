using System;
namespace DOL.API.Models.Customs.Request
{
	public class UpdateImageRequest
	{
		public UpdateImageRequest()
		{
		}

        public int Id { get; set; }

        public string Flag { get; set; }

        public string Username { get; set; }

        public IFormFile? FileUpload { get; set; }
    }
}

