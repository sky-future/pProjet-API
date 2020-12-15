namespace Application.Services.RequestCarpooling.DTO
{
    public class InputDtoUpdateConfirmation
    {
        public int IdRequestReceiver { get; set; }
        public int IdRequestSender { get; set; }
        public int Confirmation { get; set; }
    }
}