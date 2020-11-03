using System.Data.SqlClient;

namespace Infrastructure.SqlServer
{
    public static class Database
    {
        private static readonly string ConnectionString = @"Server=LAPTOP-LO5RF0IB\SQLEXPRESS;Database=User;Integrated Security=SSPI";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}