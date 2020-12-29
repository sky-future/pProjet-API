using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Shared
{
    public static class Database
    {
        //private static readonly string ConnectionString = @"Server=127.0.0.1,1433;Database=pGestionHelha;User id=SA;Password=ingeid/3510";
        private static readonly string ConnectionString = @"Server=tcp:pgestionhelhaprojet.database.windows.net,1433;Initial Catalog=pGestionHelha;Persist Security Info=False;User ID=Falcon;Password=ingeid/3510;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;";

        public static SqlConnection GetConnection()
        {
            return new SqlConnection(ConnectionString);
        }
    }
}