using Microsoft.AspNetCore.Identity;
using System;

namespace Northstar.WS.Models.DTO
{
    public class UserRoleDTO : IdentityRole<Guid>
    {
        public UserRoleDTO() : base()
        {

        }
        public UserRoleDTO(string roleName) : base(roleName)
        {

        }
        public int? UserRoleId { get; set; }
        public string UserRoleName { get; set; }
    }
}
