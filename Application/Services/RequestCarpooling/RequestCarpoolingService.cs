using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using Application.Repositories;
using Application.Services.RequestCarpooling.DTO;
using Domain.Profile;
using Domain.RequestCarpooling;
using static System.Console;

namespace Application.Services.RequestCarpooling
{
    public class RequestCarpoolingService : IRequestCarpoolingService
    {
        private readonly IRequestCarpoolingRepository _requestCarpoolingRepository;
        private readonly IRequestCarpoolingFactory _requestCarpoolingFactory = new RequestCarpoolingFactory();
        private readonly IProfileRepository _profileRepository;
        private readonly ICarRepository _carRepository;
        private readonly IOfferCarpoolingRepository _offerCarpoolingRepository;
        
        public RequestCarpoolingService(IRequestCarpoolingRepository requestCarpoolingRepository, IProfileRepository profileRepository, ICarRepository carRepository, IOfferCarpoolingRepository offerCarpoolingRepository)
        {
            _requestCarpoolingRepository = requestCarpoolingRepository;
            _profileRepository = profileRepository;
            _carRepository = carRepository;
            _offerCarpoolingRepository = offerCarpoolingRepository;
        }
        
        public IEnumerable<OutputDtoRequestCarpoolingById> GetByIdReceiver(
            InputDtoGetByIdRequestCarpooling inputDtoGetByIdRequestCarpooling)
        {
            
            var requestInDb = _requestCarpoolingRepository.GetByIdReceiver(inputDtoGetByIdRequestCarpooling.IdRequestReceiver);

            IProfile profile;
            IList<OutputDtoRequestCarpoolingById> outputDtoRequestCarpoolingByIds = new List<OutputDtoRequestCarpoolingById>();

            foreach (var request in requestInDb)
            {
                profile = _profileRepository.GetByIdUser(request.IdRequestSender);
                var output = new OutputDtoRequestCarpoolingById
                {
                    IdUser = request.IdRequestSender,
                    Firstname = profile.Firstname,
                    Lastname = profile.Lastname,
                    Telephone = profile.Telephone,
                    Confirmation = request.Confirmation
                };
                outputDtoRequestCarpoolingByIds.Add(output);
            }

            return outputDtoRequestCarpoolingByIds;
        }

        public bool AddCarPoolingRequest(InputDtoAddCarpoolingRequest inputDtoAddCarpoolingRequest)
        {
            
            var requestFromDto = _requestCarpoolingFactory.CreateRequest(
                        inputDtoAddCarpoolingRequest.IdRequestSender,
                        inputDtoAddCarpoolingRequest.IdRequestReceiver,
                        inputDtoAddCarpoolingRequest.Confirmation);

            return _requestCarpoolingRepository.Create(requestFromDto);
            
        }
        

        public OutputDtoRequestCarpooling GetSenderById(int idRequestSender)
        {
            var requestInDb = _requestCarpoolingRepository.GetRequestByIdSender(idRequestSender);

            if (requestInDb == null)
                return null;
            
            return new OutputDtoRequestCarpooling
            {
                Id = requestInDb.Id,
                IdRequestSender = requestInDb.IdRequestSender,
                IdRequestReceiver = requestInDb.IdRequestReceiver,
                Confirmation = requestInDb.Confirmation
            };
        }

        public bool DeleteRequestCarpooling(InputDtoDeleteRequestCarpooling inputDtoDeleteRequestCarpooling)
        {
            return _requestCarpoolingRepository.Delete(inputDtoDeleteRequestCarpooling.IdSender,
                inputDtoDeleteRequestCarpooling.IdReceiver);
        }

        public bool UpdateConfirmation(InputDtoUpdateConfirmation confirmation)
        {
            if (confirmation.Confirmation == 1 &&  _carRepository.GetByIdUserCar(confirmation.IdRequestReceiver).PlaceNb - 1 == 0)
            {
                _offerCarpoolingRepository.DeleteByIdUser(confirmation.IdRequestReceiver);
            }
            
            _carRepository.PatchPlaceNb(_carRepository.GetByIdUserCar(confirmation.IdRequestReceiver).PlaceNb - 1,
                confirmation.IdRequestReceiver);
            
            return _requestCarpoolingRepository.UpdateConfirmationRequest(confirmation);
        }
    }
}