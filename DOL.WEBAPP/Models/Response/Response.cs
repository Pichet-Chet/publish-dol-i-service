using System;
namespace DOL.WEBAPP.Models.Response
{
    public class Response
    {
        public Response()
        {
        }

        public bool? status { get; set; } // true , false
        public int statusCode { get; set; } // 1000, 1001, 1002, ... รอตาราง mapping ความหมาย
        public string? type { get; set; } // "Success" , "Error" , "Warning" , ""
        public string? message { get; set; }
        public string? exception { get; set; }
        public object? data { get; set; }
        public int httpCode { get; set; }
        public string? responseTime { get; set; }
        public int? pageNumber { get; set; }
        public int? pageSize { get; set; }
        public int? effectRow { get; set; }
    }
}

