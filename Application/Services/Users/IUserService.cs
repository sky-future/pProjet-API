using System.Collections.Generic;
using Application.Services.Users.Dto;

namespace Application.Services.Users
{
    public interface IUserService
    {
        IEnumerable<OutputDtoQueryUser> Query();
        OutputDtoAddUser Create(InputDtoAddUser inputDtoAddUser);
        bool Update(int id, InputDtoUpdateUser inputDtoUpdateUser);
        OutputDtoGetByIdUser GetById(InputDtoGetByIdUser inputDtoGetById);

        bool DeleteById(InputDtoDeleteByIdUser inputDtoDeleteById);
        OutputDtoAuthenticate Authenticate(InputDtoAuthenticate inputDtoAuthenticate);

        bool UpdatePassword( InputDTOUpdateUserPassword inputDTOupdatePassword);

        bool UpdateLastConnexion(int id);

        bool CreateAdminUser(InputDtoAddAdminUser inputDtoAddAdminUser);
    }
}