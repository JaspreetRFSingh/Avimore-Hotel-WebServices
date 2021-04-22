using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
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
    }
}
