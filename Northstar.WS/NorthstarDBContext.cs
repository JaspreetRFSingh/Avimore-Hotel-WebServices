using Microsoft.EntityFrameworkCore;
using Northstar.WS.Models;

namespace Northstar.WS
{
    public class NorthstarDBContext : DbContext
    {
        public NorthstarDBContext()
        {}

        public  DbSet<Hotel> Hotels { get; set; }
        public DbSet<Room> Rooms { get; set; }

    }
}
