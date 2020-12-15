namespace Application.Services.RequestCarpooling.DTO
{
    public class InputDtoUpdateConfirmation
    {
        public int idRequestReceiver { get; set; }
        public int idRequestSender { get; set; }
        public int confirmation { get; set; }
    }
}