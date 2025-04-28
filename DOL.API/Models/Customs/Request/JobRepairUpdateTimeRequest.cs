using System;
namespace DOL.API.Models.Customs.Request
{
    public class JobRepairUpdateTimeRequest
    {
        public JobRepairUpdateTimeRequest()
        {
            Id = 0;
            Value = DateTime.Now;
            Flag = string.Empty;
        }

        public int Id { get; set; }
        public DateTime Value { get; set; } // JobCreatedDate , JobAcceptDate , JobProcessDate , JobCompleteDate
        public string Flag { get; set; }
    }
}

