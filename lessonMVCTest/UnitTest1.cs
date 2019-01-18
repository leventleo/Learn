using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace lessonMVCTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void SingalRConnectTest()
        {
            HubConnection connection = new HubConnection("http://localhost:65092/updatehub", false);

            var creathubconnect = connection.CreateHubProxy("updatehub");
            var strint = connection.Url;

            Assert.ThrowsException(connection.start());
           

        }
    }
}
