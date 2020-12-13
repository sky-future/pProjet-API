using System.Collections.Generic;
using Domain.Address;

namespace Application.Services.AddressUser.Dto
{
    public class OutputDtoGetAddressListForCarpooling
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }    }
}