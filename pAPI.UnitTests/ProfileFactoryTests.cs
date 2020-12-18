using Domain.Profile;
using NUnit.Framework;

namespace pAPI.UnitTests
{
    [TestFixture]
    public class ProfileFactoryTests
    {
        [Test]
        public void CreateProfile_ProfileAttributs_IProfile()
        {
            //Arrange
            IProfileFactory profileFactory = new ProfileFactory();
            string lastname = "Test";
            string firstname = "Test";
            string matricule = "Test";
            string telephone = "Test";
            string descript = "Test";
            int idUser = 1;
            IProfile expected = new Profile
            {
                Lastname = lastname,
                Firstname = firstname,
                Matricule = matricule,
                Telephone = telephone,
                Descript = descript,
                IdUser = idUser
            };
            
            //Act
            var result = profileFactory.CreateProfile(lastname,firstname,matricule,telephone,descript,idUser);

            //Asserts
            Assert.AreEqual(expected, result);
        }
        
        [Test]
        public void CreateProfileWithId_ProfileAttributs_IProfile()
        {
            //Arrange
            IProfileFactory profileFactory = new ProfileFactory();
            int id = 2;
            string lastname = "Test";
            string firstname = "Test";
            string matricule = "Test";
            string telephone = "Test";
            string descript = "Test";
            IProfile expected = new Profile
            {
                Id = id,
                Lastname = lastname,
                Firstname = firstname,
                Matricule = matricule,
                Telephone = telephone,
                Descript = descript
            };
            
            //Act
            var result = profileFactory.CreateProfileWithId(id,lastname,firstname,matricule,telephone,descript);

            //Asserts
            Assert.AreEqual(expected, result);
        }
    }
}