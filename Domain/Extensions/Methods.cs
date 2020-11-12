using System.Collections.Generic;
using System.Linq;
using Domain.Users;

namespace pAPI.Extensions
{
    public static class Methods
    {
        public static IEnumerable<IUser> WithoutPasswordUsers(this IEnumerable<IUser> users)
        {
            return users.Select(x => x.WithoutPassword());
        }

        public static IUser WithoutPassword(this IUser user)
        {
            user.Password = null;
            return user;
        }
    }
}