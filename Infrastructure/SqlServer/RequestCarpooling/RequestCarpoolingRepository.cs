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

                command.Parameters.AddWithValue($"@{RequestCarpoolingSqlServer.ColIdRequestReceiver}", 3);

                var reader = command.ExecuteReader(CommandBehavior.CloseConnection);

                while (reader.Read())
                {
                    requestCarpoolings.Add(_requestCarpoolingFactory.CreateFromReader(reader));
                }
            }

            return requestCarpoolings;
        }

    }
    
}