namespace Domain.RequestCarpooling
{
    public class RequestCarpoolingFactory : IRequestCarpoolingFactory
    {
        public IRequestCarpooling createRequest(int idRequestSender, int idRequestReceiver, bool confirmation)
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