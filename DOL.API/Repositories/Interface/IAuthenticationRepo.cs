using System;
using DOL.API.Models.Customs;
using DOL.API.Models.Filters;
using DOL.API.Models.Response;

namespace DOL.API.Repositories.Interface
{
	public interface IAuthenticationRepo
	{
        Task<Response> SignIn(AuthorizationModel param);
    }
}

