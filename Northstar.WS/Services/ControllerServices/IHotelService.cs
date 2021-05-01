using Northstar.WS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public interface IHotelService: IBaseService
    {
        Task<Hotel> GetHotelAsync(int hotelId);
        List<Hotel> GetHotels();
        Task<List<Room>> GetRoomsForHotelAsync(int hotelId);
    }
}
