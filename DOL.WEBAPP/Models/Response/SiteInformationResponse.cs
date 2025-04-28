using System;
namespace DOL.WEBAPP.Models.Response
{
	public class SiteInformationResponse : SiteInformation
	{
	
        public SiteInformationResponse()
        {
            SiteNetwork = new SiteNetwork();
            SysStatus = new SysStatus();
            SysUser = new SysUser();
        }

        public SiteNetwork? SiteNetwork { get; set; }
        public SysStatus? SysStatus { get; set; }
        public SysUser? SysUser { get; set; }
    }
}

