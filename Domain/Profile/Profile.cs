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

        public Profile(int id,string lastname, string firstname, string matricule, string telephone, string descript)
        {
            Id = id;
            Lastname = lastname;
            Firstname = firstname;
            Matricule = matricule;
            Telephone = telephone;
            Descript = descript;
        }

        public Profile()
        {
        }

       
    }
}