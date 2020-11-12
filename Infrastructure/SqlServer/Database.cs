using System.Data.SqlClient;

namespace Infrastructure.SqlServer
{
    public static class Database
    {
        private static readonly string ConnectionString = @"Server=127.0.0.1,1433;Database=pGestionHelha;User id=SA;Password=ingeid/3510";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}