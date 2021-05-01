using Microsoft.AspNetCore.Mvc;
using Northstar.WS.Models;
using Northstar.WS.Services;
using System.Collections.Generic;
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
            var room = await _roomService.GetRoomByIdAsync(roomId);
            if(room == null)
            {
                return NotFound(_roomService.GetApiErrorResponse());
            }
            return room;
        }

        [HttpDelete("{roomId}", Name = nameof(DeleteRoom))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public ActionResult DeleteRoom(short roomId)
        {
            if (!_roomService.DeleteRoom(roomId))
            {
                BadRequest(_roomService.GetApiErrorResponse());
            }
            return Ok();
        }

        [HttpPost(Name = nameof(AddRoom))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public ActionResult AddRoom([FromBody] Room room)
        {
            if (!_roomService.InsertRoom(room))
            {
                return BadRequest(_roomService.GetApiErrorResponse());
            }
            return Ok();
        }

        [HttpPut(Name = nameof(UpdateRoom))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public ActionResult UpdateRoom([FromBody] Room room)
        {
            if (!_roomService.UpdateRoom(room))
            {
                BadRequest(_roomService.GetApiErrorResponse());
            }
            return Ok();
        }

    }
}
