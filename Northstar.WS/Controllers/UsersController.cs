using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Northstar.WS.Models;
using Northstar.WS.Services.ControllerServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

        public ActionResult<List<User>> GetUsers()
        {
            return _userService.GetUsers();
        }

        [HttpPost(Name = nameof(AddUser))]
        [ProducesResponseType(200)]
        public ActionResult AddUser([FromBody] User user)
        {
            if (!_userService.RegisterUser(user))
            {
                return BadRequest(_userService.GetGenericApiResponse());
            }
            return Ok(_userService.GetGenericApiResponse());
        }


    }
}
