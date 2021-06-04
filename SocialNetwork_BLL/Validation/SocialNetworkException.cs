using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace SocialNetwork_BLL.Validation
{
    [Serializable]
    public class SocialNetworkException : Exception
    {
        public SocialNetworkException()
        {
        }

        public SocialNetworkException(string message)
            : base(message)
        {
        }

        public SocialNetworkException(string message, Exception innerException)
            : base(message, innerException)
        {
        }

        // Without this constructor, deserialization will fail
        protected SocialNetworkException(SerializationInfo info, StreamingContext context)
            : base(info, context)
        {
        }
    }
}
