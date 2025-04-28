using System;
namespace DOL.API.Models.Customs.Request
{
    public class ExportJobRepairRequest
    {
        public ExportJobRepairRequest()
        {
            Year = 0;
            Month = 0;
            Type = string.Empty;
        }

        public int Year { get; set; }
        public int Month { get; set; }
        public string Type { get; set; }
        public bool IsAdmin { get; set; }
    }
}

