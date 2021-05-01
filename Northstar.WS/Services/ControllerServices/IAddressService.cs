using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    public interface IAddressService
    {
        FacilityAddress GetAddress(short addressId);
    }
}
