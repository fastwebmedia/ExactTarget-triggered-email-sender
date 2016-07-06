using ExactTarget.Subscription.Core.Configuration;
using NUnit.Framework;

namespace ExactTarget.Subscription.Test.Integration
{
    [TestFixture]
    public class TestBase
    {
        public ExactTargetConfiguration Config {get; private set; }
        public string TestSubscriberEmail { get; private set; }
        public int TestListId { get; private set; }

        [TestFixtureSetUp]
        public void TestFixtureSetUp()
        {
            TestSubscriberEmail = "";

            if (string.IsNullOrWhiteSpace(TestSubscriberEmail))
            {
                Assert.Fail("You have to supply value for TestSubscriberEmail before running these tests.");
            }

            Config = new ExactTargetConfiguration
            {
                ApiUserName = "",
                ApiPassword = "",
                EndPoint = "https://webservice.s6.exacttarget.com/Service.asmx",//update your correct endpoint
                ClientId = 6269489, // optional  business unit to use
            };

            if (string.IsNullOrWhiteSpace(Config.ApiUserName) || string.IsNullOrWhiteSpace(Config.ApiPassword))
            {
                Assert.Fail("You need to supply API credentials before running these tests.");
            }
            
        }
    }

    
}
