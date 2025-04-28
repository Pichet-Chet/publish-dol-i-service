using System;
namespace DOL.API.Models.Customs.Response
{
	public class JobRepairTodayReponse
	{
		public JobRepairTodayReponse()
		{
		}

		public int? Waiting { get; set; }
        public int? OnProcess { get; set; }
        public int? Complete { get; set; }

    }


	public partial class Waiting
	{
		public int? Id { get; set; }
		public string? DocumentNo { get; set; }
		public string? Location { get; set; }
		public string? Province { get; set; }
        public string? NetworkName { get; set; }
        public DateTime? JobCreatedDate { get; set; }
    }
}

