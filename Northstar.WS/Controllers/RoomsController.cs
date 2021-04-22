using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Northstar.WS.Models;
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
        private readonly Room _room;
        private readonly Avimore_09Context _context;

        public RoomsController(IOptions<Room> roomInfoWrapper, Avimore_09Context context)
        {
            _room = roomInfoWrapper.Value;
            this._context = context;
        }

        [HttpGet(Name = nameof(GetRooms))]
        [ProducesResponseType(200)]
        public ActionResult<List<Room>> GetRooms()
        {
            return _context.Rooms.OrderBy(r => r.Name).ToList();
        }

        [HttpGet("{roomid}", Name = nameof(GetRoomById))]
        [ProducesResponseType(404)]
        [ResponseCache(Duration = 60)]
        public async Task<ActionResult<Room>> GetRoomById(short roomId)
        {
            var room = await _context.Rooms.SingleOrDefaultAsync(x => x.RoomId == roomId);
            if(room == null)
            {
                return NotFound();
            }
            return room;
        }

    }
}
