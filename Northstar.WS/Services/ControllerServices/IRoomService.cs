using Northstar.WS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public interface IRoomService : IBaseService
    {
        Task<Room> GetRoomByIdAsync(short roomId);
        List<Room> GetRooms();
        bool DeleteRoom(short roomId);
        bool InsertRoom(Room room);
        bool UpdateRoom(Room room);
    }
}
