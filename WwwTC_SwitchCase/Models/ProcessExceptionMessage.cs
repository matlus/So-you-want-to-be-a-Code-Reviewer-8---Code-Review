using System;

namespace WwwTC_SwitchCase
{
    internal sealed class ProcessExceptionMessage
    {
        public string MessageType { get; }
        public string MessageId { get; }
        public string ApplicationName { get; }
        public string ExceptionType { get; }
        public string ExceptionMessage { get; }
        public DateTime CreationDateTime { get; }

        public ProcessExceptionMessage(string messageType, string messageId, string applicationName, string exceptionType, string exceptionMessage, DateTime creationDateTime)
        {
            MessageType = messageType;
            MessageId = messageId;
            ApplicationName = applicationName;
            ExceptionType = exceptionType;
            ExceptionMessage = exceptionMessage;
            CreationDateTime = creationDateTime;
        }
    }
}
