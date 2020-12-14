using Domain.Shared;

namespace Domain.RequestCarpooling
{
    public interface IRequestCarpooling : IEntity
    {
       // public int Id { get; set; }
        public int IdRequestSender { get; set; }
        public int IdRequestReceiver { get; set; }
        public bool Confirmation { get; set; } 
    }
}