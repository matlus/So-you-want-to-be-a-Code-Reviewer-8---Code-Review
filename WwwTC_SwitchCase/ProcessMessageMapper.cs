using Newtonsoft.Json;
using System.Text;

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

        private static ProcessCompletionMessage DeserializeToCompletionMessage(string messageType, byte[] messageBody)
        {
            var messageBodyJson = Encoding.UTF8.GetString(messageBody);

            switch (messageType)
            {
                case "ProcessFinish":
                    var processStartFinishMessage = JsonConvert.DeserializeObject<ProcessStartFinishMessage>(messageBodyJson);
                    return new ProcessCompletionMessage(processStartFinishMessage.MessageId, processStartFinishMessage.ApplicationName);
                default:
                    var processExceptionMessage = JsonConvert.DeserializeObject<ProcessExceptionMessage>(messageBodyJson);
                    return new ProcessCompletionMessage(processExceptionMessage.MessageId, processExceptionMessage.ApplicationName);
            }
        }

        ////private static ProcessCompletionMessage DeserializeToCompletionMessage(string messageType, byte[] messageBody)
        ////{
        ////    ProcessCompletionMessage completionMessage = null;
        ////
        ////    switch (messageType)
        ////    {
        ////        case "ProcessFinish":
        ////            var processFinish = JsonConvert.DeserializeObject<ProcessStartFinishMessage>(Encoding.UTF8.GetString(messageBody));
        ////            completionMessage = new ProcessCompletionMessage(processFinish.MessageId, processFinish.ApplicationName);
        ////            break;
        ////        case "ProcessException":
        ////            var processException = JsonConvert.DeserializeObject<ProcessExceptionMessage>(Encoding.UTF8.GetString(messageBody));
        ////            completionMessage = new ProcessCompletionMessage(processException.MessageId, processException.ApplicationName);
        ////            break;
        ////        default:
        ////            break;
        ////    }

        ////    return completionMessage;
        ////}
    }
}
