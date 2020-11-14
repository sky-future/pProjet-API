using System.Data.SqlClient;
using Domain.Users;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Factory
{
    public class UserFactory : IInstanceFromReaderFactory<IUser>
    {
        public IUser CreateFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal(UserSqlServer.ColId)),
                Mail = reader.GetString(reader.GetOrdinal(UserSqlServer.ColMail)),
                Password = reader.GetString(reader.GetOrdinal(UserSqlServer.ColPassword)),
                LastConnexion = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastConnexion)),
                Admin = reader.GetBoolean(reader.GetOrdinal(UserSqlServer.ColAdmin))
            };
        }
    }
}