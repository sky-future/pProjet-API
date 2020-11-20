namespace Infrastructure.SqlServer.Address
{
    public class AddressSqlServer
    {
        public static readonly string TableName = "address";
        public static readonly string ColId = "id";
        public static readonly string ColStreet = "street";
        public static readonly string ColNumber = "number";
        public static readonly string ColPostalCode = "postalCode";
        public static readonly string ColCity = "city";
        public static readonly string ColCountry = "country";
        public static readonly string ColLongitude = "longitude";
        public static readonly string ColLatitude = "latitude";
        
        public static readonly string ReqQuery = $"SELECT * FROM {TableName}";
        public static readonly string ReqGet = ReqQuery + $" WHERE {ColId} = @{ColId}";
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColStreet},{ColNumber},{ColPostalCode},{ColCity}, {ColCountry}, {ColLongitude}, {ColLatitude})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColStreet},@{ColNumber},@{ColPostalCode},@{ColCity},@{ColCountry},@{ColLongitude},@{ColLatitude}";

        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqUpdate = $@"
            UPDATE {TableName} SET
            {ColStreet} = @{ColStreet},
            {ColNumber} = @{ColNumber},
            {ColPostalCode} = @{ColPostalCode},
            {ColCity} = @{ColCity},
            {ColCountry} = @{ColCountry},
            {ColLongitude} = @{ColLongitude},
            {ColLatitude} = @{ColLatitude}
            WHERE {ColId} = @{ColId}
        ";
    }
}