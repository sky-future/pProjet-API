using System.Collections.Generic;
using System.Data;
using System.Net;
using Domain.Users;

namespace Infrastructure.SqlServer.Users
{
    public class SqlServerUserRepository: IUserRepository
    {
        public static readonly string TableName = "test";
        public static readonly string ColId = "id";
        public static readonly string ColMail = "mail";
        public static readonly string ColPassword = "password";
        public static readonly string ColLastConnexion = "lastConnexion";
        public static readonly string ColAdmin = "admin";

        public static readonly string ReqQuery = $"SELECT * FROM {TableName}";
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColMail},{ColPassword},{ColLastConnexion},{ColAdmin})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColMail},@{ColPassword},@{ColLastConnexion},0)";
        
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
            throw new System.NotImplementedException();
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

                user.Id = (int)command.ExecuteScalar();
            }

            return user;
        }

        public bool Delete(int id)
        {
            throw new System.NotImplementedException();
        }

        public bool Update(int id, IUser user)
        {
            throw new System.NotImplementedException();
        }
    }
}