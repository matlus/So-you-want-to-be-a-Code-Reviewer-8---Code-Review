using Newtonsoft.Json;
using System.Text;
using System.Threading;

namespace WwwTC_SwitchCase
{
    internal static class ProcessMessageMapper
    {
        public static ProcessCompletionMessage MapToProcessCompletionMessage(string messageType, byte[] messageBody)
        {
            if (messageType != "ProcessFinish" && messageType != "ProcessException")
            {
                return null;
            }

            return DeserializeToCompletionMessage(messageType, messageBody);            
        }

        private static ProcessCompletionMessage DeserializeToCompletionMessage_Orig(string messageType, byte[] messageBody)
        {
            ProcessCompletionMessage processCompletionMessage = null;

            switch (messageType)
            {
                case "ProcessFinish":
                    var processFinish = JsonConvert.DeserializeObject<ProcessStartFinishMessage>(Encoding.UTF8.GetString(messageBody));
                    processCompletionMessage = new ProcessCompletionMessage(processFinish.MessageId, processFinish.ApplicationName, new CancellationToken());
                    break;
                case "ProcessException":
                    var processException = JsonConvert.DeserializeObject<ProcessExceptionMessage>(Encoding.UTF8.GetString(messageBody));
                    processCompletionMessage = new ProcessCompletionMessage(processException.MessageId, processException.ApplicationName, new CancellationToken());
                    break;
                default:
                    throw new MessageTypeNotSupportedException($"The Message Type: {messageType} is either not Expected or not Supported");
            }

            return processCompletionMessage;
        }

        private static ProcessCompletionMessage DeserializeToCompletionMessage(string messageType, byte[] messageBody)
        {
            var cancellationToken = new CancellationToken();

            switch (messageType)
            {
                case "ProcessFinish":
                    var processStartFinishMessage = DeSerialize<ProcessStartFinishMessage>(messageBody);
                    return new ProcessCompletionMessage(processStartFinishMessage.MessageId, processStartFinishMessage.ApplicationName, cancellationToken);
                case "ProcessException":
                    var processExceptionMessage = DeSerialize<ProcessExceptionMessage>(messageBody);
                    return new ProcessCompletionMessage(processExceptionMessage.MessageId, processExceptionMessage.ApplicationName, cancellationToken);
                default:
                    throw new MessageTypeNotSupportedException($"The Message Type: {messageType} is either not Expected or not Supported");
            }
        }

        private static T DeSerialize<T>(byte[] jsonBytes)
        {
            var jsonString = Encoding.UTF8.GetString(jsonBytes);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }
    }
}