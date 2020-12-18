using System;

namespace Domain.RequestCarpooling
{
    public class RequestCarpooling : IRequestCarpooling
    {
        public int Id { get; set; }
        public int IdRequestSender { get; set; }
        public int IdRequestReceiver { get; set; }
        public int Confirmation { get; set; }

        public RequestCarpooling()
        {
            
        }

        public RequestCarpooling(int id, int idRequestSender, int idRequestReceiver, int confirmation)
        {
            Id = id;
            IdRequestSender = idRequestSender;
            IdRequestReceiver = idRequestReceiver;
            Confirmation = confirmation;
        }

        protected bool Equals(RequestCarpooling other)
        {
            return Id == other.Id && IdRequestSender == other.IdRequestSender && IdRequestReceiver == other.IdRequestReceiver && Confirmation == other.Confirmation;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((RequestCarpooling) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, IdRequestSender, IdRequestReceiver, Confirmation);
        }
    }
}