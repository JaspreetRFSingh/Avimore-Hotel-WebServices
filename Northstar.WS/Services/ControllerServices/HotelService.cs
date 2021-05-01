using Microsoft.EntityFrameworkCore;
using Northstar.WS.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public class HotelService : BaseService, IHotelService
    {
        private readonly AvimoreDBContext _context;

        public HotelService(AvimoreDBContext context)
        {
            _context = context;
        }

        async Task<Hotel> IHotelService.GetHotelAsync(int hotelId)
        {
            return await _context.Hotels.SingleOrDefaultAsync<Hotel>(h => h.HotelId == hotelId);
        }

        List<Hotel> IHotelService.GetHotels()
        {
            var hotels = _context.Hotels.ToList();
            int count = hotels.Count;
            IAddressService defaultAddressService = new AddressService(_context);
            
            for (int i = 0; i < count; i++)
            {
                hotels[i].Location = defaultAddressService.GetAddress(hotels[i].LocationId);
            }
            return hotels;
        }

        async Task<List<Room>> IHotelService.GetRoomsForHotelAsync(int hotelId)
        {
            var hotelRooms = await  _context.HotelRooms.Where(hr => hr.HotelId == hotelId).ToListAsync();
            List<Room> roomsForGivenHotel = new List<Room>();
            int count = hotelRooms.Count;
            for(int i=0;i<count; i++)
            {
                var currentRoom = await _context.Rooms.FirstOrDefaultAsync( r=> r.RoomId == hotelRooms[i].RoomId);
                roomsForGivenHotel.Add(currentRoom);
            }
            return roomsForGivenHotel;
        }
    }
}
