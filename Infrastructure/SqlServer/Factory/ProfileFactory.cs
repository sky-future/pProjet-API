using System.Data.SqlClient;
using Domain.Profile;
using Infrastructure.SqlServer.Address;
using Infrastructure.SqlServer.Profile;

namespace Infrastructure.SqlServer.Factory
{
    public class ProfileFactory : IInstanceFromReaderFactory<IProfile>
    {
        public IProfile CreateFromReader(SqlDataReader reader)
        {
            return new Domain.Profile.Profile
            {
                Id = reader.GetInt32(reader.GetOrdinal(ProfileSqlServer.ColId)),
                Lastname = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColLastName)),
                Firstname = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColFirstName)),
                Matricule = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColMatricule)),
                Telephone = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColTelephone)),
                Descript = reader.GetString(reader.GetOrdinal(ProfileSqlServer.ColDescription)),
            };
        }
        
    }
}