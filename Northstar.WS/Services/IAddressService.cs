using Northstar.WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public interface IAddressService
    {
        FacilityAddress GetAddress(short addressId);
    }
}
