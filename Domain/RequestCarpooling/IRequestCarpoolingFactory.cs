namespace Domain.RequestCarpooling
{
    public interface IRequestCarpoolingFactory
    {
        IRequestCarpooling createRequest(int idRequestSender, int idRequestReceiver, bool confirmation);
    }
}