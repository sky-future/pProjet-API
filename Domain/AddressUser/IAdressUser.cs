using Domain.Address;
using Domain.Shared;
using Domain.Users;

namespace Domain.AddressUser
{
    public interface IAdressUser : IEntity
    {
        public IUser User { get; set; }
        public IAddress Address { get; set; }
    }
}