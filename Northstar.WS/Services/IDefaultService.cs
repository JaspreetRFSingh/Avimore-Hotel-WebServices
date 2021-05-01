using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    public interface IDefaultService
    {
        public void SetErrorResponse(int errorCode, string resourceId, string resourceName);
        public ApiError GetApiErrorResponse();
        public string CreateCustomErrorMessage(int code, string resourceId, string resourceName);
    }
}
