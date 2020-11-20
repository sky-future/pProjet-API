namespace Domain.Address
{
    public interface IAddressFactory
    {
        IAddress CreateAddress(string street, int number, int postalCode, string city, string country, string longitude,
            string latitude);
        IAddress CreateAddressWithoutCoord(string street, int number, int postalCode, string city, string country);
    }
}