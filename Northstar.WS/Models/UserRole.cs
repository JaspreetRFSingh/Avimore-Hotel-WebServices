using System;
using System.Collections.Generic;

#nullable disable

namespace Northstar.WS.Models
{
    public partial class UserRole
    {
        public UserRole()
        {
            Users = new HashSet<User>();
        }

        public int UserRoleId { get; set; }
        public string RoleName { get; set; }

        public virtual ICollection<User> Users { get; set; }
    }
}
