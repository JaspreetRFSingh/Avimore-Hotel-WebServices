using Microsoft.EntityFrameworkCore;
using Northstar.WS.Models;
using Northstar.WS.Utility;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public class RoomService : BaseService, IRoomService
    {
        private readonly AvimoreDBContext _context;
        
        public RoomService(AvimoreDBContext context)
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

        public bool InsertRoom(Room room)
        {
            try
            {
                _context.Rooms.Add(room);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                SetErrorResponse(302, resourceName: CommonConstants.ResourceNameForRoomController, obj: room);
                return false;
            }
            catch (Exception)
            {
                SetErrorResponse(102, resourceName: CommonConstants.ResourceNameForRoomController);
                return false;
            }
        }

        public bool UpdateRoom(Room room)
        {
            Room roomToBeUpdated = _context.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);
            if (roomToBeUpdated == null)
            {
                SetErrorResponse(301, room.RoomId.ToString(), CommonConstants.ResourceNameForRoomController);
                return false;
            }
            try
            {
                roomToBeUpdated.Name = room.Name;
                roomToBeUpdated.Rate = room.Rate;
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateException)
            {
                SetErrorResponse(303, resourceName: CommonConstants.ResourceNameForRoomController, obj: room);
                return false;
            }
            catch (Exception)
            {
                SetErrorResponse(102, resourceName: CommonConstants.ResourceNameForRoomController);
                return false;
            }
        }

        public bool DeleteRoom(short roomId)
        {
            Room roomToBeDeleted = GetRoomByIdAsync(roomId).Result;
            if (roomToBeDeleted == null)
            {
                SetErrorResponse(301, roomId.ToString(), CommonConstants.ResourceNameForRoomController);
                return false;
            }
            _context.Rooms.Remove(roomToBeDeleted);
            _context.SaveChanges();
            return true;
        }
        #endregion

    }
}
