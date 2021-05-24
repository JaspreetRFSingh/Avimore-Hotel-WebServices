using Microsoft.AspNetCore.DataProtection;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Models.DTO
{
    public class UserDTO : IdentityUser<Guid>
    {
        public override string UserName { get; set; }
        public override string Email { get; set; }
        public UserRoleDTO Role { get; set; }
        public string Password { get; set; }
    }
}
