using Northstar.WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services.ControllerServices
{
    public interface IUserService : IBaseService
    {
        List<User> GetUsers();
        bool RegisterUser(User user);
    }
}
