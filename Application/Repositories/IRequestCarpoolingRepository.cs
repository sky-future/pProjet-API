using System.Collections.Generic;
using Domain.RequestCarpooling;

namespace Application.Repositories
{
    public interface IRequestCarpoolingRepository
    {
        IEnumerable<IRequestCarpooling> GetByIdReceiver(int idUser);
        IRequestCarpooling GetRequestByIdSender(int idRequestSender);

        bool Create(IRequestCarpooling requestCarpooling);
        bool Delete(int idSender, int idReceiver);
    }
}