using System.Collections.Generic;
using Application.Services.Cars.Dto;

namespace Application.Services.Cars
{
    public interface ICarService
    {
        IEnumerable<OutputDtoQueryCar> Query();
        OutputDtoAddCar Create(InputDtoAddCar inputDtoAddCar);
        bool Update(int id, InputDtoUpdateCar inputDtoUpdateCar);
        OutputDtoGetByIdCar GetById(InputDtoGetByIdCar inputDtoGetByIdCar);

        bool DeleteById(InputDtoDeleteByIdCar inputDtoDeleteByIdCar);
    }
}