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
                IdRequestSender = reader.GetInt32(reader.GetOrdinal(RequestCarpoolingSqlServer.ColIdRequestSender)),
                IdRequestReceiver = reader.GetInt32(reader.GetOrdinal(RequestCarpoolingSqlServer.ColIdRequestReceiver)),
                Confirmation = reader.GetInt32(reader.GetOrdinal(RequestCarpoolingSqlServer.ColConfirmation))
            };
        }
    }
}