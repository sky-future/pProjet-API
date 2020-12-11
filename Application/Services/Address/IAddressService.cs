using System.Collections.Generic;
using Application.Services.Address.Dto;

namespace Application.Services.Address
{
    public interface IAddressService
    {
        IEnumerable<OutputDtoQueryAddress> Query();
        OutputDtoAddAddress Create(InputDtoAddAddress inputDtoAddAddress);
        OutputDTOAddAddressAndCar CreateAddressAndCarByid(InputDTOAddAddressAndCar inputDtoAddAddressAndCar);
        bool Update(int id, InputDtoUpdateAddress inputDtoUpdateAddress);
        OutputDtoGetByIdAddress GetById(InputDtoGetByIdAddress inputDtoGetByIdAddress);

        bool DeleteById(InputDtoDeleteByIdAddress inputDtoDeleteByIdAddress);
    }
}