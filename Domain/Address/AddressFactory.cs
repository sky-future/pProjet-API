namespace Domain.Address
{
    public class AddressFactory : IAddressFactory
    {
        public IAddress CreateAddress(string street, int number, int postalCode, string city, string country, string longitude,
            string latitude)
        {
            return new Address
            {
                Street = street,
                Number = number,
                PostalCode = postalCode,
                City = city,
                Country = country,
                Longitude = longitude,
                Latitude = latitude
                
            };
        }

        public IAddress CreateAddressWithoutCoord(string street, int number, int postalCode, string city, string country)
        {
            return new Address
            {
                Street = street,
                Number = number,
                PostalCode = postalCode,
                City = city,
                Country = country
            };
        }
        
        
        
    }
}