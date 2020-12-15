namespace Infrastructure.SqlServer.RequestCarpooling
{
    public class RequestCarpoolingSqlServer
    {
        public static readonly string TableName = "carPoolingRequests";
        public static readonly string ColIdCarPoolingRequest = "id";
        public static readonly string ColIdRequestSender = "idRequestSender";
        public static readonly string ColIdRequestReceiver = "idRequestReceiver";
        public static readonly string ColConfirmation = "confirmation";

        public static readonly string RequestCarPoolingProfileById = $@"
                SELECT * FROM {TableName}
                WHERE {ColIdRequestReceiver} = @{ColIdRequestReceiver}";

        public static readonly string ReqCreate = $@"
                INSERT INTO {TableName} ({ColIdRequestSender},{ColIdRequestReceiver},{ColConfirmation})
                OUTPUT INSERTED.{ColIdCarPoolingRequest} 
                VALUES(@{ColIdRequestSender}, @{ColIdRequestReceiver}, @{ColConfirmation})";

        public static readonly string ReqQueryByIdRequestSender = $@"
                SELECT * FROM {TableName}
                WHERE {ColIdRequestSender} = @{ColIdRequestSender}";

        public static readonly string ReqDel = $@"DELETE FROM {TableName} WHERE {ColIdRequestSender} = @{ColIdRequestSender} AND {ColIdRequestReceiver} = @{ColIdRequestReceiver}";

        public static readonly string Req_Update_Confirmation = $@"
                UPDATE {TableName}
                SET {ColConfirmation} = @{ColConfirmation}NEW
                WHERE {ColIdRequestSender} = @{ColIdRequestSender}
                AND {ColIdRequestReceiver} = @{ColIdRequestReceiver}";
    }
}