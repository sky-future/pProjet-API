using System.Collections.Generic;
using Application.Services.Address.Dto;
using Application.Services.Profile.DTO;

namespace Application.Services.Profile
{
    public interface IProfileService
    {
        IEnumerable<OutputDtoQueryProfile> Query();
        OutputDtoAddProfile Create(InputDtoAddProfile inputDtoAddProfile);
        bool Update(InputDtoUpdateByIdProfile inputDtoUpdateByIdProfile, InputDtoUpdateProfile inputDtoUpdateProfile);
        OutputDtoGetByIdProfile GetById(InputDtoGetByIdProfile inputDtoGetByIdProfile);

        OutputDtoGetByidUserProfile GetByUserIdProfile(InputDtoGetByidUserProfile inputDtoGetByidUserProfile);

        bool DeleteById(InputDtoDeleteByIdProfile inputDtoDeleteByIdProfile);
    }
}