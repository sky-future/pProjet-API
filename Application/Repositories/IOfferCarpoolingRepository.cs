using System.Collections.Generic;
using Domain.OfferCarpooling;

namespace Application.Repositories
{
    public interface IOfferCarpoolingRepository
    {
        IEnumerable<IOfferCarpooling> Query();
        IOfferCarpooling Create(IOfferCarpooling offerCarpooling);
        bool DeleteById(int id);
        bool DeleteByIdUser(int idUser);

    }
}