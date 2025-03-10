using System.Collections.Generic;
using Application.Services.AddressUser.Dto;

namespace Application.Services.AddressUser
{
    public interface IAddressUserService
    {
        OutputDtoCreateAddressUser CreateAddressUser(InputDtoIdUserCreateAddressUser inputDtoIdUserCreateAddressUser,
            InputDtoIdAddressCreateAddressUser inputDtoIdAddressCreateAddressUser);

        OutputDtoGetByIdAddressAddressUser GetByIdAddressAddressUser(
            InputDtoGetByIdAddressAdressUser inputDtoGetByIdAddressAddressUser);
        OutputDtoGetByIdUserAddressUser GetByIdUserAddressUser(
            InputDtoGetByIdUserAddressUser inputDtoGetByIdUserAddressUser);

        IEnumerable<OutputDtoGetAddressListForCarpooling> GetAddressListForCarpooling();
    }
}