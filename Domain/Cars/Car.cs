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
    }
}