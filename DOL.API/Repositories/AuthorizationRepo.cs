using System;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs;
using DOL.API.Models.Response;
using DOL.API.Repositories.Interface;
using DOL.API.Services;
using WatchDog;

namespace DOL.API.Repositories
{
	public class AuthorizationRepo : IAuthorizationRepo
    {
        private readonly DolContext _context;

        private readonly AuthorizationService service;

        public AuthorizationRepo()
        {
            _context = new DolContext();

            service = new AuthorizationService(_context);
        }

        public async Task<Response> GenerateToken(AuthorizationModel param)
        {
            Response resp = new Response();

            try
            {
                resp = await Task.Run(() => service.GenerateToken(param));
            }
            catch (Exception ex)
            {
                resp.httpCode = Constants.httpCode500;
                resp.status = Constants.statusError;
                resp.statusCode = Constants.statusCodeException;
                resp.message = Constants.httpCode500Message;
                resp.exception = ex.Message;

                WatchLogger.LogError("Message : " + ex.Message + " | " + "Exception : " + ex.InnerException == null ? "" : ex.InnerException.ToString());
            }

            return resp;
        }
    }
}

