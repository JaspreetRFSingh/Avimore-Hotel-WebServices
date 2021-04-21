using System.Collections.Generic;

namespace Northstar.WS.Models
{
    public class Hotel : Resource
    {
        public string Title { get; set; }
        public string Tagline { get; set; }
        public string Email { get; set; }
        public string Website {get; set;}
        public Address Location { get; set; }
        public List<Room> Rooms { get; set; }

    }

    public class Address
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
    }
}
