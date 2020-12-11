using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Address;
using Domain.AddressUser;
using Domain.Users;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.AddressUser
{
    public class AddressUserRepository : IAddressUserRepository
    {
        private readonly IInstanceFromReaderFactory<IAddressUser> _factory = new AddressUserFactory();
        
        public IEnumerable<IAddressUser> GetByUser(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public IEnumerable<IAddressUser> GetByAddress(IAddress address)
        {
            IList<IAddressUser> addressUsers = new List<IAddressUser>();
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = AddressUserSqlServer.ReqQueryJoinUsersAndAddress;
                cmd.Parameters.AddWithValue($"@{AddressUserSqlServer.ColIdAddress}", address.Id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    addressUsers.Add(_factory.CreateFromReader(reader));
                }
            }

            return addressUsers;
        }
        
        public IAddressUser CreateAddressUser(IUser user, IAddress address)
        {
            var userAddress = new Domain.AddressUser.AddressUser
            {
                User = user,
                Address = address
            };
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = AddressUserSqlServer.ReqCreate;
                
                cmd.Parameters.AddWithValue($"@{AddressUserSqlServer.ColIdUser}", user.Id);
                cmd.Parameters.AddWithValue($"@{AddressUserSqlServer.ColIdAddress}", address.Id);

                userAddress.Id = (int) cmd.ExecuteScalar();
            }

            return userAddress;
        }

        public bool Delete(int idUser, int idAddress)
        {
            throw new System.NotImplementedException();
        }
    }
}