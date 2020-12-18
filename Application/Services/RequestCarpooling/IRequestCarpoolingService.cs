using System.Collections.Generic;
using Application.Services.RequestCarpooling.DTO;

namespace Application.Services.RequestCarpooling
{
    public interface IRequestCarpoolingService
    {
        IEnumerable<OutputDtoRequestCarpooling> GetRequestByIdReceiver(InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver);

        bool AddCarPoolingRequest(InputDtoAddCarpoolingRequest inputDtoAddCarpoolingRequest);

        bool DeleteRequestCarpooling(InputDtoDeleteRequestCarpooling inputDtoDeleteRequestCarpooling);
        IEnumerable<OutputDtoRequestCarpooling> GetRequestByIdSender(InputDtoGetRequestByIdSender inputDtoGetRequestByIdSender);
        OutputDtoRequestCarpooling GetRequestByIdSenderReceiver(InputDtoGetRequestByIdSender inputDtoGetRequestByIdSender, InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver);

        bool UpdateConfirmation(InputDtoUpdateConfirmation confirmation);
        IEnumerable<OutputDtoRequestCarpoolingById> GetRequestProfileByIdReceiver(InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver);
        bool DeleteAllByIdReceiver(InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver);

        IEnumerable<OutputDtoRequestCarpoolingById> GetRequestProfileByIdSender(
            InputDtoGetRequestByIdSender inputDtoGetRequestByIdSender);
    }
}