
using System.Runtime.ConstrainedExecution;
using System.Threading;

namespace WwwTC_SwitchCase
{
    internal sealed class ProcessCompletionMessage
    {
        public string MessageType { get; }
        public string MessageId { get; }
        public string ApplicationName { get; }
        public CancellationToken CancellationToken { get; }

        public ProcessCompletionMessage(string messageId, string applicationName, CancellationToken cancellationToken)
        {
            MessageType = "CompletionMessage";
            MessageId = messageId;
            ApplicationName = applicationName;
            CancellationToken = cancellationToken;
        }
    }
}
