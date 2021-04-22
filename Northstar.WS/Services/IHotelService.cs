using Northstar.WS.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public interface IHotelService
    {
        Task<Hotel> GetHotelAsync(int hotelId);
        List<Hotel> GetHotels();
    }
}
