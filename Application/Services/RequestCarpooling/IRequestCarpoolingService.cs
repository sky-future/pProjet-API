using System.Collections.Generic;
using Application.Services.Address.Dto;
using Application.Services.RequestCarpooling.DTO;

namespace Application.Services.RequestCarpooling
{
    public interface IRequestCarpoolingService
    {
        IEnumerable<OutputDTORequestCarpoolingByID> GetByIdReceiver(
            InputDTOGetByIDRequestCarpooling inputDtoGetByIdRequestCarpooling);

        bool AddCarPoolingRequest(InputDTOAddCarpoolingRequest inputDtoAddCarpoolingRequest);

        OutputDtoRequestCarpooling GetSenderById(int idRequestSender);
    }
}