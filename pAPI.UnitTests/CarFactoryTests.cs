using Domain.Cars;
using NUnit.Framework;

namespace pAPI.UnitTests
{
    [TestFixture]
    public class CarFactoryTests
    {
        [Test]
        public void CreateCar_CarAttributs_ICar()
        {
            //Arrange
            ICarFactory carFactory = new CarFactory();
            string immatriculation = "Test";
            int idUser = 1;
            int placeNb = 3;
            ICar expected = new Car
            {
                Immatriculation = immatriculation,
                IdUser = idUser,
                PlaceNb = placeNb
            };
            
            //Act
            var result = carFactory.createCar(immatriculation,idUser,placeNb);

            //Asserts
            Assert.AreEqual(expected, result);
        }
    }
}