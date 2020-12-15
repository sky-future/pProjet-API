using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Domain.Cars;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;
using Infrastructure.SqlServer.Users;
using CarFactory = Infrastructure.SqlServer.Factory.CarFactory;

namespace Infrastructure.SqlServer.Cars
{
    public class CarRepository : ICarRepository
    {
        private readonly IInstanceFromReaderFactory<ICar> _carFactory = new CarFactory();
        
        public IEnumerable<ICar> Query()
        {
            IList<ICar> cars = new List<ICar>();
            
            using (var connection = Database.GetConnection())
            {
                //Connection à la base de donnée
                connection.Open();
                //Crée une commande qui contiendra la requête demandée
                var command = connection.CreateCommand();
                command.CommandText = CarSqlServer.ReqQuery;

                //Création d'un reader qui fermera la connection à la fin de la lecture
                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                //Pour chaque ligne lue, crée un user que l'on rajoute dans la liste users
                while (reader.Read())
                {
                    cars.Add(_carFactory.CreateFromReader(reader));
                }
            }

            return cars;
        }

        public ICar GetById(int id)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = CarSqlServer.ReqGet;

                command.Parameters.AddWithValue($"@{CarSqlServer.ColId}", id);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader.Read() ? _carFactory.CreateFromReader(reader) : null;
            }
        }

        public ICar Create(ICar car)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = CarSqlServer.ReqCreate;

                command.Parameters.AddWithValue($"@{CarSqlServer.ColImmatriculation}", car.Immatriculation);
                command.Parameters.AddWithValue($"@{CarSqlServer.ColIdUser}",car.IdUser);
                command.Parameters.AddWithValue($"@{CarSqlServer.ColPlaceNb}", car.PlaceNb);
            // vérifie si la voiture a bien été créé, en verifiant la valeur contenu dans l'id de la voiture crée.                                 
                try
                {
                    car.Id = (int) command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return null;
                }
            }
            
            return car;
        }

        public bool DeleteById(int id)
        {
            bool hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = CarSqlServer.ReqDelete;

                command.Parameters.AddWithValue($"@{CarSqlServer.ColId}", id);

                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }

            return hasBeenDeleted;
        }

        public bool Update(int id, ICar car)
        {
            bool hasBeenChanged = false;
            
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = CarSqlServer.ReqUpdate;
                
                command.Parameters.AddWithValue($"@{CarSqlServer.ColImmatriculation}", car.Immatriculation);
                command.Parameters.AddWithValue($"@{CarSqlServer.ColIdUser}",car.IdUser);
                command.Parameters.AddWithValue($"@{CarSqlServer.ColPlaceNb}", car.PlaceNb);
                command.Parameters.AddWithValue($"@{CarSqlServer.ColId}", id);

                hasBeenChanged = command.ExecuteNonQuery() == 1;
            }
            return hasBeenChanged;
        }
        
        public ICar GetByIdUserCar(int idUser)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = CarSqlServer.ReqGetUser;

                command.Parameters.AddWithValue($"@{CarSqlServer.ColIdUser}", idUser);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                return reader.Read() ? _carFactory.CreateFromReader(reader) : null;
            }
            
        }
    }
}