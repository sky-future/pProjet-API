namespace Domain.Cars
{
    public interface ICarFactory
    {
        ICar createCar(string immatriculation, int idUser, int nbPlace);
    }
}