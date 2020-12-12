using System.Data.SqlClient;
using Domain.OfferCarpooling;
using Infrastructure.SqlServer.OfferCarpooling;

namespace Infrastructure.SqlServer.Factory
{
    public class OfferCarpoolingFactory : IInstanceFromReaderFactory<IOfferCarpooling>
    {
        public IOfferCarpooling CreateFromReader(SqlDataReader reader)
        {
            return new Domain.OfferCarpooling.OfferCarpooling
            {
                Id = reader.GetInt32(reader.GetOrdinal(OfferCarpoolingSqlServer.ColId)),
                IdUser = reader.GetInt32(reader.GetOrdinal(OfferCarpoolingSqlServer.ColIdUser))
            };
        }
    }
}