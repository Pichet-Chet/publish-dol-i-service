using System;
namespace DOL.API.Models.Customs.Response
{
	public class JobOnsiteResponse : JobOnsite
	{
		public JobOnsiteResponse()
		{
            siteInformation = new SiteInformation();
            SysStatus = new SysStatus();
            SysUser = new SysUser();
        }

        public SiteInformation? siteInformation { get; set; }
        public SysStatus? SysStatus { get; set; }
        public SysUser? SysUser { get; set; }

    }
}

