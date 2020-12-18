using System;
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

        protected bool Equals(AddressUser other)
        {
            return Id == other.Id && Equals(User, other.User) && Equals(Address, other.Address);
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((AddressUser) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, User, Address);
        }
    }
}