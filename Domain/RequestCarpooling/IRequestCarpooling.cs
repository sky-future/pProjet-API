using Domain.Shared;

namespace Domain.RequestCarpooling
{
    public interface IRequestCarpooling : IEntity
    {
        public int IdRequestSender { get; set; }
        public int IdRequestReceiver { get; set; }
        public int Confirmation { get; set; } 
    }
}