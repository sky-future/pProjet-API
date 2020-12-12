namespace Infrastructure.SqlServer.Cars
{
    public class CarSqlServer
    {
        public static readonly string TableName = "car";
        public static readonly string ColId = "id";
        public static readonly string ColImmatriculation = "immatriculation";
        public static readonly string ColIdUser = "idUser";
        public static readonly string ColPlaceNb = "placeNb";
        
        public static readonly string ReqQuery = $"SELECT * FROM {TableName}";
        public static readonly string ReqGet = ReqQuery + $" WHERE {ColId} = @{ColId}";
        public static readonly string ReqGetUser = ReqQuery + $" WHERE {ColIdUser} = @{ColIdUser}";
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColImmatriculation},{ColIdUser},{ColPlaceNb})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColImmatriculation},@{ColIdUser},@{ColPlaceNb})";
        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqUpdate = $@"
            UPDATE {TableName} SET
            {ColImmatriculation} = @{ColImmatriculation},
            {ColIdUser} = @{ColIdUser},
            {ColPlaceNb} = @{ColPlaceNb}
            WHERE {ColId} = @{ColId}
        ";
    }
}