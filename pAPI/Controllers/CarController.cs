using Application.Services.Cars;
using Application.Services.Cars.Dto;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;
        private readonly IUserService _userService;

        public CarController(ICarService carService, IUserService userService)
        {
            _carService = carService;
            _userService = userService;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryCar> QueryCar()
        {
            var query = _carService.Query();

            if (query == null)
            {
                return BadRequest(new {message = "Aucune données n'a été trouvées."});
            }
            
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(query);
        }

        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetByIdCar> GetByIdCar(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            var inputDtoGetByIdCar = new InputDtoGetByIdCar
            {
                id = id
            };
            
            OutputDtoGetByIdCar car = _carService.GetById(inputDtoGetByIdCar);

            if (car == null)
            {
                return BadRequest(new {message = "Aucune voiture ne correspond à l'id envoyé"});
            }

            return Ok(car);
        }

        //TODO Intégralité des données voiture
        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddCar> CreateCar([FromBody]InputDtoAddCar inputDtoAddCar)
        {
            return Ok(_carService.Create(inputDtoAddCar));
        }

        //TODO Rajouter la même condition que dans adresse
        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdCar(int id)
        {
            if (id < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoDeleteByIdCar = new InputDtoDeleteByIdCar()
            {
                id = id
            };
            
            var inputDtoGByIdCar = new InputDtoGetByIdCar
            {
                id = id
            };

            if (_carService.GetById(inputDtoGByIdCar) == null)
            {
                return BadRequest(new
                    {message = "La voiture n'existe pas."});
            }
            
            if (_carService.DeleteById(inputDtoDeleteByIdCar))
            {
                return Ok("Vous avez supprimée la voiture");
            }

            return NotFound();
        }

        //TODO Intégralité des données
        [HttpPut]
        [Route("{idUser}")]
        public ActionResult UpdateCar(int idUser,[FromBody]InputDtoUpdateCar inputDtoUpdateCar)
        {
            if (_carService.Update(idUser, inputDtoUpdateCar))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{idUser}/user")]
        public ActionResult<OutputDtoGetByIdUserCar> GetByIdUserCar(int idUser)
        {
            if (idUser < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoGetByIdUserCar = new InputDtoGetByIdUserCar
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

            OutputDtoGetByIdUserCar car = _carService.GetByIdUserCar(inputDtoGetByIdUserCar);

            if (car == null)
            {
                return BadRequest(new {message = "Aucune voiture n'a été trouvée pour cet utilisateur."});
            }

            return Ok(car);
        }
        
    }
}