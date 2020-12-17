using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
using Application.Services.RequestCarpooling.DTO;
using Domain.RequestCarpooling;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;
using RequestCarpoolingFactory = Infrastructure.SqlServer.Factory.RequestCarpoolingFactory;


namespace Infrastructure.SqlServer.RequestCarpooling
{
    public class RequestCarpoolingRepository : IRequestCarpoolingRepository
    {
        private readonly IInstanceFromReaderFactory<IRequestCarpooling> _requestCarpoolingFactory = new RequestCarpoolingFactory();
        //IEnumerable car on retourne une liste de CarpoolingRequests
        public IEnumerable<IRequestCarpooling> GetRequestByIdReceiver(int idReceiver)
        {
            IList<IRequestCarpooling> requestCarpoolings = new List<IRequestCarpooling>();

            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.RequestCarPoolingProfileById;

                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestReceiver}", idReceiver);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    requestCarpoolings.Add(_requestCarpoolingFactory.CreateFromReader(reader));
                }
            }

            return requestCarpoolings;
        }

        public IEnumerable<IRequestCarpooling> GetRequestByIdSender(int idRequestSender)
        {
            IList<IRequestCarpooling> requestCarpoolings = new List<IRequestCarpooling>();

            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.ReqGetByIdSender;

                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestSender}", idRequestSender);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    requestCarpoolings.Add(_requestCarpoolingFactory.CreateFromReader(reader));
                }

                return requestCarpoolings;
            }
        }

        public IRequestCarpooling GetRequestByTwoId(int idSender, int idReceiver)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.ReqGetByIdSenderReceiver;

                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestSender}", idSender);
                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestReceiver}", idReceiver);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);
                
                return reader.Read() ? _requestCarpoolingFactory.CreateFromReader(reader) : null;

            }
        }

        public bool Create(IRequestCarpooling requestCarpooling)
        {
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.ReqCreate;

                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestSender}",
                    requestCarpooling.IdRequestSender);
                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestReceiver}",
                    requestCarpooling.IdRequestReceiver);
                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColConfirmation}",
                    requestCarpooling.Confirmation);

                try
                {
                    requestCarpooling.Id = (int) command.ExecuteScalar();
                }
                catch (Exception e)
                {
                    Console.WriteLine(e);
                    return false;
                }

            }

            return true;
        }

        public bool Delete(int idSender, int idReceiver)
        {
            var hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.ReqDel;

                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestSender}", idSender);
                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestReceiver}", idReceiver);
                
                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }
            
            return hasBeenDeleted;
        }

        public bool UpdateConfirmationRequest(InputDtoUpdateConfirmation confirmation)
        {
            var hasBeenChanged = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.ReqUpdateConfirmation;

                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestSender}",
                    confirmation.IdRequestSender);
                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestReceiver}",
                    confirmation.IdRequestReceiver);
                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColConfirmation}New",
                    confirmation.Confirmation);

                hasBeenChanged = command.ExecuteNonQuery() == 1;

            }

            return hasBeenChanged;
        }

        public bool DeleteAllByIdReceiver(int idReceiver)
        {
            var hasBeenDeleted = false;

            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.ReqDelAllByIdReceiver;
                
                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestReceiver}", idReceiver);
                
                hasBeenDeleted = command.ExecuteNonQuery() == 1;

            }
            
            return hasBeenDeleted;
        }
    }
    
}