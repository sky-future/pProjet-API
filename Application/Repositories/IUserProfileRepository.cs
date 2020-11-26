using System.Collections.Generic;
using Domain.Profile;
using Domain.UserProfile;
using Domain.Users;

namespace Application.Repositories
{
    public interface IUserProfileRepository
    {
        IEnumerable<IUserProfile> GetByUser(IUser user);
        IUserProfile CreateUserProfile(IProfile profile, IUser user);
        bool Delete(int profileId, int profileUser);
    }
}