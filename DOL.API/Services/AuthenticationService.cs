using System;
using DOL.API.Models;
using DOL.API.Models.Constants;
using DOL.API.Models.Customs;
using DOL.API.Models.Response;
using WatchDog;

namespace DOL.API.Services
{
	public class AuthenticationService
    {
        private readonly DolContext _context;

        public AuthenticationService(DolContext context)
        {
            _context = context;
        }

        public async Task<Response> SignIn(AuthorizationModel param)
        {
            Response resp = new Response();

            SysUser objData = new SysUser();

            try
            {
                var findUsername = await Task.Run(() => _context.SysUsers.Where(x => x.Username.ToLower() == param.UserName.ToLower()).ToList());

                if (findUsername != null && findUsername.Count > 0)
                {
                    findUsername = findUsername.Where(x => x.Password == param.Password).ToList();

                    if (findUsername != null && findUsername.Count > 0)
                    {
                        var execute = findUsername.FirstOrDefault();

                        if (execute.IsActive == true)
                        {
                            objData = execute;

                            resp.httpCode = Constants.httpCode200;
                            resp.status = Constants.statusSuccess;
                            resp.statusCode = Constants.statusCodeOK;
                            resp.effectRow = 1;

                            resp.data = objData;
                        }
                        else
                        {
                            resp.httpCode = Constants.httpCode200;
                            resp.status = Constants.statusError;
                            resp.statusCode = Constants.statusCodeDataNotFound;
                            resp.message = Constants.authenticationAccountBanned;
                            resp.data = execute;
                        }
                    }
                    else
                    {
                        resp.httpCode = Constants.httpCode200;
                        resp.status = Constants.statusError;
                        resp.statusCode = Constants.statusCodeDataNotFound;
                        resp.message = Constants.authenticationInvalidPassword;

                        resp.data = await Task.Run(() => _context.SysUsers.Where(x => x.Username.ToLower() == param.UserName.ToLower()).FirstOrDefault());
                    }
                }
                else
                {
                    resp.httpCode = Constants.httpCode200;
                    resp.status = Constants.statusError;
                    resp.statusCode = Constants.statusCodeDataNotFound;
                    resp.message = Constants.authenticationUsernameNotFound;
                }
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

