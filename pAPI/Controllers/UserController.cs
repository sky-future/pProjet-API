using System;
using System.Diagnostics;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Domain.Shared;
using Domain.Users;
using Infrastructure.SqlServer.Users;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;
        private UserRepository _userRepository = new UserRepository();

        public UserController(IUserService userService)
        {
            Console.WriteLine("Constructeur controller");
            _userService = userService;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryUser> Query()
        {
            Console.Write("Controller query");
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(_userService.Query());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<User> GetById(int id)
        {
            IUser user = (User) _userRepository.Get(id);
            return _userService!= null ? (ActionResult<User>) Ok(user) : NotFound();
        }

        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddUser> Create([FromBody]InputDtoAddUser inputDtoAddUser)
        {
            return Ok(_userService.Create(inputDtoAddUser));
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
        public ActionResult Put(int id,[FromBody]InputDtoUpdateUser inputDtoUpdateUser)
        {
            if (_userService.Update(id, inputDtoUpdateUser))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] AuthenticateModel model)
        {
            Debug.Assert(_userRepository != null, nameof(_userRepository) + " != null");
            var user = _userRepository.Authenticate(model.Mail, model.Password);

            if (user == null)
            {
                return BadRequest(new {message = "L'adresse e-mail ou le mot de passe est incorrect."});
            }
            
            Console.WriteLine(user.Id);
            Console.WriteLine(user.Mail);

            return Ok(user);
        }
    }
}