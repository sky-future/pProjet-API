using Domain.Shared;

namespace Domain.OfferCarpooling
{
    public interface IOfferCarpooling : IEntity
    {
        public int IdUser { get; set; }
    }
}