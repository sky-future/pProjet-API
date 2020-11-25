using System.ComponentModel;
using Infrastructure.SqlServer.Profile;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.UserProfile
{
    public class UserProfileSqlServer
    {
        public static readonly string TableName = "userProfile";
        public static readonly string ColId = "id";
        public static readonly string ColIdProfile = "idProfile";
        public static readonly string ColIdUser = "idUser";

        public static readonly string ReqCreate = $@"
            INSERT INTO {TableName}({ColIdProfile}, {ColIdUser})
            OUTPUT INSERTED.{ColId}
            VALUES(@{ColIdProfile},@{ColIdUser})
        ";

        public static readonly string ReqQueryJoinUsersAndProfiles =
            $@"SELECT *, profile.lastname, profile.firstname, profile.matricule, profile.telephone, profile.descript, userLa.mail, userLa.lastConnexion FROM {TableName}
            INNER JOIN {ProfileSqlServer.TableName} profile ON {ColIdProfile} = profile.{ProfileSqlServer.ColId}
            INNER JOIN {UserSqlServer.TableName} userLa ON {ColIdUser} = userLa.{UserSqlServer.ColId}
            WHERE {ColIdUser} = @{ColIdUser}";

        public static readonly string reqDelete = $@"DELETE FROM {TableName}
            WHERE {ColIdUser} = @{ColIdUser} AND {ColId} = @{ColId}";
    }
}