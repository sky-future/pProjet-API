using System;
using Application.Services.RequestCarpooling;
using Application.Services.RequestCarpooling.DTO;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/requestcarpooling")]
    public class RequestCarpoolingController : ControllerBase
    {

        private readonly IRequestCarpoolingService _requestCarpoolingService;

        private RequestCarpoolingController(IRequestCarpoolingService requestCarpoolingService)
        {
            _requestCarpoolingService = requestCarpoolingService;
        }

        [HttpGet]
        [Route("{idReceiver}")]
        public ActionResult<OutputDTORequestCarpoolingByID> GetByIdRequest(int idReceiver)
        {
            InputDTOGetByIDRequestCarpooling inputDtoGetByIdRequestCarpooling = new InputDTOGetByIDRequestCarpooling
            {
                idRequestReceiver = idReceiver
            }; 
            Console.WriteLine(inputDtoGetByIdRequestCarpooling.idRequestReceiver);
            return Ok(_requestCarpoolingService.GetByIdReceiver(inputDtoGetByIdRequestCarpooling));
        }

    }
}