using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Address;
using Domain.AddressUser;
using Domain.Users;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;
using AddressUserFactory = Infrastructure.SqlServer.Factory.AddressUserFactory;


namespace Infrastructure.SqlServer.AddressUser
{
    public class AddressUserRepository : IAddressUserRepository
    {
        private readonly IInstanceFromReaderFactory<IAddressUser> _addressUserFactory = new AddressUserFactory();
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        public AddressUserRepository(IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        public IAddressUser GetByUser(IUser user)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();

                cmd.CommandText = AddressUserSqlServer.ReqQueryJoinUsersAndAddress2;
                cmd.Parameters.AddWithValue($"@{AddressUserSqlServer.ColIdUser}", user.Id);

                var reader = cmd.ExecuteReader(CommandBehavior.CloseConnection);

                
                return reader.Read() ? _addressUserFactory.CreateFromReader(reader) : null;
                
            }
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

                
                return reader.Read() ? _addressUserFactory.CreateFromReader(reader) : null;
                
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

        public IEnumerable<IAddressUser> Query()
        {
            IList<IAddressUser> addressUsers = new List<IAddressUser>();
            
            using (var connection = Database.GetConnection())
            {
                //Connection à la base de donnée
                connection.Open();
                //Crée une commande qui contiendra la requête demandée
                var command = connection.CreateCommand();
                command.CommandText = AddressUserSqlServer.ReqQuery;

                //Création d'un reader qui fermera la connection à la fin de la lecture
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                //Pour chaque ligne lue, crée un user que l'on rajoute dans la liste users
                while (reader.Read())
                {
                    addressUsers.Add(_addressUserFactory.CreateFromReader(reader));
                }
            }

            return addressUsers;
        }

        public bool DeleteAddress(int idAddress)
        {
            bool hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = AddressUserSqlServer.ReqDeleteAddress;

                command.Parameters.AddWithValue($"@{AddressUserSqlServer.ColIdAddress}", idAddress);

                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }

            return hasBeenDeleted;
        }
    }
}