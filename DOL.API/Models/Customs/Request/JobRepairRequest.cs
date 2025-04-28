using System;
using Newtonsoft.Json;

namespace DOL.API.Models.Customs.Request
{
	public class JobRepairRequest : JobRepair
	{
        [JsonProperty("JobImage1")] // Customize JSON property name
        public new IFormFile? jobImage1 { get; set; }

        [JsonProperty("JobImage2")] // Customize JSON property name
        public new IFormFile? jobImage2 { get; set; }

        [JsonProperty("JobImage3")] // Customize JSON property name
        public new IFormFile? jobImage3 { get; set; }

        [JsonProperty("JobImage4")] // Customize JSON property name
        public new IFormFile? jobImage4 { get; set; }

    }
}

