using Application.Services.Profile.DTO;
using Application.Services.UserProfile.Dto;

namespace Application.Services.UserProfile
{
    public interface IUserProfileService
    {
        OutputDtoCreateUserProfile CreateUserProfile(InputDtoIdUserCreateUserProfile inputDtoIdUserCreateUserProfile,
            InputDtoProfileCreateUserProfile inputDtoProfileCreateUserPofile);
    }
}