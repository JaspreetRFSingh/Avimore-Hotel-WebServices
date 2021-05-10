using Newtonsoft.Json;

namespace Northstar.WS.Models
{
    /// <summary>
    /// POCO representing a model for a Generic API Response
    /// </summary>
    public class GenericApiResponse
    {
        public string Message
        {
            get; set;
        }
        public int Code { get; set; }
        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public object? ResponseObject { get; set; }
    }
}
