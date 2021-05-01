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

        public void DeleteRoom(short roomId)
        {
            Room roomToBeDeleted = GetRoomAsync(roomId).Result;
            _context.Rooms.Remove(roomToBeDeleted);
            _context.SaveChanges();
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

        public void InsertRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public bool UpdateRoom(Room room)
        {
            Room roomToBeUpdated = _context.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);
            if(roomToBeUpdated == null)
            {
                return false;
            }
            roomToBeUpdated.Name = room.Name;
            roomToBeUpdated.Rate = room.Rate;
            _context.SaveChanges();
            return true;
        }

        List<Room> IRoomService.GetRooms()
        {
            return _context.Rooms.OrderBy(r => r.Name).ToList();
        }
    }
}
