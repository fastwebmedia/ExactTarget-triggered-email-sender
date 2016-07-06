using System;

namespace ExactTarget.Subscription.Core.RequestClients.Shared
{
    public interface ISharedCoreRequestClient : IDisposable
    {
        bool DoesObjectExist(string propertyName, string value, string objectType);
        string RetrieveObjectId(string propertyName, string value, string objectType);
    }
}