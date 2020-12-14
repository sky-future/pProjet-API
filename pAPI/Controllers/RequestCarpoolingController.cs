using System;
using Application.Services.RequestCarpooling;
using Application.Services.RequestCarpooling.DTO;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/requestCarpooling")]
    public class RequestCarpoolingController : ControllerBase
    {

        private readonly IRequestCarpoolingService _requestCarpoolingService;

        public RequestCarpoolingController(IRequestCarpoolingService requestCarpoolingService)
        {
            _requestCarpoolingService = requestCarpoolingService;
        }

        [HttpGet]
        [Route("{idReceiver}")]
        public ActionResult<OutputDtoRequestCarpoolingById> GetByIdRequest(int idReceiver)
        {
            InputDtoGetByIdRequestCarpooling inputDtoGetByIdRequestCarpooling = new InputDtoGetByIdRequestCarpooling
            {
                IdRequestReceiver = idReceiver
            };
            return Ok(_requestCarpoolingService.GetByIdReceiver(inputDtoGetByIdRequestCarpooling));
        }
    }
}