using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    /// <summary>
    /// An interface to represent the structure of a general response to be sent by the service
    /// </summary>
    public interface IApiGenericResponse
    {
        public GenericApiResponse GetGenericApiResponse();
    }
}
