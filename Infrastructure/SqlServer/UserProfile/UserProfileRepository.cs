using System.Collections.Generic;
using Application.Repositories;
using Domain.Profile;
using Domain.UserProfile;
using Domain.Users;
using Infrastructure.SqlServer.Factory;
using Infrastructure.SqlServer.Shared;

namespace Infrastructure.SqlServer.UserProfile
{
    public class UserProfileRepository : IUserProfileRepository
    {
        private readonly IInstanceFromReaderFactory<IUserProfile> _factory = new UserProfileFactory();
        
        public IEnumerable<IUserProfile> GetByUser(IUser user)
        {
            throw new System.NotImplementedException();
        }

        public IUserProfile CreateUserProfile(IProfile profile, IUser user)
        {
            var userProfile = new Domain.UserProfile.UserProfile
            {
                Profile = profile,
                User = user
            };
            using (var connection = Database.GetConnection())
            {
                connection.Open();
                var cmd = connection.CreateCommand();
                cmd.CommandText = UserProfileSqlServer.ReqCreate;
                
                cmd.Parameters.AddWithValue($"@{UserProfileSqlServer.ColIdProfile}", profile.Id);
                cmd.Parameters.AddWithValue($"@{UserProfileSqlServer.ColIdUser}", user.Id);

                userProfile.Id = (int) cmd.ExecuteScalar();
            }

            return userProfile;
        }

        public bool Delete(int profileId, int profileUser)
        {
            throw new System.NotImplementedException();
        }
    }
}