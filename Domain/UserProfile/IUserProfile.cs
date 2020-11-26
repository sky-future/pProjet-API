using Domain.Profile;
using Domain.Shared;
using Domain.Users;

namespace Domain.UserProfile
{
    public interface IUserProfile : IEntity
    {
        public IUser User { get; set; }
        public IProfile Profile { get; set; }
        
    }
}