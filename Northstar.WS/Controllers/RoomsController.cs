using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Northstar.WS.Models;
using Northstar.WS.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class RoomsController : ControllerBase
    {
        private readonly IRoomService _roomService;

        public RoomsController(IRoomService roomService)
        {
            _roomService = roomService;
        }

        [HttpGet(Name = nameof(GetRooms))]
        [ProducesResponseType(200)]
        public ActionResult<List<Room>> GetRooms()
        {
            return _roomService.GetRooms();
        }

        [HttpGet("{roomid}", Name = nameof(GetRoomById))]
        [ProducesResponseType(200)]
        [ProducesResponseType(404)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<Room>> GetRoomById(short roomId)
        {
            var room = await _roomService.GetRoomAsync(roomId);
            if(room == null)
            {
                return NotFound();
            }
            return room;
        }

    }
}
