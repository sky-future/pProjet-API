using System.Collections.Generic;
using Application.Services.Users.Dto;

namespace Application.Services.Users
{
    public interface IUserService
    {
        IEnumerable<OutputDtoQueryUser> Query();
        OutputDtoAddUser Create(InputDtoAddUser inputDtoAddUser);
        bool Update(int id, InputDtoUpdateUser inputDtoUpdateUser);
        OutputDtoGetById GetById(InputDtoGetById inputDtoGetById);

        bool DeleteById(InputDtoDeleteById inputDtoDeleteById);
        OutputDtoAuthenticate Authenticate(InputDtoAuthenticate inputDtoAuthenticate);
    }
}