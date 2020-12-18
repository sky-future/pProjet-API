using System;

namespace Domain.Cars
{
    public class Car : ICar
    {
        public int Id { get; set; }
        public string Immatriculation { get; set; }
        public int IdUser { get; set; }
        public int PlaceNb { get; set; }

        public Car()
        {
            
        }

        public Car(int id, string immatriculation, int idUser, int placeNb)
        {
            Id = id;
            Immatriculation = immatriculation;
            IdUser = idUser;
            PlaceNb = placeNb;
        }

        protected bool Equals(Car other)
        {
            return Id == other.Id && Immatriculation == other.Immatriculation && IdUser == other.IdUser && PlaceNb == other.PlaceNb;
        }

        public override bool Equals(object obj)
        {
            if (ReferenceEquals(null, obj)) return false;
            if (ReferenceEquals(this, obj)) return true;
            if (obj.GetType() != this.GetType()) return false;
            return Equals((Car) obj);
        }

        public override int GetHashCode()
        {
            return HashCode.Combine(Id, Immatriculation, IdUser, PlaceNb);
        }
    }
}