using System;
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
        public IActionResult CreateRequestCarPooling([FromBody] InputDtoAddCarpoolingRequest inputDtoAddCarpoolingRequest)
        {
            var requestExist = _requestCarpoolingService.GetSenderById(inputDtoAddCarpoolingRequest.IdRequestSender);
            //Recherche si existe déjà fonctionne
             if (requestExist != null)
            {
                return BadRequest(new {message = "Vous avez déjà fais une demande !"});
            }
             
            _requestCarpoolingService.AddCarPoolingRequest(inputDtoAddCarpoolingRequest); 
            return Ok(new {message = "La demande a bien été enregistré"});
            
        }

        [HttpDelete]
        [Route("{idSender}/{idReceiver}")]
        public IActionResult DeleteRequestCarpooling(int idSender, int idReceiver)
        {
            InputDtoDeleteRequestCarpooling inputDtoDeleteRequestCarpooling = new InputDtoDeleteRequestCarpooling
            {
                IdSender = idSender,
                IdReceiver = idReceiver
            };

            if (_requestCarpoolingService.DeleteRequestCarpooling(inputDtoDeleteRequestCarpooling))
            {
                return Ok();
            }

            return NotFound();
        }

        [HttpPatch]
        [Route("confirmation")]
        public ActionResult UpdateConfirmation([FromBody] InputDtoUpdateConfirmation confirmation)
        {
            if (_requestCarpoolingService.UpdateConfirmation(confirmation))
            {
                Console.WriteLine("It works");
                return Ok(new {message = "La demande a été confirmé"});
            }

            return BadRequest(new {message = "Le demande n'a pas pus être envoyé."});
        }

    }
}