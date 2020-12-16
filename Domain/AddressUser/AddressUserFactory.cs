using Domain.Address;
using Domain.Users;

namespace Domain.AddressUser
{
    public class AddressUserFactory : IAddressUserFactory
    {
        public IAddressUser CreateAddressUser(IUser user, IAddress address)
        {
            return new AddressUser
            {
                User = user,
                Address = address
            };
        }
    }
}