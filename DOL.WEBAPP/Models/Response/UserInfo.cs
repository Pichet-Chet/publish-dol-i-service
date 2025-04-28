using System;
namespace DOL.WEBAPP.Models.Response
{
    public class UserInfo
    {
        public UserInfo()
        {
            Id = 0;
            Username = string.Empty;
            Name = string.Empty;
            UserGroup = string.Empty;
            IsActive = true;
            Token = string.Empty;
            TemplateConfig = string.Empty;
        }

        public int Id { get; set; }

        public string Username { get; set; }

        public string Name { get; set; }

        public string UserGroup { get; set; }

        public bool IsActive { get; set; }

        public string Token { get; set; }

        public string? TemplateConfig { get; set; }
    }
}

