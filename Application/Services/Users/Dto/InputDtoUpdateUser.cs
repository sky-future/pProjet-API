namespace Application.Services.Users.Dto
{
    public class InputDtoUpdateUser
    {
        public string mail { get; set; }
        public string password { get; set; }
        public string lastConnexion { get; set; }
        public bool admin { get; set; }
    }
}