using System;
using Application.Services.AddressUser;
using Application.Services.AddressUser.Dto;
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
        private readonly IAddressUserService _addressUserService;

        public OfferCarpoolingController(IOfferCarpoolingService offerCarpoolingService, IAddressUserService addressUserService)
        {
            _offerCarpoolingService = offerCarpoolingService;
            _addressUserService = addressUserService;
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
        
        [HttpGet]
        [Route("list")]
        public ActionResult<OutputDtoGetAddressListForCarpooling> GetAddressListForCarpooling()
        {
            return Ok(_addressUserService.GetAddressListForCarpooling());
        }
    }
}