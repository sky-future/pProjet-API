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

        public UserController(IUserService userService)
        {
            _userService = userService;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryUser> Query()
        {
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(_userService.Query());
        }

        //TODO : Vérifier le passage de l'id par la route sans utiliser inputDto
        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetById> GetById(int id)
        {
            var inputDtoGetById = new InputDtoGetById
            {
                id = id
            };
            
            OutputDtoGetById user = _userService.GetById(inputDtoGetById);
            return _userService!= null ? (ActionResult<OutputDtoGetById>) Ok(user) : NotFound();
        }

        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddUser> Create([FromBody]InputDtoAddUser inputDtoAddUser)
        {
            return Ok(_userService.Create(inputDtoAddUser));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteById(int id)
        {
            var inputDtoDeleteById = new InputDtoDeleteById()
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
        public ActionResult Update(int id,[FromBody]InputDtoUpdateUser inputDtoUpdateUser)
        {
            if (_userService.Update(id, inputDtoUpdateUser))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPost("authenticate")]
        public IActionResult Authenticate([FromBody] InputDtoAuthenticate inputDtoAuthenticate)
        {
            Debug.Assert(_userService != null, nameof(_userService) + " != null");
            var user = _userService.Authenticate(inputDtoAuthenticate);

            if (user == null)
            {
                return BadRequest(new {message = "L'adresse e-mail ou le mot de passe est incorrect."});
            }

            return Ok(user);
        }
    }
}