using ExactTarget.Subscription.Core.Configuration;
using ExactTarget.Subscription.ExactTargetApi;

namespace ExactTarget.Subscription.Core
{
    public class SoapClientFactory
    {
        public static SoapClient Manufacture(IExactTargetConfiguration config)
        {
            var client = new SoapClient(config.SoapBinding ?? "ExactTarget.Soap", config.EndPoint);
            if (client.ClientCredentials == null) return null;
            client.ClientCredentials.UserName.UserName = config.ApiUserName;
            client.ClientCredentials.UserName.Password = config.ApiPassword;
            return client;
        }
    }
}
