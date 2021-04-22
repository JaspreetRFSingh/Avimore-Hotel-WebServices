using Newtonsoft.Json;
using System;
using System.Collections.Generic;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class Hotel
    {
        public string Title { get; set; }
        public string Tagline { get; set; }
        public string Email { get; set; }

        [JsonProperty(NullValueHandling = NullValueHandling.Ignore)]
        public string Website { get; set; }
        public short LocationId { get; set; }
        public int HotelId { get; set; }

        public virtual FacilityAddress Location { get; set; }
    }
}
