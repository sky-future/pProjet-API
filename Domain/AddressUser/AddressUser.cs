using Domain.Address;
using Domain.Users;

namespace Domain.AddressUser
{
    public class AddressUser : IAddressUser
    {
        public int Id { get; set; }
        public IUser User { get; set; }
        public IAddress Address { get; set; }

        public AddressUser()
        {
            
        }
    }
}