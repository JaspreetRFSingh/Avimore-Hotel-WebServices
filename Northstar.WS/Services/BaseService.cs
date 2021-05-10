using Northstar.WS.Models;
using Northstar.WS.Utility;

namespace Northstar.WS.Services
{
    /// <summary>
    /// Implementation of the IBaseService, to be inherited by all the service classes
    /// </summary>
    public class BaseService : IBaseService
    {
        private readonly GenericApiResponse _genericApiResponse = new GenericApiResponse();

        #region IBaseService methods definition

        public void SetErrorResponse(int errorCode, string resourceId="", string resourceName="", object obj = null)
        {
            _genericApiResponse.Code = errorCode;
            _genericApiResponse.Message = CreateCustomErrorMessage(errorCode, resourceId, resourceName, obj);
        }

        public void SetErrorResponse(int errorCode, string message="")
        {
            _genericApiResponse.Code = errorCode;
            if (string.IsNullOrEmpty(message))
            {
                _genericApiResponse.Message = CreateCustomErrorMessage(errorCode, string.Empty, string.Empty, null);
            }
            else
            {
                _genericApiResponse.Message = message;
            }
        }

        public void SetSuccessResponse(int successCode, string resourceName = "", object obj=null)
        {
            _genericApiResponse.Code = successCode;
            _genericApiResponse.Message = CreateCustomSuccessMessage(successCode, resourceName);
            _genericApiResponse.ResponseObject = obj;
        }

        #endregion

        public GenericApiResponse GetGenericApiResponse()
        {
            return _genericApiResponse;
        }

        #region Private Methods
        private string CreateCustomSuccessMessage(int code, string resourceName)
        {
            string message = CommonConstants.CustomGenericServiceResponses[code];
            if (message.Contains(CommonConstants.ResourcePlaceHolder))
            {
                message = message.Replace(CommonConstants.ResourcePlaceHolder, resourceName);
            }
            return message;
        }

        private string CreateCustomErrorMessage(int code, string resourceId, string resourceName, object obj)
        {
            string message = CommonConstants.CustomGenericServiceResponses[code];
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

        #endregion

    }
}
