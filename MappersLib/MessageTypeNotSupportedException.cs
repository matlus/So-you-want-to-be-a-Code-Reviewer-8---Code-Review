using System;
using System.Diagnostics.CodeAnalysis;
using System.Runtime.Serialization;

namespace WwwTC_SwitchCase
{

    [Serializable]
    [ExcludeFromCodeCoverage]
    public sealed class MessageTypeNotSupportedException : Exception
    {
        public MessageTypeNotSupportedException() { }
        public MessageTypeNotSupportedException(string message) : base(message) { }
        public MessageTypeNotSupportedException(string message, Exception inner) : base(message, inner) { }
        private MessageTypeNotSupportedException(SerializationInfo info, StreamingContext context) : base(info, context) { }
    }
}
