using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.RequestCarpooling.DTO;
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
        
        public IEnumerable<OutputDTORequestCarpoolingByID> GetByIdReceiver(
            InputDTOGetByIDRequestCarpooling inputDtoGetByIdRequestCarpooling)
        {
            var requestInDb = _requestCarpoolingRepository.GetByIdReceiver(inputDtoGetByIdRequestCarpooling.idRequestReceiver);
            
            IList<int> idSenderLists = new List<int>();
            
            IList<OutputDTORequestCarpoolingByID> outputDtoRequestCarpoolingByIds = new List<OutputDTORequestCarpoolingByID>();
            
            foreach (var request in requestInDb)
            {
                var profile = _profileRepository.GetByIdUser(request.IdRequestSender);
                OutputDTORequestCarpoolingByID test = new OutputDTORequestCarpoolingByID
                {
                    IdUser = request.IdRequestSender,
                    Lastname = profile.Lastname,
                    Firstname = profile.Firstname,
                    Telephone = profile.Telephone,
                    Confirmation = request.Confirmation
                };
                outputDtoRequestCarpoolingByIds.Add(test);
                // idSenderLists.Add(request.IdRequestSender);   
            }
            
            return outputDtoRequestCarpoolingByIds.Select( output => new OutputDTORequestCarpoolingByID
            {
                IdUser = output.IdUser,
                Lastname = output.Lastname,
                Firstname = output.Firstname,
                Telephone = output.Telephone,
                Confirmation = output.Confirmation
            });
        }
    }
}