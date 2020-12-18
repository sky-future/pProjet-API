using System;
using Application.Services.Profile;
using Application.Services.Profile.DTO;
using Application.Services.UserProfile;
using Application.Services.UserProfile.Dto;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Microsoft.AspNetCore.Mvc;


namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        
         private readonly IProfileService _profileService;
         private readonly IUserProfileService _userProfileService;
         private readonly IUserService _userService;

        public ProfileController(IProfileService profileService, IUserProfileService userProfileService, IUserService userService)
        {
            _profileService = profileService;
            _userProfileService = userProfileService;
            _userService = userService;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryProfile> QueryProfile()
        {
            var query = _profileService.Query();

            if (query == null)
            {
                return BadRequest(new {message = "Aucune données n'a été trouvées."});

            }
            
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(query);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetByIdProfile> GetByIdProfile(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoGetByIdProfile = new InputDtoGetByIdProfile()
            {
                Id = id
            };
            
            OutputDtoGetByIdProfile profile = _profileService.GetById(inputDtoGetByIdProfile);
            return profile!= null ? (ActionResult<OutputDtoGetByIdProfile>) Ok(profile) : BadRequest(new {message = "Le profile n'existe pas."});
        }
        
        [HttpGet]
        [Route("{idUser}/profile")]
        public ActionResult<OutputDtoGetByidUserProfile> GetByUserIdProfile(int idUser)
        {
            if (idUser < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoGetByidUserProfile = new InputDtoGetByidUserProfile()
            {
                IdUser = idUser
            };
            
            var inputDtoGetByIdUser = new InputDtoGetByIdUser
            {
                id = idUser
            };
            
            if (_userService.GetById(inputDtoGetByIdUser) == null)
            {
                return BadRequest(new
                    {message = "L'utilisateur n'existe pas."});
            }
            
            OutputDtoGetByidUserProfile profile = _profileService.GetByUserIdProfile(inputDtoGetByidUserProfile);
            return profile != null ? (ActionResult<OutputDtoGetByidUserProfile>) Ok(profile) : BadRequest(new {message = "Aucun profile n'a été trouvé pour cet utilisateur."});
        }

        //TODO vérifier intégralité
        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddProfile> CreateProfile([FromBody]InputDtoAddProfile inputDtoAddProfile)
        {
            var queryProfile = _profileService.Query();

            foreach (var query in queryProfile)
            {
                if (query.Matricule == inputDtoAddProfile.Matricule)
                    return BadRequest(new {message ="Ce matricule existe déjà !"});
            }
            
            var profile = _profileService.Create(inputDtoAddProfile);

            if (profile == null)
            {
                return BadRequest(new {message = "Le profile n'a pas été créé."});
            }
            
            return Ok(profile);
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdProfile(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoDeleteByIdProfile = new InputDtoDeleteByIdProfile()
            {
                Id = id
            };
            
            if (_profileService.DeleteById(inputDtoDeleteByIdProfile))
            {
                return Ok("Vous avez supprimé le profile.");
            }

            return NotFound();
        }

        //TODO Vérifier intégralité
        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateProfile(int id,[FromBody]InputDtoUpdateProfile inputDtoUpdateProfile)
        {
            Console.WriteLine(inputDtoUpdateProfile);

            var inputDtoUpdateByIdProfile = new InputDtoUpdateByIdProfile()
            {
                Id = id
            };
            
            
            if (_profileService.Update(inputDtoUpdateByIdProfile, inputDtoUpdateProfile))
            {
                return Ok();
            }

            return NotFound();
        }

        //TODO Vérifier intégralité
        [HttpPost]
        [Route("{idUser}/users")]
        public ActionResult<OutputDtoCreateUserProfile> CreateUserProfile(int idUser, [FromBody]InputDtoProfileCreateUserProfile inputDtoProfileCreateUserProfile)
        {
            Console.WriteLine(idUser);
            var inputDtoIdUserCreateUserProfile = new InputDtoIdUserCreateUserProfile()
            {
                IdUser = idUser
            };
            return Ok(_userProfileService.CreateUserProfile(inputDtoIdUserCreateUserProfile,inputDtoProfileCreateUserProfile));
        }
        
        
    }
}