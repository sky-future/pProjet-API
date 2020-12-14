namespace Domain.RequestCarpooling
{
    public class RequestCarpoolingProfile : IRequestCarpoolingProfile
    {
        public int IdUser { get; set; }
        public string Lastname { get; set; }
        public string Firstname { get; set; }
        public string Telephone { get; set; }
        public bool Confirmation { get; set; }

        public RequestCarpoolingProfile()
        {
            
        }

        public RequestCarpoolingProfile(int idUser, string lastname, string firstname, string telephone,
            bool confirmation)
        {
            IdUser = idUser;
            Lastname = lastname;
            Firstname = firstname;
            Telephone = telephone;
        }
    }
}