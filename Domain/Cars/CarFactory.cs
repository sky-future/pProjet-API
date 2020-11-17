namespace Domain.Cars
{
    public class CarFactory : ICarFactory
    {
        public ICar createCar(string immatriculation, int idUser, int placeNb)
        {
            return new Car
            {
                Immatriculation = immatriculation,
                IdUser = idUser,
                PlaceNb = placeNb
            };
        }
    }
}