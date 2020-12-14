namespace Application.Services.RequestCarpooling.DTO
{
    public class OutputDtoRequestCarpooling
    {
        public int Id { get; set; }
        public int IdRequestSender { get; set; }
        public int IdRequestReceiver { get; set; }
        public int Confirmation { get; set; }
    }
}