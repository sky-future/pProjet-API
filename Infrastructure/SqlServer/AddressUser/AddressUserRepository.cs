using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Application.Services.Address;
using Application.Services.Users;
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
        
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;

        public AddressUserRepository(IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        public IEnumerable<IAddressUser> GetByUser(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public IAddressUser GetByAddress(IAddress address)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = AddressUserSqlServer.ReqQueryJoinUsersAndAddress;
                cmd.Parameters.AddWithValue($"@{AddressUserSqlServer.ColIdAddress}", address.Id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                
                return reader.Read() ? _factory.CreateFromReader(reader) : null;
                
            }
        }
        
        public IAddressUser CreateAddressUser(int idUser, int idAddress)
        {
            var userAddress = new Domain.AddressUser.AddressUser
            {
                User = _userRepository.GetById(idUser),
                Address = _addressRepository.GetById(idAddress)
            };
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = AddressUserSqlServer.ReqCreate;
                
                cmd.Parameters.AddWithValue($"@{AddressUserSqlServer.ColIdUser}", idUser);
                cmd.Parameters.AddWithValue($"@{AddressUserSqlServer.ColIdAddress}", idAddress);

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