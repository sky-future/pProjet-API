using System.Net.Mail;
using Domain.Users;
using NUnit.Framework;

namespace pAPI.UnitTests
{
    [TestFixture]
    public class UserFactoryTests
    {
        
        [Test]
        public void CreateSimpleUser_UserAttributs_IUser()
        {
            //Arrange
            IUserFactory userFactory = new UserFactory();
            string mail = "Test";
            string password = "Test";
            string lastConnexion = "Test";
            IUser expected = new User
            {
                Mail = mail,
                Password = password,
                LastConnexion = lastConnexion
            };
            
            //Act
            var result = userFactory.CreateSimpleUser(mail,password,lastConnexion);

            //Asserts
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void CreateAdminUser_AdminAttributs_Admin()
        {
            //Arrange
            IUserFactory userFactory = new UserFactory();
            string mail = "Test";
            string password = "Test";
            string lastConnexion = "";
            bool admin = true;
            IUser expected = new User
            {
                Mail = mail,
                Password = password,
                LastConnexion = lastConnexion,
                Admin = admin
            };
            
            //Act
            var result = userFactory.CreateAdminUser(mail,password,lastConnexion, admin);

            //Asserts
            Assert.AreEqual(expected, result);
        }
        
    }
}