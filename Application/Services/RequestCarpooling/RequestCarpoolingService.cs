using System;
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
        
        public RequestCarpoolingService(IRequestCarpoolingRepository requestCarpoolingRepository, IProfileRepository profileRepository)
        {
            _requestCarpoolingRepository = requestCarpoolingRepository;
            _profileRepository = profileRepository;
        }
        
        public IEnumerable<OutputDtoRequestCarpoolingById> GetByIdReceiver(
            InputDtoGetByIdRequestCarpooling inputDtoGetByIdRequestCarpooling)
        {
            var requestInDb = _requestCarpoolingRepository.GetByIdReceiver(inputDtoGetByIdRequestCarpooling.IdRequestReceiver);
            
            IList<IProfile> profiles = new List<IProfile>();

            foreach (var request in requestInDb)
            {
                profiles.Add(_profileRepository.GetByIdUser(request.IdRequestSender));
            }

            var outputDtoRequestCarpoolingByIds = profiles.Select( output => new OutputDtoRequestCarpoolingById
            {
                IdUser = output.IdUser,
                Lastname = output.Lastname,
                Firstname = output.Firstname,
                Telephone = output.Telephone,
                Confirmation = requestInDb.GetEnumerator().Current.Confirmation
            });

            foreach (var request in requestInDb)
            {
                Console.WriteLine(outputDtoRequestCarpoolingByIds.GetEnumerator().Current.IdUser);
                outputDtoRequestCarpoolingByIds.GetEnumerator().Current.Confirmation = request.Confirmation;
            }

            return outputDtoRequestCarpoolingByIds;
        }

        public bool AddCarPoolingRequest(InputDTOAddCarpoolingRequest inputDtoAddCarpoolingRequest)
        {
            
            var requestFromDto = _requestCarpoolingFactory.createRequest(
                        inputDtoAddCarpoolingRequest.IdRequestSender,
                        inputDtoAddCarpoolingRequest.IdRequestReceiver,
                        inputDtoAddCarpoolingRequest.Confirmation);

            return _requestCarpoolingRepository.Create(requestFromDto);
            
        }
        

        public OutputDtoRequestCarpooling GetSenderById(int idRequestSender)
        {
            var requestInDb =
                _requestCarpoolingRepository.GetReqestByIdSender(idRequestSender);
            
            return new OutputDtoRequestCarpooling
            {
                Id = requestInDb.Id,
                IdRequestSender = requestInDb.IdRequestSender,
                IdRequestReceiver = requestInDb.IdRequestReceiver,
                Confirmation = requestInDb.Confirmation
            };
        }

       
    }
}