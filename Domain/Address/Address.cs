namespace Domain.Address
{
    public class Address : IAddress
    {
        public int Id { get; set; }
        public string Street { get; set; }
        public int Number { get; set; }
        public int PostalCode { get; set; }
        public string City { get; set; }
        public string Country { get; set; }
        public string Longitude { get; set; }
        public string Latitude { get; set; }

        public Address()
        {
            
        }

        public Address(int id, string street, int number, int postalCode, string city, string country, string longitude, string latitude)
        {
            Id = id;
            Street = street;
            Number = number;
            PostalCode = postalCode;
            City = city;
            Country = country;
            Longitude = longitude;
            Latitude = latitude;
        }
    }
}