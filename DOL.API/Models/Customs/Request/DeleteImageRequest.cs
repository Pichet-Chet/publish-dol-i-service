using System;
namespace DOL.API.Models.Customs.Request
{
	public class DeleteImageRequest
	{
		public DeleteImageRequest()
		{
		}

        public int Id { get; set; }

        public string Flag { get; set; }

        public string Username { get; set; }
    }
}

