using System.Data.SqlClient;
using Domain.RequestCarpooling;
using Infrastructure.SqlServer.RequestCarpooling;


namespace Infrastructure.SqlServer.Factory
{
    public class RequestCarpoolingFactory : IInstanceFromReaderFactory<IRequestCarpooling>
    {
        public IRequestCarpooling CreateFromReader(SqlDataReader reader)
        {
            return new Domain.RequestCarpooling.RequestCarpooling
            {
                Id = reader.GetInt32(reader.GetOrdinal(RequestCarpoolingSqlServer.ColIdCarPoolingRequest)),
                Confirmation = reader.GetBoolean(reader.GetOrdinal(RequestCarpoolingSqlServer.ColConfirmation)),
                IdRequestReceiver = reader.GetInt32(reader.GetOrdinal(RequestCarpoolingSqlServer.ColIdRequestReceiver)),
                IdRequestSender = reader.GetInt32(reader.GetOrdinal(RequestCarpoolingSqlServer.ColIdRequestSender))
            };
        }
    }
}