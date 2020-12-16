using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.RequestCarpooling.DTO;
using Domain.Profile;
using Domain.RequestCarpooling;

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
        
        public IEnumerable<OutputDtoRequestCarpoolingById> GetRequestProfileByIdReceiver(
            InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver)
        {
            
            var requestInDb = _requestCarpoolingRepository.GetRequestByIdReceiver(inputDtoGetRequestByIdReceiver.IdRequestReceiver);

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

        public IEnumerable<OutputDtoRequestCarpooling> GetRequestByIdReceiver(InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver)
        {
            var requestInDb = _requestCarpoolingRepository.GetRequestByIdReceiver(inputDtoGetRequestByIdReceiver.IdRequestReceiver);

            if (requestInDb == null)
                return null;
            
            return requestInDb.Select( output => new OutputDtoRequestCarpooling
            {
                Id = output.Id,
                IdRequestSender = output.IdRequestSender,
                IdRequestReceiver = output.IdRequestReceiver,
                Confirmation = output.Confirmation
            });
        }

        public bool AddCarPoolingRequest(InputDtoAddCarpoolingRequest inputDtoAddCarpoolingRequest)
        {
            
            var requestFromDto = _requestCarpoolingFactory.CreateRequest(
                        inputDtoAddCarpoolingRequest.IdRequestSender,
                        inputDtoAddCarpoolingRequest.IdRequestReceiver,
                        inputDtoAddCarpoolingRequest.Confirmation);

            return _requestCarpoolingRepository.Create(requestFromDto);
            
        }


        public IEnumerable<OutputDtoRequestCarpooling> GetRequestByIdSender(InputDtoGetRequestByIdSender inputDtoGetRequestByIdSender)
        {
            var requestInDb = _requestCarpoolingRepository.GetRequestByIdSender(inputDtoGetRequestByIdSender.IdSender);

            if (requestInDb == null)
                return null;
            
            return requestInDb.Select( output => new OutputDtoRequestCarpooling
            {
                Id = output.Id,
                IdRequestSender = output.IdRequestSender,
                IdRequestReceiver = output.IdRequestReceiver,
                Confirmation = output.Confirmation
            });
        }

        public OutputDtoRequestCarpooling GetRequestByIdSenderReceiver(InputDtoGetRequestByIdSender inputDtoGetRequestByIdSender, InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver)
        {
            var requestInDb = _requestCarpoolingRepository.GetRequestByTwoId(inputDtoGetRequestByIdSender.IdSender, inputDtoGetRequestByIdReceiver.IdRequestReceiver);

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
            
            bool done = _requestCarpoolingRepository.UpdateConfirmationRequest(confirmation);

            if (_carRepository.GetByIdUserCar(confirmation.IdRequestReceiver).PlaceNb == 0)
            {
                var requestReceiver = _requestCarpoolingRepository.GetRequestByIdReceiver(confirmation.IdRequestReceiver);

                foreach (var request in requestReceiver)
                {
                    if (request.Confirmation == 0)
                    {
                        _requestCarpoolingRepository.Delete(request.IdRequestSender, request.IdRequestReceiver);
                    }
                }
            }

            return done;
        }
    }
}