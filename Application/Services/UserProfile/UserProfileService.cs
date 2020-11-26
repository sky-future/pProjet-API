using System;
using Application.Repositories;
using Application.Services.UserProfile.Dto;
using Domain.Profile;
using Domain.UserProfile;

namespace Application.Services.UserProfile
{
    public class UserProfileService : IUserProfileService
    {
        private readonly IUserProfileRepository _userProfileRepository;
        private readonly IUserRepository _userRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileFactory _profileFactory = new ProfileFactory();

        public UserProfileService(IUserProfileRepository userProfileRepository, IUserRepository userRepository,
            IProfileRepository profileRepository)
        {
            _userProfileRepository = userProfileRepository;
            _userRepository = userRepository;
            _profileRepository = profileRepository;
        }
        
        public OutputDtoCreateUserProfile CreateUserProfile(InputDtoIdUserCreateUserProfile inputDtoIdUserCreateUserProfile,
            InputDtoProfileCreateUserProfile inputDtoProfileCreateUserPofile)
        {
            var user = _userRepository.GetById(inputDtoIdUserCreateUserProfile.IdUser);
            Console.WriteLine(user);
            var profile = _profileFactory.CreateProfileWithId(
                inputDtoProfileCreateUserPofile.Profile.Id,
                inputDtoProfileCreateUserPofile.Profile.Lastname,
                inputDtoProfileCreateUserPofile.Profile.Firstname,
                inputDtoProfileCreateUserPofile.Profile.Matricule,
                inputDtoProfileCreateUserPofile.Profile.Telephone,
                inputDtoProfileCreateUserPofile.Profile.Descript
            );
            Console.WriteLine(profile);

            var userProfileInDb = _userProfileRepository.CreateUserProfile(profile, user);

            return new OutputDtoCreateUserProfile()
            {
                IdProfile = userProfileInDb.Profile.Id,
                IdUser = userProfileInDb.User.Id,
                Id = userProfileInDb.Id
            };
        }
    }
}