namespace Domain.Users
{
    public interface IUserFactory
    {
        IUser CreateSimpleUser(string mail, string password, string lastConnexion);
        IUser CreateAdminUser(string mail, string password, string lastConnexion, bool admin);
        
    }
}