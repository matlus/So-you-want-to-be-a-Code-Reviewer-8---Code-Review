using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;

namespace WwwTC_SwitchCase
{
    internal static class ProcessMessageMapper
    {
        public static ProcessCompletionMessage MapToProcesMessagesToProcessCompletionMessage(string messageType, byte[] messageBody)
        {
            if (messageType == "ProcessFinish" || messageType == "ProcessException")
            {
                var completionMessage = DeserializeToCompletionMessage(messageType, messageBody);
                Console.WriteLine(completionMessage);
            }

            return null;
        }

        private static ProcessCompletionMessage DeserializeToCompletionMessage(string messageType, byte[] messageBody)
        {
            ProcessCompletionMessage completionMessage = null;

            switch (messageType)
            {
                case "ProcessFinish":
                    var processFinish = JsonSerializer.Deserialize<ProcessStartFinishMessage>(messageBody);
                    completionMessage = new ProcessCompletionMessage(processFinish.MessageId, processFinish.ApplicationName);
                    break;
                case "ProcessException":
                    var processException = JsonSerializer.Deserialize<ProcessExceptionMessage>(messageBody);
                    completionMessage = new ProcessCompletionMessage(processException.MessageId, processException.ApplicationName);
                    break;
                default:
                    break;
            }

            return completionMessage;
        }
    }
}
