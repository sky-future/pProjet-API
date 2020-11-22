using System.Data.SqlClient;
using Domain.Address;
using Infrastructure.SqlServer.Address;

namespace Infrastructure.SqlServer.Factory
{
    public class AddressFactory : IInstanceFromReaderFactory<IAddress>
    {
        public IAddress CreateFromReader(SqlDataReader reader)
        {
            return new Domain.Address.Address
            {
                Id = reader.GetInt32(reader.GetOrdinal(AddressSqlServer.ColId)),
                Street = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColStreet)),
                Number = reader.GetInt32(reader.GetOrdinal(AddressSqlServer.ColNumber)),
                PostalCode = reader.GetInt32(reader.GetOrdinal(AddressSqlServer.ColPostalCode)),
                City = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColCity)),
                Country = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColCountry)),
                Longitude = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColLongitude)),
                Latitude = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColLatitude))
            };
        }
    }
}