using System.Collections.Generic;

namespace Domain.Users
{
    public interface IUserRepository
    {
        IEnumerable<IUser> Query();
        IUser Get(int id);
        IUser Create(IUser user);
        bool Delete(int id);
        bool Update(int id, IUser user);
    }
}