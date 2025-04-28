using System;
namespace DOL.API.Models.Customs.Response
{
	public class SignInResponse
	{
		public SignInResponse()
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

