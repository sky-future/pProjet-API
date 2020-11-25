using Domain.Profile;
using Domain.Users;

namespace Domain.UserProfile
{
    public class UserProfile : IUserProfile
    {
        public int Id { get; set; }
        public IUser User { get; set; }
        public IProfile Profile { get; set; }

        public UserProfile()
        {
            
        }
    }
}