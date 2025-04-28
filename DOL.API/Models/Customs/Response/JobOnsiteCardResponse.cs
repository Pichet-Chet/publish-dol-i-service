using System;
namespace DOL.API.Models.Customs.Response
{
	public class JobOnsiteCardResponse
    {
		public JobOnsiteCardResponse()
		{

		}

		public int? Id { get; set; }

		public string? NetworkName { get; set; }

		public string? LocationName { get; set; }

		public string? Address { get; set; }

		public string? Tel { get; set; }

		public string? GoogleMap { get; set; }

        public int? StatusId { get; set; }

        public string? StatusName { get; set; }
	}
}

