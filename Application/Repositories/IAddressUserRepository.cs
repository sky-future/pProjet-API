using System.Collections.Generic;
using Domain.Address;
using Domain.AddressUser;
using Domain.Users;

namespace Application.Repositories
{
    public interface IAddressUserRepository
    {
        IEnumerable<IAddressUser> GetByUser(IUser user);
        IAddressUser GetByAddress(IAddress address);
        IAddressUser CreateAddressUser(int idUser, int idAddress);
        bool Delete(int idUser, int idAddress);
    }
}