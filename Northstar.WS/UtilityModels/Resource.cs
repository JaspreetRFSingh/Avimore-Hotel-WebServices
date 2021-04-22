using Newtonsoft.Json;

namespace Northstar.WS.Models
{
    public abstract class Resource
    {
        [JsonProperty(Order = -2)]
        string Href { get; set; }
    }
}
