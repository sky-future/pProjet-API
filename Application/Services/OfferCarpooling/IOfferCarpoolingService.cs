using System.Collections.Generic;
using Application.Services.OfferCarpooling.DTO;

namespace Application.Services.OfferCarpooling
{
    public interface IOfferCarpoolingService
    {
        IEnumerable<OutputDtoQueryCarpooling> Query();
        OutputDtoAddOfferCarpooling Create(InputDtoAddOfferCarpooling inputDtoAddOfferCarpooling);
        public bool DeleteById(InputDtoDeleteById inputDtoDeleteById);
        public OutputDtoInfoModal GetInfoModal(InputDtoIdAddress inputDtoIdAddress);
        public OutputDtoQueryCarpooling GetByIdUser(InputDtoAddOfferCarpooling inputDtoAddOfferCarpooling);
    }
}