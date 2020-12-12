using Application.Services.OfferCarpooling;
using Application.Services.OfferCarpooling.DTO;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/offerCarpooling")]
    public class OfferCarpoolingController : ControllerBase
    {
        private readonly IOfferCarpoolingService _offerCarpoolingService;

        public OfferCarpoolingController(IOfferCarpoolingService offerCarpoolingService)
        {
            _offerCarpoolingService = offerCarpoolingService;
        }
        
        [HttpGet]
        public ActionResult<OutputDtoQueryCarpooling> QueryOfferCarpooling()
        {
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(_offerCarpoolingService.Query());
        }
        
        [HttpPost]
        public ActionResult<OutputDtoAddOfferCarpooling> CreateOfferCarpooling([FromBody]InputDtoAddOfferCarpooling inputDtoAddOfferCarpooling)
        {
            return Ok(_offerCarpoolingService.Create(inputDtoAddOfferCarpooling));
        }
        
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdOfferCarpooling(int id)
        {
            var inputDtoDeleteByIdOfferCarpooling = new InputDtoDeleteById()
            {
                Id = id
            };
            
            if (_offerCarpoolingService.DeleteById(inputDtoDeleteByIdOfferCarpooling))
            {
                return Ok();
            }

            return NotFound();
        }
    }
}