using System.Runtime;
using Domain.Shared;

namespace Domain.Address
{
    public interface IAddress : IEntity
    {
        public string Street { get; set; }
        public int Number { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }
        
    }
}