namespace Infrastructure.SqlServer.Users
{
    public class UserSqlServer
    {
        public static readonly string TableName = "userLa";
        public static readonly string ColId = "id";
        public static readonly string ColMail = "mail";
        public static readonly string ColPassword = "password";
        public static readonly string ColLastConnexion = "lastConnexion";
        public static readonly string ColAdmin = "admin";

        public static readonly string ReqQuery = $"SELECT * FROM {TableName}";
        public static readonly string ReqGet = ReqQuery + $" WHERE {ColId} = @{ColId}";
        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColMail},{ColPassword},{ColLastConnexion},{ColAdmin})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColMail},@{ColPassword},@{ColLastConnexion},@{ColAdmin})";

        public static readonly string ReqDelete = $"DELETE FROM {TableName} WHERE {ColId} = @{ColId}";
        public static readonly string ReqUpdate = $@"
            UPDATE {TableName} SET
            {ColMail} = @{ColMail},
            {ColPassword} = @{ColPassword},
            {ColLastConnexion} = @{ColLastConnexion},
            {ColAdmin} = @{ColAdmin}
            WHERE {ColId} = @{ColId}
        ";
    }
}