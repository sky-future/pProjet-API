using Application.Repositories;
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
        private readonly IAddressRepository _addressRepository;

        public OfferCarpoolingController(IOfferCarpoolingService offerCarpoolingService, IAddressUserService addressUserService, IAddressRepository addressRepository)
        {
            _offerCarpoolingService = offerCarpoolingService;
            _addressUserService = addressUserService;
            _addressRepository = addressRepository;
        }
        
        [HttpGet]
        public ActionResult<OutputDtoQueryCarpooling> QueryOfferCarpooling()
        {
            var query = _offerCarpoolingService.Query();

            if (query == null)
            {
                return BadRequest(new {message = "Aucune données n'a été trouvées."});
            }
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(query);
        }
        
        //TODO intégralité des données
        [HttpPost]
        public ActionResult<OutputDtoAddOfferCarpooling> CreateOfferCarpooling([FromBody]InputDtoAddOfferCarpooling inputDtoAddOfferCarpooling)
        {
            return Ok(_offerCarpoolingService.Create(inputDtoAddOfferCarpooling));
        }
        
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdOfferCarpooling(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
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
            var get = _addressUserService.GetAddressListForCarpooling();

            if (get == null)
            {
                return BadRequest(new {message = "Aucune liste n'a été trouvée."});
            }
            
            return Ok(get);
        }

        [HttpGet]
        [Route("{idAddress}/info")]
        public ActionResult<OutputDtoInfoModal> GetInfoModal(int idAddress)
        {
            if (idAddress < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            InputDtoIdAddress inputDtoIdAddress = new InputDtoIdAddress
            {
                IdAddress = idAddress
            };

            if (_addressRepository.GetById(idAddress) == null)
            {
                return BadRequest(new {message = "Cette adresse n'existe pas."});
            }

            var infoModal = _offerCarpoolingService.GetInfoModal(inputDtoIdAddress);

            if (infoModal == null)
            {
                return BadRequest(new{message = "Toutes les informations n'ont pas été trouvées"});
            }
            
            return Ok(infoModal);
        }
    }
}