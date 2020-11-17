using System.Data.SqlClient;
using Domain.Cars;
using Infrastructure.SqlServer.Cars;

namespace Infrastructure.SqlServer.Factory
{
    public class CarFactory : IInstanceFromReaderFactory<ICar>
    {
        public ICar CreateFromReader(SqlDataReader reader)
        {
            return new Car
            {
                Id = reader.GetInt32(reader.GetOrdinal(CarSqlServer.ColId)),
                Immatriculation = reader.GetString(reader.GetOrdinal(CarSqlServer.ColImmatriculation)),
                IdUser = reader.GetInt32(reader.GetOrdinal(CarSqlServer.ColIdUser)),
                PlaceNb = reader.GetInt32(reader.GetOrdinal(CarSqlServer.ColPlaceNb))
            };
        }
    }
}