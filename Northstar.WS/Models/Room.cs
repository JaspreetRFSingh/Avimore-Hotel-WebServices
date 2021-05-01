using Newtonsoft.Json;
using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class Room
    {
        public short RoomId { get; set; }
        [Required]
        public string Name { get; set; }
        [Range(400.0, 12000.00)]
        public float Rate { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
