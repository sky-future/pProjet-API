using Domain.Address;
using Domain.Users;

namespace Domain.AddressUser
{
    public interface IAddressUserFactory
    {
        IAddressUser CreateAddressUser(IUser user, IAddress address);
    }
}