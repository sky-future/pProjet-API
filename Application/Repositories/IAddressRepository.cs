using System.Collections.Generic;
using Domain.Address;

namespace Application.Repositories
{
    public interface IAddressRepository
    {
        IEnumerable<IAddress> Query();
        IAddress GetById(int id);
        IAddress Create(IAddress address);
        bool DeleteById(int id);
        bool Update(int id, IAddress address);
    }
}