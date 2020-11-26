using System.Collections.Generic;
using Domain.Address;
using Domain.Profile;

namespace Application.Repositories
{
    public interface IProfileRepository
    {
        IEnumerable<IProfile> Query();
        IProfile GetById(int id);
        IProfile GetByIdUser(int idUser);
        IProfile Create(IProfile profile);
        bool DeleteById(int id);
        bool Update(int id, IProfile profile);
    }
}