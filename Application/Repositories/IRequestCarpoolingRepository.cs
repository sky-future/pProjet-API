using System.Collections.Generic;
using Application.Services.RequestCarpooling.DTO;
using Domain.RequestCarpooling;

namespace Application.Repositories
{
    public interface IRequestCarpoolingRepository
    {
        IEnumerable<IRequestCarpooling> GetRequestByIdReceiver(int idReceiver);
        IEnumerable<IRequestCarpooling> GetRequestByIdSender(int idRequestSender);

        IRequestCarpooling GetRequestByTwoId(int idSender, int idReceiver);

        bool Create(IRequestCarpooling requestCarpooling);
        bool Delete(int idSender, int idReceiver);

        bool UpdateConfirmationRequest(InputDtoUpdateConfirmation confirmation);
    }
}