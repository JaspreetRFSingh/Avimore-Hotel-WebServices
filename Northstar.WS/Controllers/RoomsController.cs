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
            var room = await _roomService.GetRoomAsync(roomId);
            if(room == null)
            {
                return NotFound(_roomService.PopulateErrorResponse(400,"Room not found with the given id: " + roomId));
            }
            return room;
        }

        [HttpDelete("{roomId}", Name = nameof(DeleteRoom))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public void DeleteRoom(short roomId)
        {
            _roomService.DeleteRoom(roomId);
        }

        [HttpPost(Name = nameof(AddRoom))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public void AddRoom([FromBody] Room room)
        {
            _roomService.InsertRoom(room);
        }

        [HttpPut(Name = nameof(UpdateRoom))]
        [ProducesResponseType(200)]
        [ResponseCache(Duration = 60)]
        public void UpdateRoom([FromBody] Room room)
        {
            _roomService.UpdateRoom(room);
        }

    }
}
