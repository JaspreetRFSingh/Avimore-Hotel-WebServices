using System;
using System.Collections.Generic;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class FacilityAddress
    {
        public FacilityAddress()
        {
            Hotels = new HashSet<Hotel>();
        }

        public short AddressId { get; set; }
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }

        public virtual ICollection<Hotel> Hotels { get; set; }
    }
}
