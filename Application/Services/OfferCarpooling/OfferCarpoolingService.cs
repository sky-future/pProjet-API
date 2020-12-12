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

        public OfferCarpoolingService(IOfferCarpoolingRepository offerCarpoolingRepository)
        {
            _offerCarpoolingRepository = offerCarpoolingRepository;
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
    }
}