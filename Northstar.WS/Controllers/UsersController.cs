using Microsoft.AspNetCore.Mvc;
using Northstar.WS.Models;
using Northstar.WS.Models.DTO;
using Northstar.WS.Services.ControllerServices;
using System.Collections.Generic;

namespace Northstar.WS.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService _userService;
        public UsersController(IUserService userService)
        {
            _userService = userService;
        }

        public ActionResult<List<UserDTO>> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpPost(Name = nameof(AddUser))]
        [ProducesResponseType(200)]
        public ActionResult AddUser([FromBody] UserDTO user)
        {
            if (!_userService.RegisterUser(user))
            {
                return BadRequest(_userService.GetGenericApiResponse());
            }
            return Ok(_userService.GetGenericApiResponse());
        }

    }
}
