using System;

namespace Domain.Users
{
    public class   User : IUser
    {
        
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string LastConnexion { get; set; }
        public bool Admin { get; set; }
        public string Token { get; set; }

        public User(int id, string mail, string password, string lastConnexion, bool admin)
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

        protected bool Equals(User other)
        {
            return Id == other.Id && Mail == other.Mail && Password == other.Password && LastConnexion == other.LastConnexion && Admin == other.Admin && Token == other.Token;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((User) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Mail, Password, LastConnexion, Admin, Token);
        }
    }
}