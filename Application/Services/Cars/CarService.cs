using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Cars.Dto;
using Domain.Cars;

namespace Application.Services.Cars
{
    public class CarService : ICarService
    {
        private readonly ICarRepository _carRepository;
        private readonly ICarFactory _carFactory = new CarFactory();
        
        public CarService(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }
        public IEnumerable<OutputDtoQueryCar> Query()
        {
            return _carRepository
                .Query()
                .Select(car => new OutputDtoQueryCar
                {
                    id =  car.Id,
                    immatriculation = car.Immatriculation,
                    idUser = car.IdUser,
                    placeNb = car.PlaceNb
                });
        }

        public OutputDtoAddCar Create(InputDtoAddCar inputDtoAddCar)
        {
            var carFromDto = _carFactory.createCar(inputDtoAddCar.immatriculation, inputDtoAddCar.idUser,
                inputDtoAddCar.placeNb);

            var carInDb = _carRepository.Create(carFromDto);
            
            return new OutputDtoAddCar
            {
                id =  carInDb.Id,
                immatriculation = carInDb.Immatriculation,
                idUser = carInDb.IdUser,
                placeNb = carInDb.PlaceNb
            };
        }

        public bool Update(int idUser, InputDtoUpdateCar inputDtoUpdateCar)
        {
            var carFromDto = _carFactory.createCar(inputDtoUpdateCar.immatriculation, inputDtoUpdateCar.idUser,
                inputDtoUpdateCar.placeNb);

            var id = _carRepository.GetByIdUserCar(idUser).Id;
            
            return _carRepository.Update(id, carFromDto);
        }

        public OutputDtoGetByIdCar GetById(InputDtoGetByIdCar inputDtoGetByIdCar)
        {
            var carInDb = _carRepository.GetById(inputDtoGetByIdCar.id);
            
            return new OutputDtoGetByIdCar
            {
                id =  carInDb.Id,
                immatriculation = carInDb.Immatriculation,
                idUser = carInDb.IdUser,
                placeNb = carInDb.PlaceNb
            };
        }

        public bool DeleteById(InputDtoDeleteByIdCar inputDtoDeleteByIdCar)
        {
            return _carRepository.DeleteById(inputDtoDeleteByIdCar.id);
        }

        public OutputDtoGetByIdUserCar GetByIdUserCar(InputDtoGetByIdUserCar inputDtoGetByIdUserCar)
        {
            var carInDb = _carRepository.GetByIdUserCar(inputDtoGetByIdUserCar.IdUser);

            return new OutputDtoGetByIdUserCar
            {
                Id = carInDb.Id,
                Immatriculation = carInDb.Immatriculation,
                IdUser = carInDb.IdUser,
                PlaceNb = carInDb.PlaceNb
            };
        }
    }
}