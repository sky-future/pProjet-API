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
            var queryInDb = _profileRepository.Query();

            if (queryInDb == null)
            {
                return null;
            }
            
            return queryInDb.Select(profile => new OutputDtoQueryProfile()
                {
                    Id = profile.Id,
                    Lastname = profile.Lastname,
                    Firstname = profile.Firstname,
                    Matricule = profile.Matricule,
                    Telephone = profile.Telephone,
                    Description = profile.Descript,
                    IdUser = profile.IdUser
                });
        }

        public OutputDtoAddProfile Create(InputDtoAddProfile inputDtoAddProfile)
        {
            var profileFromDto = _profileFactory.CreateProfile(
                inputDtoAddProfile.Lastname,
                inputDtoAddProfile.Firstname,
                inputDtoAddProfile.Matricule,
                inputDtoAddProfile.Telephone,
                inputDtoAddProfile.Descript,
                inputDtoAddProfile.IdUser
            );


            var profileInDb = _profileRepository.Create(profileFromDto);

            return new OutputDtoAddProfile()
            {
                Id = profileInDb.Id,
                Lastname = profileInDb.Lastname,
                Firstname = profileInDb.Firstname,
                Matricule = profileInDb.Matricule,
                Telephone = profileInDb.Telephone,
                Descript = profileInDb.Descript,
                IdUser = profileInDb.IdUser
            };
        }

        public bool Update(InputDtoUpdateByIdProfile inputDtoUpdateByIdProfile,
            InputDtoUpdateProfile inputDtoUpdateProfile)
        {
            var profileFromDto = _profileFactory.CreateProfile(
                inputDtoUpdateProfile.Lastname,
                inputDtoUpdateProfile.Firstname,
                inputDtoUpdateProfile.Matricule,
                inputDtoUpdateProfile.Telephone,
                inputDtoUpdateProfile.Descript,
                inputDtoUpdateProfile.IdUser
            );

            return _profileRepository.Update(inputDtoUpdateByIdProfile.Id, profileFromDto);
        }

        public OutputDtoGetByIdProfile GetById(InputDtoGetByIdProfile inputDtoGetByIdProfile)
        {
            var profileInDb = _profileRepository.GetById(inputDtoGetByIdProfile.Id);

            if (profileInDb == null)
            {
                return null;
            }

            return new OutputDtoGetByIdProfile()
            {
                Id = profileInDb.Id,
                Lastname = profileInDb.Lastname,
                Firstname = profileInDb.Firstname,
                Matricule = profileInDb.Matricule,
                Telephone = profileInDb.Telephone,
                Descript = profileInDb.Descript,
                IdUser = profileInDb.IdUser
            };
        }


        public OutputDtoGetByidUserProfile GetByUserIdProfile(InputDtoGetByidUserProfile inputDtoGetByidUserProfile)
        {
            var profileInDb = _profileRepository.GetByIdUser(inputDtoGetByidUserProfile.IdUser);

            if (profileInDb == null)
            {
                return null;
            }

            return new OutputDtoGetByidUserProfile()
            {
                Id = profileInDb.Id,
                Lastname = profileInDb.Lastname,
                Firstname = profileInDb.Firstname,
                Matricule = profileInDb.Matricule,
                Telephone = profileInDb.Telephone,
                Descript = profileInDb.Descript,
                IdUser = profileInDb.IdUser
            };
        }


        public bool DeleteById(InputDtoDeleteByIdProfile inputDtoDeleteByIdProfile)
        {
            return _profileRepository.DeleteById(inputDtoDeleteByIdProfile.Id);
        }
    }
}