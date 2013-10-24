using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Chat.Services.Models;

namespace Chat.Services.Controllers
{
    public class BaseApiController : ApiController
    {
        private static Dictionary<string, HttpStatusCode> errorToStatusCodes = new Dictionary<string, HttpStatusCode>();

        static BaseApiController()
        {
            errorToStatusCodes["INV_GAME_USR"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["ERR_GEN_SVR"] = HttpStatusCode.InternalServerError;
            errorToStatusCodes["INV_OP_TURN"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["ERR_INV_NUM"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["ERR_INV_USR"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["ERR_INV_GAME"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["INV_GAME_AUTH_LEN"] = HttpStatusCode.Unauthorized;
            errorToStatusCodes["ERR_GAME_STAT_FULL"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["ERR_GAME_STAT_PROG"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["ERR_GAME_STAT_FIN"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["INV_GAME_AUTH"] = HttpStatusCode.Unauthorized;
            errorToStatusCodes["INV_OP_GAME_OWNER"] = HttpStatusCode.Unauthorized;
            errorToStatusCodes["INV_OP_GAME_STAT"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["ERR_NOT_IN_GAME"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["INV_USRNAME_LEN"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["INV_USRNAME_CHARS"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["INV_NICK_LEN"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["INV_NICK_CHARS"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["INV_USR_AUTH_LEN"] = HttpStatusCode.BadRequest;
            errorToStatusCodes["ERR_DUP_USR"] = HttpStatusCode.Conflict;
            errorToStatusCodes["ERR_DUP_NICK"] = HttpStatusCode.Conflict;
            errorToStatusCodes["INV_USR_AUTH"] = HttpStatusCode.BadRequest;
        }

        public BaseApiController()
        {
        }

        protected HttpResponseMessage PerformOperation(Action action)
        {
            try
            {
                action();
                return Request.CreateResponse(HttpStatusCode.OK);
            }
            catch (ServerErrorException ex)
            {
                return BuildErrorResponse(ex.Message, ex.ErrorCode);
            }
            catch (Exception ex)
            {
                var errCode = "ERR_GEN_SVR";
                return BuildErrorResponse(ex.Message, errCode);
            }
        }

        protected HttpResponseMessage PerformOperation<T>(Func<T> action)
        {
            try
            {
                var result = action();
                return Request.CreateResponse(HttpStatusCode.OK, result);
            }
            catch (ServerErrorException ex)
            {
                return BuildErrorResponse(ex.Message, ex.ErrorCode);
            }
            catch (Exception ex)
            {
                var errCode = "ERR_GEN_SVR";
                return BuildErrorResponse(ex.Message, errCode);
            }
        }

        private HttpResponseMessage BuildErrorResponse(string message, string errCode)
        {
            var httpError = new HttpError(message);
            httpError["errCode"] = errCode;
            var statusCode = errorToStatusCodes[errCode];
            return Request.CreateErrorResponse(statusCode, httpError);
        }
    }
}
