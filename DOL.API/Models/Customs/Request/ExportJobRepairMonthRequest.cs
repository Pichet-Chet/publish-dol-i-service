using System;
namespace DOL.API.Models.Customs.Request
{
	public class ExportJobRepairMonthRequest
	{
		public ExportJobRepairMonthRequest()
		{
            Year = 0;
            Month = 0;

        }

        public int Year { get; set; }
        public int Month { get; set; }
    }
}

