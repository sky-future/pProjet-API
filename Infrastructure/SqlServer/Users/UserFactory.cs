using System.Data.SqlClient;
using Domain.Users;

namespace Infrastructure.SqlServer.Users
{
    public class UserFactory : IUserFactory
    {
        public IUser CreateFromReader(SqlDataReader reader)
        {
            return new User
            {
                Id = reader.GetInt32(reader.GetOrdinal(SqlServerUserRepository.ColId)),
                Mail = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColMail)),
                Password = reader.GetString(reader.GetOrdinal(SqlServerUserRepository.ColPassword)),
                LastConnexion = reader.GetDateTime(reader.GetOrdinal(SqlServerUserRepository.ColLastConnexion)),
                Admin = reader.GetBoolean(reader.GetOrdinal(SqlServerUserRepository.ColAdmin))
            };
        }
    }
}