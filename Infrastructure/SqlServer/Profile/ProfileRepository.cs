using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Profile;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;
using ProfileFactory = Infrastructure.SqlServer.Factory.ProfileFactory;

namespace Infrastructure.SqlServer.Profile
{
    public class ProfileRepository : IProfileRepository
    {
        
        private readonly IInstanceFromReaderFactory<IProfile> _profileFactory = new ProfileFactory();
        
        public IEnumerable<IProfile> Query()
        {
            
            IList<IProfile> profile = new List<IProfile>();
            
            using (var connection = Database.GetConnection())
            {
                //Connection à la base de donnée
                connection.Open();
                //Crée une commande qui contiendra la requête demandée
                var command = connection.CreateCommand();
                command.CommandText = ProfileSqlServer.ReqQuery;

                //Création d'un reader qui fermera la connection à la fin de la lecture
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                //Pour chaque ligne lue, crée un user que l'on rajoute dans la liste users
                while (reader.Read())
                {
                    profile.Add(_profileFactory.CreateFromReader(reader));
                }
            }

            return profile;
        }

        public IProfile GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ProfileSqlServer.ReqGet;

                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColId}", id);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader.Read() ? _profileFactory.CreateFromReader(reader) : null;
            }
        }

        public IProfile GetByIdUser(int idUser)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ProfileSqlServer.ReqGetUser;

                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColIdUser}", idUser);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader.Read() ? _profileFactory.CreateFromReader(reader) : null;
            }
            
        }

        public IProfile Create(IProfile profile)
        {
            
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ProfileSqlServer.ReqCreate;

                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColLastName}", profile.Lastname);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColFirstName}",profile.Firstname);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColMatricule}", profile.Matricule);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColTelephone}", profile.Telephone);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColDescription}", profile.Descript);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColIdUser}", profile.IdUser);

                try
                {
                    profile.Id = (int) command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
            
            return profile;
        }

        public bool DeleteById(int id)
        {
            
            bool hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ProfileSqlServer.ReqDelete;

                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColId}", id);

                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }

            return hasBeenDeleted;
            
        }

        public bool Update(int id, IProfile profile)
        {
            
            bool hasBeenChanged = false;
            
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = ProfileSqlServer.ReqUpdate;
                
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColLastName}", profile.Lastname);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColFirstName}",profile.Firstname);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColMatricule}", profile.Matricule);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColTelephone}", profile.Telephone);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColDescription}", profile.Descript);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColIdUser}", profile.IdUser);
                command.Parameters.AddWithValue($"@{ProfileSqlServer.ColId}", id);


                hasBeenChanged = command.ExecuteNonQuery() == 1;
            }
            return hasBeenChanged;
            
        }
    }
}