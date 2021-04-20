using Northstar.WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public class DefaultRoomService : IRoomService
    {
        //private readonly _context;

        public DefaultRoomService()
        {

        }

        public Task<Room> GetRoomAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
