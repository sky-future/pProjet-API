using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Profile.DTO;
using Domain.Profile;

namespace Application.Services.Profile
{
    public class ProfileService : IProfileService
    {
        
        private readonly IProfileRepository _profileRepository;
        private readonly IProfileFactory _profileFactory = new ProfileFactory();
        
        public ProfileService(IProfileRepository profileRepository)
        {
            _profileRepository = profileRepository;
        }
        
        public IEnumerable<OutputDtoQueryProfile> Query()
        {
            
            return _profileRepository
                .Query()
                .Select(profile => new OutputDtoQueryProfile()
                {
                    Id =  profile.Id,
                    Lastname = profile.Lastname,
                    Firstname = profile.Firstname,
                    Matricule = profile.Matricule,
                    Telephone = profile.Telephone,
                    Description = profile.Descript,
                });
            
        }

        public OutputDtoAddProfile Create(InputDtoAddProfile inputDtoAddProfile)
        {

            var profileFromDto = _profileFactory.CreateProfile(
                inputDtoAddProfile.Lastname,
                inputDtoAddProfile.Firstname,
                inputDtoAddProfile.Matricule,
                inputDtoAddProfile.Telephone,
                inputDtoAddProfile.Descript);

            
            var profileInDb = _profileRepository.Create(profileFromDto);
            
            return new OutputDtoAddProfile()
            {
                Id =  profileInDb.Id,
                Lastname = profileInDb.Lastname,
                Firstname = profileInDb.Firstname,
                Matricule = profileInDb.Matricule,
                Telephone = profileInDb.Telephone,
                Descript = profileInDb.Descript,
            };
            
        }

        public bool Update(int id, InputDtoUpdateProfile inputDtoUpdateAddress)
        {
            
            var profileFromDto = _profileFactory.CreateProfile(
                inputDtoUpdateAddress.Lastname,
                inputDtoUpdateAddress.Firstname,
                inputDtoUpdateAddress.Matricule,
                inputDtoUpdateAddress.Telephone,
                inputDtoUpdateAddress.Descript);
            
            return _profileRepository.Update(id, profileFromDto);
        }

        public OutputDtoGetByIdProfile GetById(InputDtoGetByIdProfile inputDtoGetByIdProfile)
        {
            
            var profileInDb = _profileRepository.GetById(inputDtoGetByIdProfile.Id);
            
            return new OutputDtoGetByIdProfile()
            {
                Id =  profileInDb.Id,
                Lastname = profileInDb.Lastname,
                Firstname = profileInDb.Firstname,
                Matricule = profileInDb.Matricule,
                Telephone = profileInDb.Telephone,
                Descript = profileInDb.Descript,
            };
            
        }

        public bool DeleteById(InputDtoDeleteByIdProfile inputDtoDeleteByIdProfile)
        {
            return _profileRepository.DeleteById(inputDtoDeleteByIdProfile.Id);
        }
    }
}