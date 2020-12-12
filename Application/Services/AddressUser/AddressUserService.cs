using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Repositories;
using Application.Services.AddressUser.Dto;
using Domain.Users;

namespace Application.Services.AddressUser
{
    public class AddressUserService : IAddressUserService
    {
        private readonly IAddressUserRepository _addressUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        
        public AddressUserService(IAddressUserRepository addressUserRepository, IUserRepository userRepository, IAddressRepository addressRepository)
        {
            _addressUserRepository = addressUserRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
        }

        public OutputDtoCreateAddressUser CreateAddressUser(InputDtoIdUserCreateAddressUser inputDtoIdUserCreateAddressUser,
            InputDtoIdAddressCreateAddressUser inputDtoIdAddressCreateAddressUser)
        {
            var addressUserInDb = _addressUserRepository.CreateAddressUser(inputDtoIdUserCreateAddressUser.IdUser, inputDtoIdAddressCreateAddressUser.IdAddress);

            return new OutputDtoCreateAddressUser()
            {
                Id = addressUserInDb.Id,
                IdUser = addressUserInDb.User.Id,
                IdAddress = addressUserInDb.Address.Id
            };
        }

        public OutputDtoGetByIdAddressAddressUser GetByIdAddressAddressUser(
            InputDtoGetByIdAddressAdressUser inputDtoGetByIdAddressAddressUser)
        {
            var address = _addressRepository.GetById(inputDtoGetByIdAddressAddressUser.IdAddress);

            var addressUser = _addressUserRepository.GetByAddress(address);

            return new OutputDtoGetByIdAddressAddressUser
            {
                IdUser = addressUser.User.Id
            };
        }
    }
}