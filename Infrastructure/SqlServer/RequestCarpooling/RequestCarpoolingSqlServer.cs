namespace Infrastructure.SqlServer.RequestCarpooling
{
    public class RequestCarpoolingSqlServer
    {
        public static readonly string TableName = "carPoolingRequests";
        public static readonly string ColIdCarPoolingRequest = "id";
        public static readonly string ColIdRequestSender = "idRequestSender";
        public static readonly string ColIdRequestReceiver = "idRequestReceiver";
        public static readonly string ColConfirmation = "confirmation";

        public static readonly string RequestCarPoolingProfileById = $@"SELECT * FROM {TableName} WHERE {ColIdRequestReceiver} = @{ColIdRequestReceiver}";

    }
}