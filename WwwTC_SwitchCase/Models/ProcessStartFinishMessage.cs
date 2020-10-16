
using System;

namespace WwwTC_SwitchCase
{
    internal sealed class ProcessStartFinishMessage
    {
        public string MessageType { get; }
        public string MessageId { get; }
        public string ApplicationName { get; }
        public string ProcessName { get; }
        public string StepName { get; }
        public DateTime CreationDateTime { get; }

        public ProcessStartFinishMessage(string messageType, string messageId, string applictionName, string processName, string stepName, DateTime creationDateTime)
        {
            MessageType = messageType;
            MessageId = messageId;
            ApplicationName = applictionName;
            ProcessName = processName;
            StepName = stepName;
            CreationDateTime = creationDateTime;
        }
    }
}
