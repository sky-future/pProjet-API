using System;

namespace Domain.Users
{
    public class User : IUser
    {
        
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public DateTime LastConnexion { get; set; }
        public bool Admin { get; set; }

        public User(int id, string mail, string password, DateTime lastConnexion, bool admin)
        {
            Id = id;
            Mail = mail;
            Password = password;
            LastConnexion = lastConnexion;
            Admin = admin;
        }

        public User()
        {
        }
        
    }
}