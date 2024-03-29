﻿using Microsoft.AspNetCore.Mvc;

namespace Northstar.WS.Controllers
{
    [Route("/")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        [HttpGet(Name = nameof(Index))]
        [ProducesResponseType(200)]
        public IActionResult Index()
        {
            var response = new
            {
                href = Url.Link(nameof(Index), null),
                rooms = new
                {
                    href = Url.Link(nameof(RoomsController.GetRooms), null)
                }
            };
           return Ok(response);
        }
    }
}
