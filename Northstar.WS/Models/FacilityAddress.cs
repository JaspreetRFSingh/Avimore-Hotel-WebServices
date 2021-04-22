using System;
using System.Collections.Generic;
using System.Text.Json.Serialization;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class FacilityAddress
    {
        public short AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        [JsonIgnore]
        public virtual Hotel Hotel { get; set; }
    }
}
