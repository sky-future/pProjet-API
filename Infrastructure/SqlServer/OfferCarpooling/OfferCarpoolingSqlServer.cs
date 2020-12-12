namespace Infrastructure.SqlServer.OfferCarpooling
{
    public class OfferCarpoolingSqlServer
    {
        public static readonly string TableName = "offerCarpooling";
        public static readonly string ColId = "id";
        public static readonly string ColIdUser = "idUser";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColIdUser})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColIdUser})
        ";

        public static readonly string ReqQuery = "";
    }
}