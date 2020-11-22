using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Address;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;
using AddressFactory = Infrastructure.SqlServer.Factory.AddressFactory;

namespace Infrastructure.SqlServer.Address
{
    public class AddressRepository : IAddressRepository
    {
        private readonly IInstanceFromReaderFactory<IAddress> _addressFactory = new AddressFactory();
        
        public IEnumerable<IAddress> Query()
        {
            IList<IAddress> address = new List<IAddress>();
            
            using (var connection = Database.GetConnection())
            {
                //Connection à la base de donnée
                connection.Open();
                //Crée une commande qui contiendra la requête demandée
                var command = connection.CreateCommand();
                command.CommandText = AddressSqlServer.ReqQuery;

                //Création d'un reader qui fermera la connection à la fin de la lecture
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                //Pour chaque ligne lue, crée un user que l'on rajoute dans la liste users
                while (reader.Read())
                {
                    address.Add(_addressFactory.CreateFromReader(reader));
                }
            }

            return address;
        }

        public IAddress GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = AddressSqlServer.ReqGet;

                command.Parameters.AddWithValue($"@{AddressSqlServer.ColId}", id);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader.Read() ? _addressFactory.CreateFromReader(reader) : null;
            }
        }

        public IAddress Create(IAddress address)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = AddressSqlServer.ReqCreate;

                command.Parameters.AddWithValue($"@{AddressSqlServer.ColStreet}", address.Street);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColNumber}", address.Number);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColPostalCode}", address.PostalCode);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColCity}", address.City);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColCountry}", address.Country);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColLongitude}", address.Longitude);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColLatitude}", address.Latitude);

                try
                {
                    address.Id = (int) command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
            
            return address;
        }

        public bool DeleteById(int id)
        {
            bool hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = AddressSqlServer.ReqDelete;

                command.Parameters.AddWithValue($"@{AddressSqlServer.ColId}", id);

                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }

            return hasBeenDeleted;
        }

        public bool Update(int id, IAddress address)
        {
            bool hasBeenChanged = false;
            
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = AddressSqlServer.ReqUpdate;
                
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColStreet}", address.Street);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColNumber}",address.Number);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColPostalCode}", address.PostalCode);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColCity}", address.City);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColCountry}",address.Country);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColLongitude}", address.Longitude);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColLatitude}", address.Latitude);
                command.Parameters.AddWithValue($"@{AddressSqlServer.ColId}", address.Id);

                hasBeenChanged = command.ExecuteNonQuery() == 1;
            }
            return hasBeenChanged;
        }
    }
}