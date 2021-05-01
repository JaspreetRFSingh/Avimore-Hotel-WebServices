using Newtonsoft.Json;
using System;

namespace Northstar.WS.Models
{
    //Generic model class to represent an error/success response
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
