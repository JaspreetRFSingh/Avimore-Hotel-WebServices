using Microsoft.EntityFrameworkCore;
using Northstar.WS.Models;
using Northstar.WS.Utility;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public class DefaultRoomService : IRoomService
    {
        private readonly AvimoreDBContext _context;
        private readonly ApiError _apiError = new ApiError();

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
                SetErrorResponse(301, roomId.ToString() , CommonConstants.ResourceNameForRoomController);
                return null;
            }
            return room;
        }

        public void InsertRoom(Room room)
        {
            _context.Rooms.Add(room);
            _context.SaveChanges();
        }

        public void SetErrorResponse(int errorCode, string resourceId, string resourceName)
        {
            _apiError.code = errorCode;
            _apiError.Message = CreateCustomErrorMessage(errorCode, resourceId, resourceName);
        }

        public void UpdateRoom(Room room)
        {
            Room roomToBeUpdated = _context.Rooms.FirstOrDefault(r => r.RoomId == room.RoomId);
            if(roomToBeUpdated == null)
            {
                return;
            }
            roomToBeUpdated.Name = room.Name;
            roomToBeUpdated.Rate = room.Rate;
            _context.SaveChanges();
        }

        public string CreateCustomErrorMessage(int code, string resourceId = "", string resourceName = "")
        {
            string message = CommonConstants.CustomErrorResponses[code];
            if (message.Contains(CommonConstants.ResourceIdPlaceHolder))
            {
                message = message.Replace(CommonConstants.ResourceIdPlaceHolder, resourceId);
            }
            if (message.Contains(CommonConstants.ResourcePlaceHolder))
            {
                message = message.Replace(CommonConstants.ResourcePlaceHolder, resourceName);
            }
            return message;
        }

        ApiError IDefaultService.GetApiErrorResponse()
        {
            return _apiError;
        }

        List<Room> IRoomService.GetRooms()
        {
            return _context.Rooms.OrderBy(r => r.Name).ToList();
        }
    }
}
