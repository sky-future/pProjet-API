using System.Collections.Generic;
using Application.Services.RequestCarpooling.DTO;

namespace Application.Services.RequestCarpooling
{
    public interface IRequestCarpoolingService
    {
        IEnumerable<OutputDtoRequestCarpoolingById> GetByIdReceiver(
            InputDtoGetByIdRequestCarpooling inputDtoGetByIdRequestCarpooling);
    }
}