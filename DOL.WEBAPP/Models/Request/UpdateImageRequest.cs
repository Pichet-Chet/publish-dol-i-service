using System;
namespace DOL.WEBAPP.Models.Request
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

