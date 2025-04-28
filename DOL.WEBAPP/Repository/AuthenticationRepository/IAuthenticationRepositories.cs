using System;
using DOL.WEBAPP.Models.Filter;
using DOL.WEBAPP.Models.Response;

namespace DOL.WEBAPP.Repository.AuthenticationRepository
{
	public interface IAuthenticationRepositories
    {
        Task<Response> SignIn(AuthenticationFilter param);

    }
}

