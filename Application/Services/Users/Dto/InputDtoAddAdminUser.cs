using System;

namespace Application.Services.Users.Dto
{
    public class InputDtoAddAdminUser
    {
        public int Id { get; set; }
        public string Mail { get; set; }
        public string Password { get; set; }
        public string LastConnexion {get; set; }
        public Boolean Admin { get; set; }
    }
}