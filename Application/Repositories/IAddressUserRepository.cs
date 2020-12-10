using System.Collections.Generic;
using Domain.Address;
using Domain.AddressUser;
using Domain.Users;

namespace Application.Repositories
{
    public interface IAddressUserRepository
    {
        IEnumerable<IAddressUser> GetByUser(IUser user);
        IEnumerable<IAddressUser> GetByAddress(IAddress address);
        IAddressUser CreateAddressUser( IUser user, IAddress address);
        bool Delete(int idUser, int idAddress);
    }
}