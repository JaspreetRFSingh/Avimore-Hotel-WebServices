using Microsoft.EntityFrameworkCore;
using Northstar.WS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly AvimoreDBContext _context;

        public DefaultRoomService(AvimoreDBContext context)
        {
            _context = context;
        }

        public async Task<Room> GetRoomAsync(short roomId)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(x => x.RoomId == roomId);
            if (room == null)
            {
                return null;
            }
            return room;
        }

        List<Room> IRoomService.GetRooms()
        {
            return _context.Rooms.OrderBy(r => r.Name).ToList();
        }
    }
}
