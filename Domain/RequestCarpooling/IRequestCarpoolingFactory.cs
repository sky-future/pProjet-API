namespace Domain.RequestCarpooling
{
    public interface IRequestCarpoolingFactory
    {
        IRequestCarpooling CreateRequest(int idRequestSender, int idRequestReceiver, int confirmation);
    }
}