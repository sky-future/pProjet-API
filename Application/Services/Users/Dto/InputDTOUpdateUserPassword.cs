namespace Application.Services.Users.Dto
{
    public class InputDTOUpdateUserPassword
    {
        public int IdUser { get; set; }
        public string PasswordNew { get; set; }
        public string PasswordOld { get; set; }
    }
}