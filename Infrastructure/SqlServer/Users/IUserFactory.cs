using System.Data.SqlClient;
using Domain.Users;

namespace Infrastructure.SqlServer.Users
{
    public interface IUserFactory
    {
        IUser CreateFromReader(SqlDataReader reader);
    }
}