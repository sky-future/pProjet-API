namespace Application.Services.RequestCarpooling.DTO
{
    public class InputDtoAddCarpoolingRequest
    {
        public int IdRequestSender { get; set; }
        public int IdRequestReceiver { get; set; }
        public int Confirmation { get; set; }
    }
}