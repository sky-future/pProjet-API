using System.Collections.Generic;
using Domain.Users;
using Infrastructure.SqlServer.Users;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private IUserRepository _userRepository = new SqlServerUserRepository();

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<IEnumerable<IUser>> Query()
        {
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(_userRepository.Query());
        }

        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<IUser> Create([FromBody]User user)
        {
            return Ok(_userRepository.Create(user));
        }
    }
}