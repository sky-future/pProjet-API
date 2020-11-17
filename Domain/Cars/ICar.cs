using Domain.Shared;

namespace Domain.Cars
{
    public interface ICar : IEntity
    {

        public string Immatriculation { get; set; }
        public int IdUser { get; set; }
        public int PlaceNb { get; set; }
        
    }
}