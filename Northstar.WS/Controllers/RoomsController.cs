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
        public ActionResult<List<Room>> GetRooms(
            [FromQuery] SortOptions<Room> sortOptions
            )
        {
            return _roomService.GetRooms(sortOptions);
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
                return NotFound(_roomService.GetGenericApiResponse());
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
                return BadRequest(_roomService.GetGenericApiResponse());
            }
            return Ok(_roomService.GetGenericApiResponse());
        }

        [HttpPost(Name = nameof(AddRoom))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public ActionResult AddRoom([FromBody] Room room)
        {
            if (!_roomService.InsertRoom(room))
            {
                return BadRequest(_roomService.GetGenericApiResponse());
            }
            return Ok(_roomService.GetGenericApiResponse());
        }

        [HttpPut(Name = nameof(UpdateRoom))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public ActionResult UpdateRoom([FromBody] Room room)
        {
            if (!_roomService.UpdateRoom(room))
            {
                return BadRequest(_roomService.GetGenericApiResponse());
            }
            return Ok(_roomService.GetGenericApiResponse());
        }

    }
}
