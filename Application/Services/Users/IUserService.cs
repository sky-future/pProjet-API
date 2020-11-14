using System.Collections.Generic;
using Application.Services.Users.Dto;

namespace Application.Services.Users
{
    public interface IUserService
    {
        IEnumerable<OutputDtoQueryUser> Query();
        OutputDtoAddUser Create(InputDtoAddUser inputDtoAddUser);
        bool Update(int id, InputDtoUpdateUser inputDtoUpdateUser);
    }
}