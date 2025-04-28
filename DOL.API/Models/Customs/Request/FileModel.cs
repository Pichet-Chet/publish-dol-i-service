using System;
namespace DOL.API.Models.Customs.Request
{
	public class FileModel
	{
		public FileModel()
		{

		}

        public IFormFile? FileContent { get; set; }

        public bool? isDelete { get; set; }
    }
}

