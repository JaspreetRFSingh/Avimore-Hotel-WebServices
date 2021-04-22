using System;
using System.Collections.Generic;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class HotelRoom
    {
        public int? HotelId { get; set; }
        public short? RoomId { get; set; }

        public virtual Hotel Hotel { get; set; }
        public virtual Room Room { get; set; }
    }
}
