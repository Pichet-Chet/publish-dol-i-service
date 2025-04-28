using System;
namespace DOL.WEBAPP.Models.Response
{
	public class SysUserResponse
	{
		public SysUserResponse()
		{

		}

        public int? Id { get; set; }

        public string? Username { get; set; } = null!;

        public string? Name { get; set; } = null!;

        public string? UserGroup { get; set; } = null!;

        public bool? IsActive { get; set; }

        public string? Token { get; set; }

        public string? TemplateConfig { get; set; }

    }
}

