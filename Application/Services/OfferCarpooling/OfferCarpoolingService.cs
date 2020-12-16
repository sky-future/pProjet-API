using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.OfferCarpooling.DTO;
using Application.Services.Profile.DTO;

namespace Application.Services.OfferCarpooling
{
    public class OfferCarpoolingService : IOfferCarpoolingService
    {
        private readonly IOfferCarpoolingRepository _offerCarpoolingRepository;
        private readonly IAddressUserRepository _addressUserRepository;
        private readonly IProfileRepository _profileRepository;
        private readonly ICarRepository _carRepository;
        private readonly IAddressRepository _addressRepository;

        public OfferCarpoolingService(IOfferCarpoolingRepository offerCarpoolingRepository, IAddressUserRepository addressUserRepository, IProfileRepository profileRepository, ICarRepository carRepository, IAddressRepository addressRepository)
        {
            _offerCarpoolingRepository = offerCarpoolingRepository;
            _addressUserRepository = addressUserRepository;
            _profileRepository = profileRepository;
            _carRepository = carRepository;
            _addressRepository = addressRepository;
        }

        public IEnumerable<OutputDtoQueryCarpooling> Query()
        {
            return _offerCarpoolingRepository
                .Query()
                .Select(offerCarpooling => new OutputDtoQueryCarpooling()
                {
                    Id = offerCarpooling.Id,
                    IdUser = offerCarpooling.IdUser
                });
        }
        
        public OutputDtoAddOfferCarpooling Create(InputDtoAddOfferCarpooling inputDtoAddOfferCarpooling)
        {
            var offerCarpoolingFromDto = new Domain.OfferCarpooling.OfferCarpooling
            {
                IdUser = inputDtoAddOfferCarpooling.IdUser
            };
            
            var offerCarpoolingInDb = _offerCarpoolingRepository.Create(offerCarpoolingFromDto);

            return new OutputDtoAddOfferCarpooling()
            {
                Id = offerCarpoolingInDb.Id,
                IdUser = offerCarpoolingInDb.IdUser
            };
        }
        
        public bool DeleteById(InputDtoDeleteById inputDtoDeleteById)
        {
            return _offerCarpoolingRepository.DeleteById(inputDtoDeleteById.Id);
        }

        public OutputDtoInfoModal GetInfoModal(InputDtoIdAddress inputDtoIdAddress)
        {
            var idUser = _addressUserRepository.GetByAddress(_addressRepository.GetById(inputDtoIdAddress.IdAddress)).User.Id;

            if (idUser == 0)
            {
                return null;
            }

            var profile = _profileRepository.GetByIdUser(idUser);

            if (profile == null)
            {
                return null;
            }

            var car = _carRepository.GetByIdUserCar(idUser);

            if (car == null)
            {
                return null;
            }
            
            var infoModal = new OutputDtoInfoModal
            {
                IdUser = idUser,
                Firstname = profile.Firstname,
                Lastname = profile.Lastname,
                Telephone = profile.Telephone,
                PlaceNb = car.PlaceNb
            };

            return infoModal;
        }
    }
}