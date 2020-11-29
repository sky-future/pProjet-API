using Domain.Profile;

namespace Application.Services.Users.Dto
{
    public class OutputDtoAuthenticate
    {
        public int id { get; set; }
        public string mail { get; set; }
        public string password { get; set; }
        public string lastConnexion { get; set; }
        public bool admin { get; set; }
        public string token { get; set; }
        public int profile { get; set; }
    }
}