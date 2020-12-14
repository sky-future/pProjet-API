namespace Infrastructure.SqlServer.RequestCarpooling
{
    public class RequestCarpoolingSqlServer
    {
        public static readonly string TableName = "carPoolingRequests";
        public static readonly string ColIdCarPoolingRequest = "id";
        public static readonly string ColIdRequestSender = "idRequestSender";
        public static readonly string ColIdRequestReceiver = "idRequestReceiver";
        public static readonly string ColConfirmation = "confirmation";
        public static readonly string TableNameProfile = "profile";
        public static readonly string ColLastName = "lastname";
        public static readonly string ColFirstName = "firstname";
        public static readonly string ColTelephone = "telephone";
        public static readonly string ColIdProfile = "id";
       

        public static readonly string RequestCarPoolingProfileById = $@"
                SELECT * FROM {TableName}
                WHERE {ColIdRequestReceiver} = @{ColIdRequestReceiver}";

        public static readonly string ReqCreate = $@"
                INSERT INTO {TableName} VALUES ({ColIdRequestSender},{ColIdRequestReceiver},{ColConfirmation})";

        public static readonly string Req_Query_By_IdRequestSender = $@"
                SELECT * FROM {TableName} WHERE {ColIdRequestSender} = @{ColIdRequestSender}";

    }
}