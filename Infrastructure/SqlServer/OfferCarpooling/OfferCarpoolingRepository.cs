using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.OfferCarpooling;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;
using OfferCarpoolingFactory = Infrastructure.SqlServer.Factory.OfferCarpoolingFactory;

namespace Infrastructure.SqlServer.OfferCarpooling
{
    public class OfferCarpoolingRepository : IOfferCarpoolingRepository
    {
        private readonly IInstanceFromReaderFactory<IOfferCarpooling> _offerCarpoolingFactory = new OfferCarpoolingFactory();

        public IEnumerable<IOfferCarpooling> Query()
        {
            IList<IOfferCarpooling> offerCarpoolings = new List<IOfferCarpooling>();
            
            using (var connection = Database.GetConnection())
            {
                //Connection à la base de donnée
                connection.Open();
                //Crée une commande qui contiendra la requête demandée
                var command = connection.CreateCommand();
                command.CommandText = OfferCarpoolingSqlServer.ReqQuery;

                //Création d'un reader qui fermera la connection à la fin de la lecture
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                //Pour chaque ligne lue, crée un user que l'on rajoute dans la liste users
                while (reader.Read())
                {
                    offerCarpoolings.Add(_offerCarpoolingFactory.CreateFromReader(reader));
                }
            }

            return offerCarpoolings;
        }
        
        public IOfferCarpooling Create(IOfferCarpooling offerCarpooling)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = OfferCarpoolingSqlServer.ReqCreate;

                command.Parameters.AddWithValue($"@{OfferCarpoolingSqlServer.ColIdUser}",offerCarpooling.IdUser);

                try
                {
                    offerCarpooling.Id = (int) command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
            
            return offerCarpooling;
        }

        public bool DeleteById(int id)
        {
            bool hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = OfferCarpoolingSqlServer.ReqDelete;

                command.Parameters.AddWithValue($"@{OfferCarpoolingSqlServer.ColId}", id);

                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }

            return hasBeenDeleted;
        }

        public bool DeleteByIdUser(int idUser)
        {
            bool hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = OfferCarpoolingSqlServer.ReqDeleteIdUser;

                command.Parameters.AddWithValue($"@{OfferCarpoolingSqlServer.ColIdUser}", idUser);

                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }

            return hasBeenDeleted;
        }

        public IOfferCarpooling GetByIdUser(int idUser)
        {
            using (var connection = Database.GetConnection())
            {
                //Connection à la base de donnée
                connection.Open();
                //Crée une commande qui contiendra la requête demandée
                var command = connection.CreateCommand();
                command.CommandText = OfferCarpoolingSqlServer.ReqGetByIdUser;

                command.Parameters.AddWithValue($"@{OfferCarpoolingSqlServer.ColIdUser}", idUser);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader.Read() ? _offerCarpoolingFactory.CreateFromReader(reader) : null;
                
            }
        }
    }
}