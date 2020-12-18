using System;

namespace Domain.Profile
{
    public class Profile : IProfile
    {
        public int Id { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Matricule { get; set; }
        public string Telephone { get; set; }
        public string Descript { get; set; }
        public int IdUser { get; set; }

        public Profile(int id,string lastname, string firstname, string matricule, string telephone, string descript, int idUser)
        {
            Id = id;
            Lastname = lastname;
            Firstname = firstname;
            Matricule = matricule;
            Telephone = telephone;
            Descript = descript;
            IdUser = idUser;
        }

        public Profile()
        {
        }

        protected bool Equals(Profile other)
        {
            return Id == other.Id && Lastname == other.Lastname && Firstname == other.Firstname && Matricule == other.Matricule && Telephone == other.Telephone && Descript == other.Descript && IdUser == other.IdUser;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Profile) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Lastname, Firstname, Matricule, Telephone, Descript, IdUser);
        }
    }
}