using Northstar.WS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public interface IRoomService: IDefaultService
    {
        Task<Room> GetRoomByIdAsync(short roomId);
        List<Room> GetRooms();
        void DeleteRoom(short roomId);
        void InsertRoom(Room room);
        void UpdateRoom(Room room);
    }
}
