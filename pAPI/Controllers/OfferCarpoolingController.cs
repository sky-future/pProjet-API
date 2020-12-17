using Application.Repositories;
using Application.Services.AddressUser;
using Application.Services.AddressUser.Dto;
using Application.Services.Cars;
using Application.Services.Cars.Dto;
using Application.Services.OfferCarpooling;
using Application.Services.OfferCarpooling.DTO;
using Application.Services.Users;
using Application.Services.Users.Dto;
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
        private readonly IUserService _userService;
        private readonly ICarService _carService;

        public OfferCarpoolingController(IOfferCarpoolingService offerCarpoolingService, IAddressUserService addressUserService, IAddressRepository addressRepository, IUserService userService, ICarService carService)
        {
            _offerCarpoolingService = offerCarpoolingService;
            _addressUserService = addressUserService;
            _addressRepository = addressRepository;
            _userService = userService;
            _carService = carService;
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
        [Route("{idUser}")]
        public ActionResult<OutputDtoAddOfferCarpooling> CreateOfferCarpooling(int idUser)
        {
            if (idUser < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoGetByIdUser = new InputDtoGetByIdUser
            {
                id = idUser
            };
            
            if (_userService.GetById(inputDtoGetByIdUser) == null)
            {
                return BadRequest(new
                    {message = "L'utilisateur n'existe pas."});
            }
            
            var inputDtoAddOfferCarpooling = new InputDtoAddOfferCarpooling
            {
                IdUser = idUser
            };

            var offerInDb =_offerCarpoolingService.GetByIdUser(inputDtoAddOfferCarpooling);

            if (offerInDb != null)
            {
                return BadRequest(new
                    {message = "Vous êtes déjà dans la liste des personnes qui proposent leurs co-voiturage."});
            }

            var inputDtoGetByIdUserCar = new InputDtoGetByIdUserCar
            {
                IdUser = idUser
            };

            var carUser = _carService.GetByIdUserCar(inputDtoGetByIdUserCar);

            if (carUser.PlaceNb <= 0)
            {
                return BadRequest(new {message = "Vous n'avez pas assez de place dans votre voiture"});
            }
            
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