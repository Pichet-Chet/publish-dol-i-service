using System;
using DOL.API.Models.Customs;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface IAuthorizationRepo
	{
        Task<Response> GenerateToken(AuthorizationModel param);
    }
}

