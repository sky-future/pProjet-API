namespace Application.Services.RequestCarpooling.DTO
{
    public class InputDTOAddCarpoolingRequest
    {
        public int IdRequestSender { get; set; }
        public int IdRequestReceiver { get; set; }
        public bool Confirmation { get; set; }
    }
}