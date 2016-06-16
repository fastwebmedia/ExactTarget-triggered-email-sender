using System.Collections.Generic;
using System.Linq;
using ExactTarget.TriggeredEmail.Core;
using ExactTarget.TriggeredEmail.Core.Configuration;
using ExactTarget.TriggeredEmail.ExactTargetApi;
using Attribute = ExactTarget.TriggeredEmail.ExactTargetApi.Attribute;
using ExactTarget.Subcription;
using System;

namespace ExactTarget.Subscription
{
    public enum RequestQueueing
    {
        No = 0,
        Yes,
    }

    public class ETSubscriber : IETSubscriber
    {
        private readonly IExactTargetConfiguration _config;

        public ETSubscriber(IExactTargetConfiguration config)
        {
            _config = config;
        }

        public void Add(ExactTargetSubscriber exactTargetSubscriber, int[] listIdArray, RequestQueueing requestQueueing = RequestQueueing.No, Priority priority = Priority.Medium)
        {
            var clientId = _config.ClientId;
            var client = SoapClientFactory.Manufacture(_config);

            // Create new Subscriber
            var subscriber = new Subscriber
            {
                EmailAddress = exactTargetSubscriber.EmailAddress,
                SubscriberKey = exactTargetSubscriber.SubscriberKey ?? exactTargetSubscriber.EmailAddress,
                Attributes =
                    exactTargetSubscriber.ReplacementValues.Select(value => new Attribute
                    {
                        Name = value.Key,
                        Value = value.Value
                    }).ToArray()
            };

            // Loop through listId's and add to Subscriber List []
            if ( listIdArray != null && listIdArray.Length > 0 )
            {
                var listEnumerator = listIdArray.GetEnumerator();

                foreach (int listId in listIdArray)
                {
                    SubscriberList list = new SubscriberList();

                    list.ID = listId;
                    list.IDSpecified = true;

                    subscriber.Lists[Convert.ToInt32(listEnumerator.Current)] = list;
                    listEnumerator.MoveNext();
                }
            }
            
            var co = new CreateOptions
            {
                RequestType = requestQueueing == RequestQueueing.No ? RequestType.Synchronous : RequestType.Asynchronous,
                RequestTypeSpecified = true,
                QueuePriority = priority == Priority.High ? TriggeredEmail.ExactTargetApi.Priority.High : TriggeredEmail.ExactTargetApi.Priority.Medium,
                QueuePrioritySpecified = true,
                PrioritySpecified = true,
                Priority = (sbyte)(priority == Priority.High ? 2 : 1),
            };
            
            using (client)
            {
                string requestId, status;
                var result = client.Create(
                    co,
                    new APIObject[] { subscriber },
                    out requestId, out status);

                ExactTargetResultChecker.CheckResult(result.FirstOrDefault());
            }
        }
    }
}
