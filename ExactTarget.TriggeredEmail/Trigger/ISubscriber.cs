using ExactTarget.Subcription;
using ExactTarget.TriggeredEmail.ExactTargetApi;

namespace ExactTarget.Subscription
{
    public interface IETSubscriber
    {
        void Add(ExactTargetSubscriber exactTargetSubscriber, int[] listIdArray, RequestQueueing requestQueueing = RequestQueueing.No, Priority priority = Priority.Medium);
    }
}