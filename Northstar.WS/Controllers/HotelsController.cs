﻿using Microsoft.AspNetCore.Mvc;
using Northstar.WS.Models;
using Northstar.WS.Services;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Northstar.WS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HotelsController : ControllerBase
    {
        private readonly IHotelService _hotelService;

        public HotelsController(IHotelService hotelService)
        {
            _hotelService = hotelService;
        }

        [HttpGet(Name = nameof(GetHotels))]
        [ProducesResponseType(200)]
        public ActionResult<List<Hotel>> GetHotels()
        {
            return _hotelService.GetHotels();
        }

        [HttpGet("{hotelId}/rooms", Name = nameof(GetRoomsForHotel))]
        [ProducesResponseType(200)]
        public async Task<ActionResult<List<Room>>> GetRoomsForHotel(int hotelId)
        {
            return await _hotelService.GetRoomsForHotelAsync(hotelId);
        }

        [HttpGet("{hotelId}", Name = nameof(GetHotelById))]
        public async Task<ActionResult<Hotel>> GetHotelById(int hotelId)
        {
            return await _hotelService.GetHotelAsync(hotelId);
        }

        // POST api/<HotelsController>
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/<HotelsController>/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/<HotelsController>/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
