using System;
using System.Collections.Generic;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using Domain.Users;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;
using Microsoft.IdentityModel.Tokens;
using pAPI.Extensions;
using Application.Repositories;
using UserFactory = Infrastructure.SqlServer.Factory.UserFactory;

namespace Infrastructure.SqlServer.Users
{
    /*public interface IUserRepository2
    {
        IUser Authenticate(string mail, string password);
        IEnumerable<IUser> Query();
    }*/
    public class UserRepository : IUserRepository
    {
        private readonly IInstanceFromReaderFactory<IUser> _userFactory = new UserFactory();

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
                command.CommandText = UserSqlServer.ReqQuery;

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
                command.CommandText = UserSqlServer.ReqGet;

                command.Parameters.AddWithValue($"@{UserSqlServer.ColId}", id);

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
                command.CommandText = UserSqlServer.ReqCreate;
                
                Console.WriteLine("test");

                command.Parameters.AddWithValue($"@{UserSqlServer.ColMail}", user.Mail);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColPassword}",user.Password);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColLastConnexion}", user.LastConnexion);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColAdmin}", false);

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
                command.CommandText = UserSqlServer.ReqDelete;

                command.Parameters.AddWithValue($"@{UserSqlServer.ColId}", id);

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
                command.CommandText = UserSqlServer.ReqUpdate;
                
                command.Parameters.AddWithValue($"@{UserSqlServer.ColMail}", user.Mail);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColPassword}",user.Password);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColLastConnexion}", user.LastConnexion);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColAdmin}", user.Admin);
                command.Parameters.AddWithValue($"@{UserSqlServer.ColId}", id);

                hasBeenChanged = command.ExecuteNonQuery() == 1;
            }
            return hasBeenChanged;
        }

        public IUser Authenticate(string mail, string password)
        {
            IUser _user = Query().SingleOrDefault(x=>x.Mail == mail && x.Password == password);

            //Null si pas d'utilisateur trouvé
            if (_user == null)
            {
                return null;
            }

            //Génération d'un JWT Token

            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes("E1CA8D28EBE516DBEA0A2D6019CD8B559C788912360FB8FB7A4C0F3BEB5B59FA");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim(ClaimTypes.Name, _user.Id.ToString()) 
                }),
                Expires = DateTime.UtcNow.AddDays(30),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            _user.Token = tokenHandler.WriteToken(token);

            return _user.WithoutPassword();
        }
    }
}