using System;

namespace ExactTarget.Subscription.Core.Exceptions
{
    public class ExactTargetException : Exception
    {
        public ExactTargetException()
        {
        }

        public ExactTargetException(string message) : base(message)
        {
        }

        public ExactTargetException(string message, Exception innerException) : base(message, innerException)
        {
        }
    }
}
