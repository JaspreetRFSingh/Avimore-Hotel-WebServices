using Northstar.WS.Models;
using Northstar.WS.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services.ControllerServices
{
    public interface IUserService : IBaseService
    {
        List<UserDTO> GetUsers();
        bool RegisterUser(UserDTO user);
        string GetUserRoleInfo(int roleId);
    }
}
