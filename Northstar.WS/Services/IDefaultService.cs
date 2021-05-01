using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    public interface IDefaultService
    {
        public ApiError PopulateErrorResponse(int code, string message)
        {
            ApiError errorResponse = new ApiError
            {
                code = code,
                Message = message
            };
            return errorResponse;
        }
    }
}
