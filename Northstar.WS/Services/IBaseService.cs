using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    /// <summary>
    /// IBaseService: Interface declaring the generic api responses(errors and success)
    /// </summary>
    public interface IBaseService: IApiGenericResponse
    {
        public void SetErrorResponse(int errorCode, string resourceId = "", string resourceName = "", object obj = null);
        public void SetErrorResponse(int errorCode, string message="");
        public void SetSuccessResponse(int successCode, string message, object obj);
    }
}
