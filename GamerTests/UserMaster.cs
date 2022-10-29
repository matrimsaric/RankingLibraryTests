using RankingLibrary.SupportObjects.PlayerObjects;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RankingLibraryTests.GamerTests
{
    [TestClass]
    public class UserMaster
    {
        private string hackText = @"<img src=x onerror=&#x22;&#x61;&#x6C;&#x65;&#x72;&#x74;&#x28;&#x31;&#x29;&#x22;>";

        [TestMethod]
        public void TestInvalidUserId()
        {
            try
            {
                Player newPlayer = new Player(-22, "grom");
            }
            catch
            {
                return;
            }
            Assert.Fail("Player had an invalid id and should have thrown an execption");
            
        }

        [TestMethod]
        public void TestUserNameHack()
        {
            try
            {
                Player newPlayer = new Player(-22, hackText);
            }
            catch
            {
                return;
            }
            Assert.Fail("Player had hack text accepted..");

        }





    }
}
