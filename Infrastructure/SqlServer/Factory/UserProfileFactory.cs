using System.Data.SqlClient;
using System.Xml.Xsl;
using Domain.UserProfile;
using Domain.Users;
using Infrastructure.SqlServer.Profile;
using Infrastructure.SqlServer.UserProfile;
using Infrastructure.SqlServer.Users;

namespace Infrastructure.SqlServer.Factory
{
    public class UserProfileFactory : IInstanceFromReaderFactory<IUserProfile>
    {
        public IUserProfile CreateFromReader(SqlDataReader reader)
        {
            return new Domain.UserProfile.UserProfile
            {
                Profile = new Domain.Profile.Profile
                {
                    Id = reader.GetInt32(reader.GetOrdinal(UserProfileSqlServer.ColIdProfile)),
                    Lastname = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColLastName)),
                    Firstname = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColFirstName)),
                    Matricule = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColMatricule)),
                    Telephone = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColTelephone)),
                    Descript = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColDescription))
                },
                User = new User
                {
                    Id = reader.GetInt32(reader.GetOrdinal(UserProfileSqlServer.ColIdUser)),
                    Mail = reader.GetString(reader.GetOrdinal(UserSqlServer.ColMail)),
                    LastConnexion = reader.GetString(reader.GetOrdinal(UserSqlServer.ColLastConnexion))
                },
                Id = reader.GetInt32(reader.GetOrdinal(UserProfileSqlServer.ColId))
            };
        }
    }
}