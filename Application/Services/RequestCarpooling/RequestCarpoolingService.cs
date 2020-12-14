using System;
using System.Collections.Generic;
using System.Diagnostics;
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
            var requestInDb = _requestCarpoolingRepository.GetReqestByIdSender(idRequestSender);
            
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