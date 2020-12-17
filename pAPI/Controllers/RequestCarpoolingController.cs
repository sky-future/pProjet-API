using Application.Services.RequestCarpooling;
using Application.Services.RequestCarpooling.DTO;
using Application.Services.Users;
using Application.Services.Users.Dto;
using Microsoft.AspNetCore.Mvc;

namespace pAPI.Controllers
{
    [ApiController]
    [Route("api/requestCarpooling")]
    public class RequestCarpoolingController : ControllerBase
    {

        private readonly IRequestCarpoolingService _requestCarpoolingService;
        private readonly IUserService _userService;

        public RequestCarpoolingController(IRequestCarpoolingService requestCarpoolingService, IUserService userService)
        {
            _requestCarpoolingService = requestCarpoolingService;
            _userService = userService;
        }

        [HttpGet]
        [Route("{idReceiver}")]
        public ActionResult<OutputDtoRequestCarpoolingById> GetByIdRequest(int idReceiver)
        {
            if (idReceiver < 0)
            {
                return BadRequest(new {message = "L'id n'est pas conforme."});
            }
            
            var inputDtoGetByIdUser = new InputDtoGetByIdUser
            {
                id = idReceiver
            };
            
            if (_userService.GetById(inputDtoGetByIdUser) == null)
            {
                return BadRequest(new
                    {message = "L'utilisateur n'existe pas."});
            }
            
            InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver = new InputDtoGetRequestByIdReceiver
            {
                IdRequestReceiver = idReceiver
            };

            var get = _requestCarpoolingService.GetRequestProfileByIdReceiver(inputDtoGetRequestByIdReceiver);

            if (get == null)
            {
                return BadRequest(new {message = "Aucune requête n'a été trouvée pour cet utilisateur"});
            }
            
            return Ok(get);
        }
        
        //TODO Vérifier intégralité des données
        //TODO Gérer qu'on ne puisse pas envoyée de demande si on est chauffeur
        [HttpPost]
        public IActionResult CreateRequestCarPooling([FromBody] InputDtoAddCarpoolingRequest inputDtoAddCarpoolingRequest)
        {
            InputDtoGetRequestByIdSender inputDtoGetRequestByIdSender = new InputDtoGetRequestByIdSender
            {
                IdSender = inputDtoAddCarpoolingRequest.IdRequestSender
            };
            
            InputDtoGetRequestByIdReceiver inputDtoGetRequestByIdReceiver = new InputDtoGetRequestByIdReceiver
            {
                IdRequestReceiver = inputDtoAddCarpoolingRequest.IdRequestReceiver
            };
            
            var requestExist = _requestCarpoolingService.GetRequestByIdSenderReceiver(inputDtoGetRequestByIdSender, inputDtoGetRequestByIdReceiver);
            //Recherche si existe déjà fonctionne
             if (requestExist != null)
            {
                return BadRequest(new {message = "Vous avez déjà fais une demande à cette personne !"});
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

        //TODO Vérifier intégralité des données
        [HttpPatch]
        [Route("confirmation")]
        public ActionResult UpdateConfirmation([FromBody] InputDtoUpdateConfirmation confirmation)
        {
            
            if (_requestCarpoolingService.UpdateConfirmation(confirmation))
            {
                return Ok(new {message = "La demande a été confirmé"});
            }

            return BadRequest(new {message = "Le demande n'a pas pus être envoyé."});
        }

    }
}