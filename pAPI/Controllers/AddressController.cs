using System;
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

        public AddressController(IAddressService addressService, IAddressUserService addressUserService)
        {
            _addressService= addressService;
            _addressUserService = addressUserService;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryAddress> QueryAddress()
        {
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(_addressService.Query());
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetByIdAddress> GetByIdAddress(int id)
        {
            var inputDtoGetByIdAddress = new InputDtoGetByIdAddress
            {
                id = id
            };
            
            OutputDtoGetByIdAddress address = _addressService.GetById(inputDtoGetByIdAddress);
            return _addressService!= null ? (ActionResult<OutputDtoGetByIdAddress>) Ok(address) : NotFound();
        }

        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddAddress> CreateAddress([FromBody]InputDtoAddAddress inputDtoAddAddress)
        {
            return Ok(_addressService.Create(inputDtoAddAddress));
        }
        
        //Post pour l'enregistrement d'une voiture et d'une adresse d'un utilisateur
        [HttpPost]
        [Route("{id}")]
        public ActionResult<OutputDTOAddAddressAndCar> PostByUserId(int id,[FromBody]InputDTOAddAddressAndCar inputDtoAddAddressAndCar)
        {
            return Ok(_addressService.CreateAddressAndCarByid(inputDtoAddAddressAndCar, id));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdAddress(int id)
        {
            var inputDtoDeleteByIdAddress = new InputDtoDeleteByIdAddress()
            {
                id = id
            };
            
            if (_addressService.DeleteById(inputDtoDeleteByIdAddress))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateAddress(int id,[FromBody]InputDtoUpdateAddress inputDtoUpdateAddress)
        {
            if (_addressService.Update(id, inputDtoUpdateAddress))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{idAddress}/users")]
        public ActionResult<OutputDtoGetByIdAddressAddressUser> GetByIdAdressUser(int idAddress)
        {
            var inputDtoGetByIdAddressAddressUser = new InputDtoGetByIdAddressAdressUser
            {
                IdAddress = idAddress
            };

            return Ok(_addressUserService.GetByIdAddressAddressUser(inputDtoGetByIdAddressAddressUser));
        }
        
        [HttpPost]
        [Route("{idUser}/{idAddress}/users")]
        public ActionResult<OutputDtoCreateAddressUser> CreateAddressUser(int idUser, int idAddress)
        {
            var inputDtoIdUserCreateAddressUser = new InputDtoIdUserCreateAddressUser
            {
                IdUser = idUser
            };
            var inputDtoIdAddressCreateAddressUser = new InputDtoIdAddressCreateAddressUser
            {
                IdAddress = idAddress
            };
            Console.WriteLine(idUser);
            return Ok(_addressUserService.CreateAddressUser(inputDtoIdUserCreateAddressUser,inputDtoIdAddressCreateAddressUser));
        }
    }
}