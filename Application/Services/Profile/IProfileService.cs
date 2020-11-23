using System.Collections.Generic;
using Application.Services.Address.Dto;
using Application.Services.Profile.DTO;

namespace Application.Services.Profile
{
    public interface IProfileService
    {
        IEnumerable<OutputDtoQueryProfile> Query();
        OutputDtoAddProfile Create(InputDtoAddProfile inputDtoAddProfile);
        bool Update(int id, InputDtoUpdateProfile inputDtoUpdateAddress);
        OutputDtoGetByIdProfile GetById(InputDtoGetByIdProfile inputDtoGetByIdProfile);

        bool DeleteById(InputDtoDeleteByIdProfile inputDtoDeleteByIdProfile);
    }
}