using System;
using System.Collections.Generic;
using System.Linq;
using Application.Repositories;
using Application.Services.Address.Dto;
using Domain.Address;

namespace Application.Services.Address
{
    public class AddressService : IAddressService
    {
        private readonly IAddressRepository _addressRepository;
        private readonly IAddressFactory _addressFactory = new AddressFactory();
        
        public AddressService(IAddressRepository addressRepository)
        {
            _addressRepository = addressRepository;
        }
        
        public IEnumerable<OutputDtoQueryAddress> Query()
        {
            return _addressRepository
                .Query()
                .Select(address => new OutputDtoQueryAddress
                {
                    id =  address.Id,
                    street = address.Street,
                    number = address.Number,
                    postalCode = address.PostalCode,
                    city = address.City,
                    country = address.Country,
                    longitude = address.Longitude,
                    latitude = address.Latitude
                });
        }

        public OutputDtoAddAddress Create(InputDtoAddAddress inputDtoAddAddress)
        {
            var addressFromDto = _addressFactory.CreateAddress(
                                        inputDtoAddAddress.street,
                                        inputDtoAddAddress.number,
                                        inputDtoAddAddress.postalCode,
                                        inputDtoAddAddress.city,
                                        inputDtoAddAddress.country,
                                        inputDtoAddAddress.longitude,
                                        inputDtoAddAddress.latitude);

            var addressInDb = _addressRepository.Create(addressFromDto);
            Console.WriteLine(addressInDb);
            return new OutputDtoAddAddress
            {
                id =  addressInDb.Id,
                street = addressInDb.Street,
                number = addressInDb.Number,
                postalCode = addressInDb.PostalCode,
                city = addressInDb.City,
                country = addressInDb.Country,
                longitude = addressInDb.Longitude,
                latitude = addressInDb.Latitude
            };
        }

        public bool Update(int id, InputDtoUpdateAddress inputDtoUpdateAddress)
        {
            var addressFromDto = _addressFactory.CreateAddress(inputDtoUpdateAddress.street, inputDtoUpdateAddress.number, inputDtoUpdateAddress.postalCode, inputDtoUpdateAddress.city, inputDtoUpdateAddress.country, inputDtoUpdateAddress.longitude, inputDtoUpdateAddress.latitude);
            
            return _addressRepository.Update(id, addressFromDto);
        }

        public OutputDtoGetByIdAddress GetById(InputDtoGetByIdAddress inputDtoGetByIdAddress)
        {
            var addressInDb = _addressRepository.GetById(inputDtoGetByIdAddress.id);
            
            return new OutputDtoGetByIdAddress
            {
                id =  addressInDb.Id,
                street = addressInDb.Street,
                number = addressInDb.Number,
                postalCode = addressInDb.PostalCode,
                city = addressInDb.City,
                country = addressInDb.Country,
                longitude = addressInDb.Longitude,
                latitude = addressInDb.Latitude
            };
        }

        public bool DeleteById(InputDtoDeleteByIdAddress inputDtoDeleteByIdAddress)
        {
            return _addressRepository.DeleteById(inputDtoDeleteByIdAddress.id);
        }
    }
}