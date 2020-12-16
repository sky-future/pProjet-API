using System;
using System.Diagnostics;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Domain.Profile;
using Infrastructure.SqlServer.Profile;
using Microsoft.AspNetCore.Mvc;

//TODO Renvoyer un message personnalisé pour chaque API en cas d'erreur !!

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/users")]
    public class UserController : ControllerBase
    {
        private readonly IUserService _userService;

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryUser> QueryUser()
        {
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(_userService.Query());
        }

        //TODO : Vérifier le passage de l'id par la route sans utiliser inputDto
        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetByIdUser> GetByIdUser(int id)
        {
            var inputDtoGetById = new InputDtoGetByIdUser
            {
                id = id
            };
            
            OutputDtoGetByIdUser user = _userService.GetById(inputDtoGetById);
            return _userService!= null ? (ActionResult<OutputDtoGetByIdUser>) Ok(user) : NotFound();
        }

        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddUser> CreateUser([FromBody]InputDtoAddUser inputDtoAddUser)
        {
            OutputDtoAddUser user = _userService.Create(inputDtoAddUser);

            return user != null ? (ActionResult<OutputDtoAddUser>) Ok(user) : BadRequest();
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdUser(int id)
        {
            var inputDtoDeleteById = new InputDtoDeleteByIdUser()
            {
                id = id
            };
            
            if (_userService.DeleteById(inputDtoDeleteById))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateUser(int id,[FromBody]InputDtoUpdateUser inputDtoUpdateUser)
        {
            if (_userService.Update(id, inputDtoUpdateUser))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost("authenticate")]
        public IActionResult AuthenticateUser([FromBody] InputDtoAuthenticate inputDtoAuthenticate)
        {
            Debug.Assert(_userService != null, nameof(_userService) + " != null");
            var user = _userService.Authenticate(inputDtoAuthenticate);
            
            if (user == null)
            {
                return NotFound(new {message = "L'adresse e-mail ou le mot de passe est incorrect."});
            }

            //Vérifie qui profile soit lié au user
            ProfileRepository pr = new ProfileRepository();
            IProfile profile = pr.GetByIdUser(user.id);

            if (profile == null) return BadRequest(user);

            user.profile = profile.Id;
            return Ok(user);
        }
        
         
         [HttpPatch]
         [Route("pwd")]
          public ActionResult UpdatePassword([FromBody] InputDTOUpdateUserPassword password)
          {
              if (_userService.UpdatePassword(password))
              {
                  return Ok(new {message = "Le mot de passe a été changé"});
              }
              
              return BadRequest(new {message = "Le mot de passe ne correspond pas !"});;
          }
         
         
         
    }
}