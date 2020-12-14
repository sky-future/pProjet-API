namespace Domain.RequestCarpooling
{
    public class RequestCarpoolingFactory : IRequestCarpoolingFactory
    {
        public IRequestCarpooling CreateRequest(int idRequestSender, int idRequestReceiver, int confirmation)
        {
            return new RequestCarpooling
            {
                IdRequestSender = idRequestSender,
                IdRequestReceiver = idRequestReceiver,
                Confirmation = confirmation
            };
        }
    }
}