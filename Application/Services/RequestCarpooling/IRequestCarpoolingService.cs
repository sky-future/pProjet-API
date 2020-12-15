using System.Collections.Generic;
using Application.Services.Address.Dto;
using Application.Services.RequestCarpooling.DTO;

namespace Application.Services.RequestCarpooling
{
    public interface IRequestCarpoolingService
    {
        IEnumerable<OutputDtoRequestCarpoolingById> GetByIdReceiver(
            InputDtoGetByIdRequestCarpooling inputDtoGetByIdRequestCarpooling);

        bool AddCarPoolingRequest(InputDtoAddCarpoolingRequest inputDtoAddCarpoolingRequest);

        OutputDtoRequestCarpooling GetSenderById(int idRequestSender);
        bool DeleteRequestCarpooling(InputDtoDeleteRequestCarpooling inputDtoDeleteRequestCarpooling);

        bool UpdateConfirmation(InputDtoUpdateConfirmation confirmation);
    }
}