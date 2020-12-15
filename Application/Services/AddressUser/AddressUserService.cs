using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using Application.Repositories;
using Application.Services.AddressUser.Dto;
using Domain.Address;
using Domain.AddressUser;
using Domain.Users;

namespace Application.Services.AddressUser
{
    public class AddressUserService : IAddressUserService
    {
        private readonly IAddressUserRepository _addressUserRepository;
        private readonly IUserRepository _userRepository;
        private readonly IAddressRepository _addressRepository;
        private readonly IOfferCarpoolingRepository _offerCarpoolingRepository;

        public AddressUserService(IAddressUserRepository addressUserRepository, IUserRepository userRepository,
            IAddressRepository addressRepository, IOfferCarpoolingRepository offerCarpoolingRepository)
        {
            _addressUserRepository = addressUserRepository;
            _userRepository = userRepository;
            _addressRepository = addressRepository;
            _offerCarpoolingRepository = offerCarpoolingRepository;
        }

        public OutputDtoCreateAddressUser CreateAddressUser(
            InputDtoIdUserCreateAddressUser inputDtoIdUserCreateAddressUser,
            InputDtoIdAddressCreateAddressUser inputDtoIdAddressCreateAddressUser)
        {
            var addressUserInDb = _addressUserRepository.CreateAddressUser(inputDtoIdUserCreateAddressUser.IdUser,
                inputDtoIdAddressCreateAddressUser.IdAddress);

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

        public OutputDtoGetByIdUserAddressUser
            GetByIdUserAddressUser(InputDtoGetByIdUserAddressUser inputDtoGetByIdUserAddressUser)
        {
            var user = _userRepository.GetById(inputDtoGetByIdUserAddressUser.IdUser);

            var addressUser = _addressUserRepository.GetByUser(user);

            if (user == null || addressUser == null)
                return null;

            return new OutputDtoGetByIdUserAddressUser
            {
                Id = addressUser.Address.Id,
                Street = addressUser.Address.Street,
                Number = addressUser.Address.Number,
                PostalCode = addressUser.Address.PostalCode,
                City = addressUser.Address.City,
                Country = addressUser.Address.Country,
                Longitude = addressUser.Address.Longitude,
                Latitude = addressUser.Address.Latitude
            };
        }

        public IEnumerable<OutputDtoGetAddressListForCarpooling> GetAddressListForCarpooling()
        {
            var carpoolingOfferList = _offerCarpoolingRepository.Query();
            IList<int> userList = new List<int>();
            foreach (var offerCarpooling in carpoolingOfferList)
            {
                userList.Add(offerCarpooling.IdUser);
            }
            IList<IAddress> addressList = new List<IAddress>();
            IUser user;
            foreach (var userListFor in userList)
            {
                user = (User) _userRepository.GetById(userListFor);
                IAddressUser addressUser = _addressUserRepository.GetByUser(user);
                addressList.Add(addressUser.Address);
            }

            return addressList.Select(address => new OutputDtoGetAddressListForCarpooling
            {
                Id =  address.Id,
                Street = address.Street,
                Number = address.Number,
                PostalCode = address.PostalCode,
                City = address.City,
                Country = address.Country,
                Longitude = address.Longitude,
                Latitude = address.Latitude
            });
        }
    }
}