using Domain.Address;
using NUnit.Framework;

namespace pAPI.UnitTests
{
    [TestFixture]
    public class AddressFactoryTests
    {
        
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void CreateAddress_AddressAttributs_IAddress()
        {
            //Arrange
            var addressFactory = new AddressFactory();
            string street = "Test";
            int number = 10;
            int codePostal = 7000;
            string city = "Test";
            string country = "Test";
            string longitude = "Test";
            string latitude = "Test";
            IAddress expected = new Address
            {
                Street = street,
                Number = number,
                PostalCode = codePostal,
                City = city,
                Country = country,
                Longitude = longitude,
                Latitude = latitude
            };
            
            //Act
            var result = addressFactory.CreateAddress(street,number,codePostal,city,country,longitude,latitude);

            //Asserts
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void CreateAddressWithoutCoords_AddressAttributsWithoutCoords_IAddressWithoutCoords()
        {
            //Arrange
            var addressFactory = new AddressFactory();
            string street = "Test";
            int number = 10;
            int codePostal = 7000;
            string city = "Test";
            string country = "Test";
            IAddress expected = new Address
            {
                Street = street,
                Number = number,
                PostalCode = codePostal,
                City = city,
                Country = country
            };
            
            //Act
            var result = addressFactory.CreateAddressWithoutCoord(street,number,codePostal,city,country);

            //Asserts
            Assert.AreEqual(expected, result);
        }
        
    }
}