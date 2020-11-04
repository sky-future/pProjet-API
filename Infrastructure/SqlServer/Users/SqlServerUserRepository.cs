using System;
using System.Collections.Generic;
using System.Data;
using System.Net;
using Domain.Users;

namespace Infrastructure.SqlServer.Users
{
    public class SqlServerUserRepository: IUserRepository
    {
        public static readonly string TableName = "UserLa";
        public static readonly string ColId = "id";
        public static readonly string ColMail = "mail";
        public static readonly string ColPassword = "password";
        public static readonly string ColLastConnexion = "lastConnexion";
        public static readonly string ColAdmin = "admin";

        public static readonly string ReqQuery = $"SELECT * FROM {TableName}";
        public static readonly string ReqGet = ReqQuery + $" WHERE {ColId} = @{ColId}";
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColMail},{ColPassword},{ColLastConnexion},{ColAdmin})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColMail},@{ColPassword},@{ColLastConnexion},0)";

        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqUpdate = $@"
            UPDATE {TableName} SET
            {ColMail} = @{ColMail},
            {ColPassword} = @{ColPassword},
            {ColLastConnexion} = @{ColLastConnexion},
            {ColAdmin} = @{ColAdmin}
            WHERE {ColId} = @{ColId}
        ";
        
        private IUserFactory _userFactory = new UserFactory();
        
        //Renvoie toutes les données de la table
        public IEnumerable<IUser> Query()
        {
            IList<IUser> users = new List<IUser>();
            
            using (var connection = Database.GetConnection())
            {
                //Connection à la base de donnée
                connection.Open();
                //Crée une commande qui contiendra la requête demandée
                var command = connection.CreateCommand();
                command.CommandText = ReqQuery;

                //Création d'un reader qui fermera la connection à la fin de la lecture
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                //Pour chaque ligne lue, crée un user que l'on rajoute dans la liste users
                while (reader.Read())
                {
                    users.Add(_userFactory.CreateFromReader(reader));
                }
            }

            return users;
        }

        public IUser Get(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ReqGet;

                command.Parameters.AddWithValue($"@{ColId}", id);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader.Read() ? _userFactory.CreateFromReader(reader) : null;
            }
        }

        public IUser Create(IUser user)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ReqCreate;

                command.Parameters.AddWithValue($"@{ColMail}", user.Mail);
                command.Parameters.AddWithValue($"@{ColPassword}",user.Password);
                command.Parameters.AddWithValue($"@{ColLastConnexion}", user.LastConnexion);
                //command.Parameters.AddWithValue($"@{ColAdmin}", user.Admin);

                try
                {
                    user.Id = (int) command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }

            return user;
        }

        public bool Delete(int id)
        {
            bool hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = ReqDelete;

                command.Parameters.AddWithValue($"@{ColId}", id);

                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }

            return hasBeenDeleted;
        }

        public bool Update(int id, IUser user)
        {
            bool hasBeenChanged = false;
            
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = ReqUpdate;
                
                command.Parameters.AddWithValue($"@{ColMail}", user.Mail);
                command.Parameters.AddWithValue($"@{ColPassword}",user.Password);
                command.Parameters.AddWithValue($"@{ColLastConnexion}", user.LastConnexion);
                command.Parameters.AddWithValue($"@{ColAdmin}", user.Admin);
                command.Parameters.AddWithValue($"@{ColId}", id);

                hasBeenChanged = command.ExecuteNonQuery() == 1;
            }

            return hasBeenChanged;
        }
    }
}