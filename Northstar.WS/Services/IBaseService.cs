using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    public interface IBaseService:IErrorResponse
    {
        public void SetErrorResponse(int errorCode, string resourceId, string resourceName);
        public string CreateCustomErrorMessage(int code, string resourceId, string resourceName);
    }
}
