using System;

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

        protected bool Equals(Address other)
        {
            return Id == other.Id && Street == other.Street && Number == other.Number && PostalCode == other.PostalCode && City == other.City && Country == other.Country && Longitude == other.Longitude && Latitude == other.Latitude;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Address) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Street, Number, PostalCode, City, Country, Longitude, Latitude);
        }
    }
}