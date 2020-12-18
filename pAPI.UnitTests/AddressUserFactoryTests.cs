using Domain.Address;
using Domain.AddressUser;
using Domain.Users;
using NUnit.Framework;

namespace pAPI.UnitTests
{
    [TestFixture]
    public class AddressUserFactoryTests
    {
        [Test]
        public void CreateAddressUser_AddressUserAttributs_IAddressUser()
        {
            //Arrange
            IAddressUserFactory addressUserFactory = new AddressUserFactory();
            string mail = "Test";
            string password = "Test";
            string lastConnexion = "Test";
            string street = "Test";
            int number = 10;
            int codePostal = 7000;
            string city = "Test";
            string country = "Test";
            string longitude = "Test";
            string latitude = "Test";
            IUser user = new User
            {
                Mail = mail,
                Password = password,
                LastConnexion = lastConnexion
            };
            IAddress address = new Address
            {
                Street = street,
                Number = number,
                PostalCode = codePostal,
                City = city,
                Country = country,
                Longitude = longitude,
                Latitude = latitude
            };
            IAddressUser expected = new AddressUser
            {
                User = user,
                Address = address
            };
            
            //Act
            var result = addressUserFactory.CreateAddressUser(user,address);

            //Asserts
            Assert.AreEqual(expected, result);
        }
    }
}