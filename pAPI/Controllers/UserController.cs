using System.Collections.Generic;
using System.Linq;
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
            return Ok(_userRepository.Query().Cast<User>());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<User> GetById(int id)
        {
            IUser user = (User) _userRepository.Get(id);
            return _userRepository != null ? (ActionResult<User>) Ok(user) : NotFound();
        }

        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<IUser> Create([FromBody]User user)
        {
            return Ok(_userRepository.Create(user));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult Delete(int id)
        {
            if (_userRepository.Delete(id))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult Put(int id,[FromBody]User user)
        {
            if (_userRepository.Update(id, user))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}