using Northstar.WS.Models;

namespace Northstar.WS.Services
{
    public interface IAddressService: IBaseService
    {
        FacilityAddress GetAddress(short addressId);
    }
}
