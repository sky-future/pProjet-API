using System;
using Application.Repositories;
using Application.Services.Address;
using Application.Services.Address.Dto;
using Application.Services.AddressUser;
using Application.Services.AddressUser.Dto;
using Microsoft.AspNetCore.Mvc;
namespace pAPI.Controllers
{
    //TODO regarder code http pour gérer dans les méthodes
    //TODO Tests unitaires
    
    [ApiController]
    [Route("api/address")]
    public class AddressController : ControllerBase
    {
        private readonly IAddressService _addressService;
        private readonly IAddressUserService _addressUserService;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressUserRepository _addressUserRepository;
        private readonly ICarRepository _carRepository;

        public AddressController(IAddressService addressService, IAddressUserService addressUserService, IUserRepository userRepository, IAddressRepository addressRepository, IAddressUserRepository addressUserRepository, ICarRepository carRepository)
        {
            _addressService= addressService;
            _addressUserService = addressUserService;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _addressUserRepository = addressUserRepository;
            _carRepository = carRepository;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryAddress> QueryAddress()
        {

            var query = _addressService.Query();

            if (query == null)
                return BadRequest(new {message = "Aucune données n'a été trouvées."});
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(query);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetByIdAddress> GetByIdAddress(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            var inputDtoGetByIdAddress = new InputDtoGetByIdAddress
            {
                id = id
            };
            
            OutputDtoGetByIdAddress address = _addressService.GetById(inputDtoGetByIdAddress);
            return address != null ? (ActionResult<OutputDtoGetByIdAddress>) Ok(address) : BadRequest(new {message = "Aucune adresse ne correspond à l'id envoyé."});
        }

        //TODO Vérifier l'intégralité des données envoyées pour le post
        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddAddress> CreateAddress([FromBody]InputDtoAddAddress inputDtoAddAddress)
        {
            var address = _addressService.Create(inputDtoAddAddress);
            if (address == null)
            {
                BadRequest(new {message = "L'adresse n'a pas été créée."});
            }
            return Ok(address);
        }
        
        //TODO Vérifier l'intégralité des données envoyées pour le post
        //Post pour l'enregistrement d'une voiture et d'une adresse d'un utilisateur
        [HttpPost]
        [Route("addressCar")]
        public ActionResult<OutputDTOAddAddressAndCar> PostByUserId([FromBody]InputDTOAddAddressAndCar inputDtoAddAddressAndCar)
        {
            InputDtoGetByIdUserAddressUser inputDtoGetByIdUserAddressUser = new InputDtoGetByIdUserAddressUser
            {
                IdUser = inputDtoAddAddressAndCar.IdUser
            };
            if (_addressUserService.GetByIdUserAddressUser(inputDtoGetByIdUserAddressUser) != null)
                return BadRequest(new {message = "Vous êtes déjà enregistré en tant que covoitureur"});

            var addresscar = _addressService.CreateAddressAndCarByid(inputDtoAddAddressAndCar);

            if (addresscar == null)
            {
                BadRequest(new {message = "L'adresse et la voiture n'ont pas été créée"});
            }
            return Ok(addresscar);
            
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdAddress(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoDeleteByIdAddress = new InputDtoDeleteByIdAddress()
            {
                id = id
            };
            
            if (_addressRepository.GetById(inputDtoDeleteByIdAddress.id) == null)
            {
                return BadRequest(new
                    {message = "L'adresse n'existe pas."});
            }

            //TODO Ne gère pas le rajout des éléments supprimés dans le cas ou il ne finit pas la condition
            if (
                _carRepository.DeleteById(_carRepository
                    .GetByIdUserCar(_addressUserRepository
                        .GetByAddress(_addressRepository
                            .GetById(inputDtoDeleteByIdAddress.id)).User.Id).Id) 
                && _addressUserRepository.DeleteAddress(inputDtoDeleteByIdAddress.id) 
                && _addressService.DeleteById(inputDtoDeleteByIdAddress)
                )
            {
                return Ok(new {message = "L'adresse a bien été suprimée"});
            }

            return BadRequest(new {message = "L'adresse n'a pas été supprimée"});
        }

        //TODO Vérifier l'intégralité des données envoyées pour l'update
        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateAddress(int id,[FromBody]InputDtoUpdateAddress inputDtoUpdateAddress)
        {
            if (_addressService.Update(id, inputDtoUpdateAddress))
            {
                return Ok();
            }

            return BadRequest(new {message = "L'adresse n'a pas été modifiée"});
        }

        [HttpGet]
        [Route("{idAddress}/users")]
        public ActionResult<OutputDtoGetByIdAddressAddressUser> GetByIdAdressUser(int idAddress)
        {
            if (idAddress < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoGetByIdAddressAddressUser = new InputDtoGetByIdAddressAdressUser
            {
                IdAddress = idAddress
            };
            
            if (_addressRepository.GetById(inputDtoGetByIdAddressAddressUser.IdAddress) == null)
            {
                return BadRequest(new
                    {message = "L'adresse n'existe pas."});
            }

            var user = _addressUserService.GetByIdAddressAddressUser(inputDtoGetByIdAddressAddressUser);

            if (user == null)
            {
                BadRequest(new {message = "Aucun n'utilisateur ne correspond à l'adresse donnée"});
            }

            return Ok(user);
        }
        
        [HttpGet]
        [Route("{idUser}/address")]
        public ActionResult<OutputDtoGetByIdUserAddressUser> GetByIdUser(int idUser)
        {
            if (idUser < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoGetByIdUserAddressUser = new InputDtoGetByIdUserAddressUser
            {
                IdUser = idUser
            };
            
            if (_userRepository.GetById(inputDtoGetByIdUserAddressUser.IdUser) == null)
            {
                return BadRequest(new
                    {message = "L'utilisateur n'existe pas."});
            }

            var address = _addressUserService.GetByIdUserAddressUser(inputDtoGetByIdUserAddressUser);

            if (address == null)
            {
                return BadRequest(new {message = "Aucune adresse ne correspond à l'utilisateur donné"});
            }

            return Ok(address);
        }

        [HttpPost]
        [Route("{idUser}/{idAddress}/users")]
        public ActionResult<OutputDtoCreateAddressUser> CreateAddressUser(int idUser, int idAddress)
        {
            if (idUser < 0 || idAddress < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoIdUserCreateAddressUser = new InputDtoIdUserCreateAddressUser
            {
                IdUser = idUser
            };
            var inputDtoIdAddressCreateAddressUser = new InputDtoIdAddressCreateAddressUser
            {
                IdAddress = idAddress
            };

            if (_userRepository.GetById(inputDtoIdUserCreateAddressUser.IdUser) == null)
            {
                return BadRequest(new
                    {message = "L'utilisateur n'existe pas."});
            }
            
            if (_addressRepository.GetById(inputDtoIdAddressCreateAddressUser.IdAddress) == null)
            {
                return BadRequest(new
                    {message = "L'adresse n'existe pas."});
            }

            var queryAddressUser = _addressUserRepository.Query();

            foreach (var query in queryAddressUser)
            {
                Console.WriteLine(query);
                if (query.User.Id == idUser)
                {
                    return BadRequest(new {message = "L'utilisateur a déjà une adresse."});
                }
            }
            
            var addressuser = _addressUserService.CreateAddressUser(inputDtoIdUserCreateAddressUser,
                inputDtoIdAddressCreateAddressUser);

            if (addressuser == null)
                return BadRequest(new
                    {message = "La correspondance adresse user n'a pas été créée dans la base de donnée"});
            
            return Ok(addressuser);
        }
    }
}