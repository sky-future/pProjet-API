using Domain.RequestCarpooling;
using NUnit.Framework;

namespace pAPI.UnitTests
{
    [TestFixture]
    public class RequestCarpoolingFactoryTests
    {
        [Test]
        public void CreateRequest_RequestAttributs_IRequestCarpooling()
        {
            //Arrange
            IRequestCarpoolingFactory requestCarpoolingFactory = new RequestCarpoolingFactory();
            int idReceiver = 1;
            int idSender = 2;
            int confirmation = 1;
            IRequestCarpooling expected = new RequestCarpooling
            {
                IdRequestReceiver = idReceiver,
                IdRequestSender = idSender,
                Confirmation = confirmation
            };
            
            //Act
            var result = requestCarpoolingFactory.CreateRequest(idSender,idReceiver,confirmation);

            //Asserts
            Assert.AreEqual(expected, result);
        }
    }
}