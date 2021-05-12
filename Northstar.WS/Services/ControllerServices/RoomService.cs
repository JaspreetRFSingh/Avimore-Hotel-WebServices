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
        public List<Room> GetRooms(SortOptions<Room> sortOptions, SearchOptions<Room> searchOptions)
        {
            IQueryable<Room> query = _context.Rooms;
            query = searchOptions.Apply(query);
            query = sortOptions.Apply(query);
            if (_context.Rooms.Count() == 0)
            {
                SetErrorResponse(300, string.Empty);
            }
            return query.ToList<Room>();
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
                SetSuccessResponse(201, CommonConstants.ResourceNameForRoomController, room);
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
                SetSuccessResponse(202, CommonConstants.ResourceNameForRoomController, roomToBeUpdated);
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
            try
            {
                _context.Rooms.Remove(roomToBeDeleted);
                _context.SaveChanges();
                SetSuccessResponse(203, CommonConstants.ResourceNameForRoomController, roomToBeDeleted);
                return true;
            }
            catch (DbUpdateException e)
            {
                SetErrorResponse(501, e.InnerException.Message);
                return false;
            }
            catch (Exception)
            {
                SetErrorResponse(102, resourceName: CommonConstants.ResourceNameForRoomController);
                return false;
            }
        }
        #endregion

    }
}
