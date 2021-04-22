﻿using Microsoft.EntityFrameworkCore;
using Northstar.WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public class HotelService : IHotelService
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
            for (int i = 0; i < count; i++)
            {

            }
            return hotels;
        }
    }
}
