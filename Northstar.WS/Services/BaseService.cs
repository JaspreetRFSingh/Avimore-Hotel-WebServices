using Northstar.WS.Models;
using Northstar.WS.Utility;

namespace Northstar.WS.Services
{
    public class BaseService : IBaseService
    {
        private readonly ApiError _apiError = new ApiError();
        public void SetErrorResponse(int errorCode, string resourceId="", string resourceName="", object obj = null)
        {
            _apiError.code = errorCode;
            _apiError.Message = CreateCustomErrorMessage(errorCode, resourceId, resourceName, obj);
        }

        public void SetErrorResponse(int errorCode, string message)
        {
            _apiError.code = errorCode;
            _apiError.Message = message;
        }

        public ApiError GetApiErrorResponse()
        {
            return _apiError;
        }

        private string CreateCustomErrorMessage(int code, string resourceId, string resourceName, object obj)
        {
            string message = CommonConstants.CustomErrorResponses[code];
            if (message.Contains(CommonConstants.ResourceIdPlaceHolder))
            {
                message = message.Replace(CommonConstants.ResourceIdPlaceHolder, resourceId);
            }
            if (message.Contains(CommonConstants.ResourcePlaceHolder))
            {
                message = message.Replace(CommonConstants.ResourcePlaceHolder, resourceName);
            }
            if (message.Contains(CommonConstants.JSONToStringPlaceHolder))
            {
                message = message.Replace(CommonConstants.JSONToStringPlaceHolder, obj.ToString());
            }
            return message;
        }

    }
}
