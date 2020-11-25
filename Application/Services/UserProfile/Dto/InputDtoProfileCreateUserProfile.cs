using Domain.Profile;

namespace Application.Services.UserProfile.Dto
{
    public abstract class InputDtoProfileCreateUserProfile
    {
        public IProfile Profile { get; set; }
    }
}