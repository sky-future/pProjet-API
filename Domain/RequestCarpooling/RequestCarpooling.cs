namespace Domain.RequestCarpooling
{
    public class RequestCarpooling : IRequestCarpooling
    {
        public int Id { get; set; }
        public int IdRequestSender { get; set; }
        public int IdRequestReceiver { get; set; }
        public bool Confirmation { get; set; }

        public RequestCarpooling()
        {
            
        }

        public RequestCarpooling(int id, int idRequestSender, int idRequestReceiver, bool confirmation)
        {
            Id = id;
            IdRequestSender = idRequestSender;
            IdRequestReceiver = idRequestReceiver;
            Confirmation = confirmation;
        }
    }
}