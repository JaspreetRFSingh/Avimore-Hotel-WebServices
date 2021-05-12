using Newtonsoft.Json;
using Northstar.WS.Infrastructure.CustomAttributes;
using System;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class Room
    {
        public short RoomId { get; set; }
        [Required]
        [Sortable]
        public string Name { get; set; }
        [Range(400.0, 12000.00)]
        [Sortable(Default = true)]
        public float Rate { get; set; }
        public override string ToString()
        {
            return JsonConvert.SerializeObject(this, Formatting.Indented);
        }
    }
}
