﻿using System;
using System.Diagnostics;
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

        [HttpPost]
        public IActionResult CreateRequestCarPooling([FromBody] InputDTOAddCarpoolingRequest inputDtoAddCarpoolingRequest)
        {
            //Recherche si existe déjà fonctionne
            Boolean requestExist = _requestCarpoolingService.GetSenderById(inputDtoAddCarpoolingRequest.IdRequestSender).IdRequestSender !=0 ;
             Console.WriteLine(requestExist);
             if (requestExist)
            {
                return BadRequest(new {message = "Vous avez déjà fais une demande !"});
            }
             
             _requestCarpoolingService.AddCarPoolingRequest(inputDtoAddCarpoolingRequest); 
             return Ok(new {message = "La demande a bien été enregistré"});
        }

    }
}