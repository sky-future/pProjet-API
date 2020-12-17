using Application.Repositories;
using Application.Services.AddressUser;
using Application.Services.AddressUser.Dto;
using Application.Services.Cars;
using Application.Services.Cars.Dto;
using Application.Services.OfferCarpooling;
using Application.Services.OfferCarpooling.DTO;
using Application.Services.RequestCarpooling;
using Application.Services.RequestCarpooling.DTO;
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
        private readonly IRequestCarpoolingService _requestCarpoolingService;

        public OfferCarpoolingController(IOfferCarpoolingService offerCarpoolingService, IAddressUserService addressUserService, IAddressRepository addressRepository, IUserService userService, ICarService carService, IRequestCarpoolingService requestCarpoolingService)
        {
            _offerCarpoolingService = offerCarpoolingService;
            _addressUserService = addressUserService;
            _addressRepository = addressRepository;
            _userService = userService;
            _carService = carService;
            _requestCarpoolingService = requestCarpoolingService;
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
        [Route("{idUser}")]
        public ActionResult DeleteByIdOfferCarpooling(int idUser)
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
                return BadRequest(new {message = "L'utilisateur n'existe pas."});
            }
            
            var inputDtoDeleteByIdOfferCarpooling = new InputDtoDeleteById()
            {
                Id = idUser
            };

            var inputDtoAddOfferCarpooling = new InputDtoAddOfferCarpooling
            {
                IdUser = idUser
            };

            //Est ce qu'il a une offre ?
            if (_offerCarpoolingService.GetByIdUser(inputDtoAddOfferCarpooling) == null)
            {
                return BadRequest(new
                {
                    message =
                        "L'utilisateur n'est pas dans la liste des utilisateurs qui proposent leurs services de covoiturage."
                });
            }

            var inputDtoGetRequestByIdReceiver = new InputDtoGetRequestByIdReceiver
            {
                IdRequestReceiver = idUser
            };
            //Supprimer toutes les requêtes ou idUser est le sender
            var requestByIdReceiver = _requestCarpoolingService.GetRequestByIdReceiver(inputDtoGetRequestByIdReceiver);

            if (requestByIdReceiver == null)
            {
                if (_offerCarpoolingService.DeleteById(inputDtoDeleteByIdOfferCarpooling))
                {
                    return Ok("Vous êtes bel et bien désinscrit de la liste de co-voiturage.");
                }

                return BadRequest("Votre demande n'a pas pu être effectuée.");
            }

            foreach (var request in requestByIdReceiver)
            {
                var input = new InputDtoGetRequestByIdReceiver
                {
                    IdRequestReceiver = request.IdRequestReceiver
                };
                var isDone = _requestCarpoolingService.DeleteAllByIdReceiver(input);

                if (!isDone)
                {
                    return BadRequest(new {message = "Il y a eu un problème lors de la suppresion des requêtes."});
                }
            }

            var offerInDb = _offerCarpoolingService.GetByIdUser(inputDtoAddOfferCarpooling);
            var inputDtoDeleteById = new InputDtoDeleteById
            {
                Id = offerInDb.Id
            };
            var delInDb = _offerCarpoolingService.DeleteById(inputDtoDeleteById);

            return Ok("Votre demande a bien été effectuée.");
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