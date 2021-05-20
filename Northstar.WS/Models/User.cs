using System;
using System.Collections.Generic;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class User
    {
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public int UserRoleId { get; set; }
        public DateTime Created { get; set; }
        public DateTime LastModified { get; set; }

        public virtual UserRole UserRole { get; set; }
    }
}
