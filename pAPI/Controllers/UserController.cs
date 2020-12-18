using System;
using System.Diagnostics;
using System.IO;
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
            var query = _userService.Query();

            if (query == null)
            {
                return BadRequest(new {message = "Aucune données n'a été trouvées."});
            }
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(query);
        }

        //TODO : Vérifier le passage de l'id par la route sans utiliser inputDto
        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetByIdUser> GetByIdUser(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoGetById = new InputDtoGetByIdUser
            {
                id = id
            };
            
            OutputDtoGetByIdUser user = _userService.GetById(inputDtoGetById);
            return user != null ? (ActionResult<OutputDtoGetByIdUser>) Ok(user) : BadRequest(new {message = "Aucun n'utilisateur ne correspond à cet id."});
        }

        //TODO Vérifier intégralité données
        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddUser> CreateUser([FromBody]InputDtoAddUser inputDtoAddUser)
        {
            OutputDtoAddUser user = _userService.Create(inputDtoAddUser);

            return user != null ? (ActionResult<OutputDtoAddUser>) Ok(user) : BadRequest();
        }

        [HttpPost]
        [Route("admin")]
        public ActionResult CreateAdminUser([FromBody] InputDtoAddAdminUser inputDtoAddAdminUser)
        {
            var inputDtoGetById = new InputDtoGetByIdUser
            {
                id = inputDtoAddAdminUser.Id
            };

            var userList = _userService.Query();

            foreach (var user in userList)
            {
                if (user.mail == inputDtoAddAdminUser.Mail)
                {
                    return BadRequest(new {message = "Cet utilisateur est déjà administrateur !"});
                }
            }
            
            OutputDtoGetByIdUser userAdmin = _userService.GetById(inputDtoGetById);

            if (userAdmin.admin)
            {
                if (inputDtoAddAdminUser.Admin)
                {
                    _userService.CreateAdminUser(inputDtoAddAdminUser);
                    return Ok(new {message = "Un nouvel administrateur a été crée."});
                }
                _userService.CreateAdminUser(inputDtoAddAdminUser);
                return Ok(new {message = "Un nouveau utilisateur a été crée."});
            }
            
            return BadRequest(new {message = "Vous n'avez pas les autorisations pour ajouter un administrateur !"});

        }
        
        
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdUser(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
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
        
        //TODO Intégralité données
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

        //TODO Intégralité données
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

            // if (profile == null)
            // {
            //     
            //     return BadRequest(user);
            // }

            // user.profile = profile.Id;
            return Ok(user);
        }
        
         //TODO Intégralité données
         [HttpPatch]
         [Route("pwd")]
          public ActionResult UpdatePassword([FromBody] InputDTOUpdateUserPassword password)
          {
              if (_userService.UpdatePassword(password))
              {
                  
                  return Ok(new {message = "Le mot de passe à bien été changé !"});
              }
              
              return BadRequest(new {message = "Le mot de passe ne correspond pas !"});;
          }

          [HttpPatch]
          [Route("lastConnexion/{id}")]
          public ActionResult UpdateLastConnexion(int id)
          {
              if (_userService.UpdateLastConnexion(id))
              {
                  return Ok(new {message = "Last connexion est à jour !"});
              }

              return BadRequest(new {message = "Il y a eu un problème avec lastconnexion"});
          }
    }
}