using Application.Services.Cars;
using Application.Services.Cars.Dto;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/cars")]
    public class CarController : ControllerBase
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        //ActionResult renvoie un code http et entre <> c'est les données qui vont être renvoyées
        [HttpGet]
        public ActionResult<OutputDtoQueryCar> QueryCar()
        {
            //Renvoie les données avec un code 200 -> Tout s'est bien passé
            return Ok(_carService.Query());
        }

        //TODO : Vérifier le passage de l'id par la route sans utiliser inputDto
        [HttpGet]
        [Route("{id}")]
        public ActionResult<OutputDtoGetByIdCar> GetByIdCar(int id)
        {
            var inputDtoGetByIdCar = new InputDtoGetByIdCar
            {
                id = id
            };
            
            OutputDtoGetByIdCar car = _carService.GetById(inputDtoGetByIdCar);
            return _carService!= null ? (ActionResult<OutputDtoGetByIdCar>) Ok(car) : NotFound();
        }

        //[FromBody] le user qu'on enverra, résidera dans le corps de la requête
        [HttpPost]
        public ActionResult<OutputDtoAddCar> CreateCar([FromBody]InputDtoAddCar inputDtoAddCar)
        {
            return Ok(_carService.Create(inputDtoAddCar));
        }

        [HttpDelete]
        [Route("{id}")]
        public ActionResult DeleteByIdCar(int id)
        {
            var inputDtoDeleteByIdCar = new InputDtoDeleteByIdCar()
            {
                id = id
            };
            
            if (_carService.DeleteById(inputDtoDeleteByIdCar))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPut]
        [Route("{id}")]
        public ActionResult UpdateCar(int id,[FromBody]InputDtoUpdateCar inputDtoUpdateCar)
        {
            if (_carService.Update(id, inputDtoUpdateCar))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpGet]
        [Route("{idUser}/user")]
        public ActionResult<OutputDtoGetByIdUserCar> GetByIdUserCar(int idUser)
        {
            var inputDtoGetByIdUserCar = new InputDtoGetByIdUserCar
            {
                IdUser = idUser
            };

            OutputDtoGetByIdUserCar car = _carService.GetByIdUserCar(inputDtoGetByIdUserCar);

            return _carService != null ? (ActionResult<OutputDtoGetByIdUserCar>) Ok(car) : NotFound();
        }
        
    }
}