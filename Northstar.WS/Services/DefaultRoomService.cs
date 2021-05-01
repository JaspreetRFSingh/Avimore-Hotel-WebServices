using Microsoft.EntityFrameworkCore;
using Northstar.WS.Models;
using Northstar.WS.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public class DefaultRoomService : BaseService, IRoomService
    {
        private readonly AvimoreDBContext _context;
        
        public DefaultRoomService(AvimoreDBContext context)
        {
            _context = context;
        }

        #region CRUD Operations
        List<Room> IRoomService.GetRooms()
        {
            return _context.Rooms.OrderBy(r => r.Name).ToList();
        }

        public async Task<Room> GetRoomByIdAsync(short roomId)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(x => x.RoomId == roomId);
            if (room == null)
            {
                SetErrorResponse(301, roomId.ToString(), CommonConstants.ResourceNameForRoomController);
                return null;
            }
            return room;
        }

        public void InsertRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void UpdateRoom(Room room)
        {
            Room roomToBeUpdated = _context.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);
            if (roomToBeUpdated == null)
            {
                return;
            }
            roomToBeUpdated.Name = room.Name;
            roomToBeUpdated.Rate = room.Rate;
            _context.SaveChanges();
        }

        public void DeleteRoom(short roomId)
        {
            Room roomToBeDeleted = GetRoomByIdAsync(roomId).Result;
            _context.Rooms.Remove(roomToBeDeleted);
            _context.SaveChanges();
        }
        #endregion


    }
}
