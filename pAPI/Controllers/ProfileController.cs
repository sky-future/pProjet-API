using Application.Services.Profile;
using Application.Services.Profile.DTO;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/profile")]
    public class ProfileController : ControllerBase
    {
        
         private readonly IProfileService _profileService;

        public ProfileController(IProfileService profileService)
        {
            _profileService = profileService;
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
        public ActionResult UpdateAddress(int id,[FromBody]InputDtoUpdateProfile inputDtoUpdateAddress)
        {
            if (_profileService.Update(id, inputDtoUpdateAddress))
            {
                return Ok();
            }

            return NotFound();
        }
        
    }
}