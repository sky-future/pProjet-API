namespace Domain.Users
{
    public class UserFactory : IUserFactory
    {
        public IUser CreateSimpleUser(string mail, string password, string lastConnexion)
        {
            return new User
            {
                Mail = mail,
                Password = password,
                LastConnexion = lastConnexion
                
            };
        }

        public IUser CreateAdminUser(string mail, string password, string lastConnexion, bool admin)
        {
            return new User
            {
                Mail = mail,
                Password = password,
                LastConnexion = lastConnexion,
                Admin = admin
            };
        }
    }
}