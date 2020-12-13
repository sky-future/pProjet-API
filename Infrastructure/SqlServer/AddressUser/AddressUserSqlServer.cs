using Infrastructure.SqlServer.Address;
using Infrastructure.SqlServer.Profile;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.AddressUser
{
    public class AddressUserSqlServer
    {
        public static readonly string TableName = "addressUser";
        public static readonly string ColId = "id";
        public static readonly string ColIdUser = "idUser";
        public static readonly string ColIdAddress = "idAddress";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColIdUser}, {ColIdAddress})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColIdUser},@{ColIdAddress})
        ";

        public static readonly string ReqQueryJoinUsersAndAddress =
            $@"SELECT *, userLa.mail, userLa.lastConnexion, address.street, address.number, address.postalCode,
            address.city, address.country, address.longitude, address.latitude FROM {TableName}
            INNER JOIN {UserSqlServer.TableName} userLa ON {ColIdUser} = userLa.{UserSqlServer.ColId}
            INNER JOIN {AddressSqlServer.TableName} address ON {ColIdAddress} = address.{AddressSqlServer.ColId}
            WHERE {ColIdAddress} = @{ColIdAddress}";

        public static readonly string ReqQueryJoinUsersAndAddress2 =
            $@"SELECT *, userLa.mail, userLa.lastConnexion, address.street, address.number, address.postalCode,
            address.city, address.country, address.longitude, address.latitude FROM {TableName}
            INNER JOIN {UserSqlServer.TableName} userLa ON {ColIdUser} = userLa.{UserSqlServer.ColId}
            INNER JOIN {AddressSqlServer.TableName} address ON {ColIdAddress} = address.{AddressSqlServer.ColId}
            WHERE {ColIdUser} = @{ColIdUser}";
        
        public static readonly string reqDelete = $@"DELETE FROM {TableName}
            WHERE {ColIdUser} = @{ColIdUser} AND {ColId} = @{ColId}";
    }
}