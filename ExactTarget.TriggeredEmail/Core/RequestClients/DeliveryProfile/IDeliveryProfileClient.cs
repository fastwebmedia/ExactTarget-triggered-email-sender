using System;

namespace ExactTarget.Subscription.Core.RequestClients.DeliveryProfile
{
    public interface IDeliveryProfileClient : IDisposable
    {
        string TryCreateBlankDeliveryProfile(string externalKey);
    }
}