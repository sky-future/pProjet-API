namespace Infrastructure.SqlServer.Profile
{
    public class ProfileSqlServer
    {
        public static readonly string TableName = "profile";
        public static readonly string ColId = "id";
        public static readonly string ColLastName = "lastname";
        public static readonly string ColFirstName = "firstname";
        public static readonly string ColMatricule = "matricule";
        public static readonly string ColTelephone = "telephone";
        public static readonly string ColDescription = "descript";

        public static readonly string ReqQuery = $"SELECT * FROM {TableName}";
        public static readonly string ReqGet = ReqQuery + $" WHERE {ColId} = @{ColId}";
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColLastName},{ColFirstName},{ColMatricule},{ColTelephone},{ColDescription})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColLastName},@{ColFirstName},@{ColMatricule},@{ColTelephone},@{ColDescription})";

        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqUpdate = $@"
            UPDATE {TableName} SET
            {ColLastName} = @{ColLastName},
            {ColFirstName} = @{ColFirstName},
            {ColMatricule} = @{ColMatricule},
            {ColTelephone} = @{ColTelephone},
            {ColDescription} = @{ColDescription}
            WHERE {ColId} = @{ColId}
        ";
    }
}