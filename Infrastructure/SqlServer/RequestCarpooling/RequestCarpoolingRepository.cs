using System;
using System.Collections.Generic;
using System.Data;
using Application.Repositories;
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
        public IEnumerable<IRequestCarpooling> GetByIdReceiver(int idReceiver)
        {
            IList<IRequestCarpooling> requestCarpoolings = new List<IRequestCarpooling>();
                
            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.RequestCarPoolingProfileById;

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                       requestCarpoolings.Add(_requestCarpoolingFactory.CreateFromReader(reader));
                }
            }

            return requestCarpoolings;
        }

        public IRequestCarpooling GetReqestByIdSender(int idRequestSender)
        {
            IRequestCarpooling request = new Domain.RequestCarpooling.RequestCarpooling();

            using (var connection = Database.GetConnection())
            {
                connection.Open();

                var command = connection.CreateCommand();
                command.CommandText = RequestCarpoolingSqlServer.Req_Query_By_IdRequestSender;

                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestSender}", idRequestSender);

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
                    return false;
                }

            }

            return true;
        }
    }
    
    
}