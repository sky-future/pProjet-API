using System.Data.SqlClient;

namespace Infrastructure.SqlServer.Factory
{
    public interface IInstanceFromReaderFactory<out T>
    {
        T CreateFromReader(SqlDataReader reader);
    }
}