using System;
using Application.Services.Profile;
using Application.Services.Profile.DTO;
using Application.Services.UserProfile;
using Application.Services.UserProfile.Dto;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        
         private readonly IProfileService _profileService;
         private readonly IUserProfileService _userProfileService;

        public ProfileController(IProfileService profileService, IUserProfileService userProfileService)
        {
            _profileService = profileService;
            _userProfileService = userProfileService;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryProfile> QueryProfile()
        {
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(_profileService.Query());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetByIdProfile> GetByIdProfile(int id)
        {
            var inputDtoGetByIdProfile = new InputDtoGetByIdProfile()
            {
                Id = id
            };
            
            OutputDtoGetByIdProfile profile = _profileService.GetById(inputDtoGetByIdProfile);
            return _profileService!= null ? (ActionResult<OutputDtoGetByIdProfile>) Ok(profile) : NotFound();
        }
        
        [HttpGet]
        [Route("{idUser}/profile")]
        public ActionResult<OutputDtoGetByidUserProfile> GetByUserIdProfile(int idUser)
        {
            var inputDtoGetByidUserProfile = new InputDtoGetByidUserProfile()
            {
                IdUser = idUser
            };
            
            OutputDtoGetByidUserProfile profile = _profileService.GetByUserIdProfile(inputDtoGetByidUserProfile);
            return _profileService!= null ? (ActionResult<OutputDtoGetByidUserProfile>) Ok(profile) : NotFound();
        }

        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddProfile> CreateAddress([FromBody]InputDtoAddProfile inputDtoAddProfile)
        {
            return Ok(_profileService.Create(inputDtoAddProfile));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdAddress(int id)
        {
            var inputDtoDeleteByIdProfile = new InputDtoDeleteByIdProfile()
            {
                Id = id
            };
            
            if (_profileService.DeleteById(inputDtoDeleteByIdProfile))
            {
                return Ok();
            }

            return NotFound();
        }

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