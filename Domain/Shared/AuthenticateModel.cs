using System.ComponentModel.DataAnnotations;

namespace Domain.Shared
{
    public class AuthenticateModel
    {
        [Required] public string Mail { get; set; }
        [Required] public string Password { get; set; }
    }
}