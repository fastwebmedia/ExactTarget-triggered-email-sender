using System;
using ExactTarget.TriggeredEmail.Trigger;
using NUnit.Framework;
using ExactTarget.Subcription;
using ExactTarget.Subscription;
using ExactTarget.Subscription.Core.Configuration;

namespace ExactTarget.Subscription.Test.Integration
{
    public class AddNewSubscriberToListTest : TestBase
    {
        [Test]
        public void Add_A_New_Subscriber_To_List()
        {
            var externalKey = Guid.NewGuid().ToString();
            Subscribe(externalKey);
        }

        private void Subscribe(string externalKey)
        {
            var newSubscriber = new ExactTargetSubscriber(externalKey, TestSubscriberEmail);

            int[] listArray = null;

            listArray[0] = TestListId;

            var ETSubscriber = new ETSubscriber(Config);
            
            Assert.DoesNotThrow( () => ETSubscriber.Add(newSubscriber, listArray) );
        }

    }
}
