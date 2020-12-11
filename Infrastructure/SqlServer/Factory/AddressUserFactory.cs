using System;
using System.Data.SqlClient;
using Domain.AddressUser;
using Domain.Users;
using Infrastructure.SqlServer.Address;
using Infrastructure.SqlServer.AddressUser;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Factory
{
    public class AddressUserFactory : IInstanceFromReaderFactory<IAddressUser>
    {
        public IAddressUser CreateFromReader(SqlDataReader reader)
        {
            return new Domain.AddressUser.AddressUser
            {
                User = new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal(AddressUserSqlServer.ColIdUser)),
                    Mail = reader.GetString(reader.GetOrdinal(UserSqlServer.ColMail)),
                    LastConnexion = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastConnexion))
                },
                Address = new Domain.Address.Address
                {
                    Id = reader.GetInt32(reader.GetOrdinal(AddressUserSqlServer.ColIdAddress)),
                    Street = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColStreet)),
                    Number = reader.GetInt32(reader.GetOrdinal(AddressSqlServer.ColNumber)),
                    PostalCode = reader.GetInt32(reader.GetOrdinal(AddressSqlServer.ColPostalCode)),
                    City = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColCity)),
                    Country = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColCountry)),
                    Longitude = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColLongitude)),
                    Latitude = reader.GetString(reader.GetOrdinal(AddressSqlServer.ColLatitude))
                },
                Id = reader.GetInt32(reader.GetOrdinal(AddressUserSqlServer.ColId))
            };
        }
    }
}