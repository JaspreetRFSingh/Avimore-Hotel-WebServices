using Microsoft.EntityFrameworkCore;
using Northstar.WS.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Northstar.WS.Services
{
    public class DefaultAddressService : IAddressService
    {
        private readonly AvimoreDBContext _context;
        public DefaultAddressService(AvimoreDBContext context)
        {
            _context = context;
        }

        FacilityAddress IAddressService.GetAddress(short addressId)
        {
           return _context.FacilityAddresses.SingleOrDefault(add => add.AddressId == addressId);
        }
    }
}
