using System;
namespace DOL.API.Models.Response
{
    public class Dropdown
    {
        public Dropdown()
        {
            data = string.Empty;
            value = string.Empty;
        }

        public string data { get; set; }
        public string value { get; set; }
    }
}

