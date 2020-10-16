
namespace WwwTC_SwitchCase
{
    internal sealed class ProcessCompletionMessage
    {
        public string MessageType { get; }
        public string MessageId { get; }
        public string ApplicationName { get; }

        public ProcessCompletionMessage(string messageId, string applicationName)
        {
            MessageType = "CompletionMessage";
            MessageId = messageId;
            ApplicationName = applicationName;
        }
    }
}
