using System;
using System.Collections.Generic;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class Room
    {
        public short RoomId { get; set; }
        public string Name { get; set; }
        public float Rate { get; set; }
    }
}
