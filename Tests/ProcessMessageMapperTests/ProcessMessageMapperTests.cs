using Newtonsoft.Json;
using System;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using WwwTC_SwitchCase;
using Xunit;

namespace ProcessMessageMapperTests
{
    [ExcludeFromCodeCoverage]
    public class ProcessMessageMapperTests
    {
        [Fact]
        [Trait("Class Tests", "")]
        public void MapToCompletionMessage_WhenMessageTypeIsProcessFinish_ShouldReturnProcessCompletionMessage()
        {
            // Arrange
            var messageType = "ProcessFinish";
            var expectedMessageId = Guid.NewGuid().ToString("N");
            var expectedApplicationName = "MyApplicationName";

            var processMessageStartFinish = new ProcessStartFinishMessage(messageType, expectedMessageId, expectedApplicationName, "Proc name", "Step 1", DateTime.UtcNow);
            var messageBody = GetJsonByteArray(processMessageStartFinish);

            // Act
            var actualProcessCompleteMessage = ProcessMessageMapper.MapToProcessCompletionMessage(messageType, messageBody);

            // Assert
            Assert.Equal("CompletionMessage", actualProcessCompleteMessage.MessageType);
            Assert.Equal(expectedMessageId, actualProcessCompleteMessage.MessageId);
            Assert.Equal(expectedApplicationName, actualProcessCompleteMessage.ApplicationName);
            Assert.False(actualProcessCompleteMessage.CancellationToken.IsCancellationRequested);
        }

        [Fact]
        [Trait("Class Tests", "")]
        public void MapToCompletionMessage_WhenMessageTypeIsProcessException_ShouldReturnProcessCompletionMessage()
        {
            // Arrange
            var messageType = "ProcessException";
            var expectedMessageId = Guid.NewGuid().ToString("N");
            var expectedApplicationName = "MyApplicationName";

            var processMessageStartFinish = new ProcessExceptionMessage(messageType, expectedMessageId, expectedApplicationName, "SomeExceptionType", "Exception Message", DateTime.UtcNow);
            var messageBody = GetJsonByteArray(processMessageStartFinish);

            // Act
            var actualProcessCompleteMessage = ProcessMessageMapper.MapToProcessCompletionMessage(messageType, messageBody);

            // Assert
            Assert.Equal("CompletionMessage", actualProcessCompleteMessage.MessageType);
            Assert.Equal(expectedMessageId, actualProcessCompleteMessage.MessageId);
            Assert.Equal(expectedApplicationName, actualProcessCompleteMessage.ApplicationName);
            Assert.False(actualProcessCompleteMessage.CancellationToken.IsCancellationRequested);
        }

        [Theory]
        [InlineData("ProcessStart")]
        [InlineData("SomeUnsupportedType")]
        [Trait("Class Tests", "")]
        public void MapToCompletionMessage_WhenMessageTypeIsProcessStart_ShouldReturnNull(string messageType)
        {
            // Arrange
            byte[] messageBody = new byte[0];

            // Act
            var actualProcessCompleteMessage = ProcessMessageMapper.MapToProcessCompletionMessage(messageType, messageBody);

            // Assert
            Assert.Null(actualProcessCompleteMessage);
        }

        [Fact]
        [Trait("Class Tests", "")]
        public void MapToCompletionMessage_WhenMessageTypeIsNotFinishOrException_ShouldReturnNull()
        {
            // Arrange
            var messageType = "SomeUnsupportedType";
            var expectedMessageId = Guid.NewGuid().ToString("N");
            var expectedApplicationName = "MyApplicationName";

            var processMessageStartFinish = new ProcessStartFinishMessage(messageType, expectedMessageId, expectedApplicationName, "Proc name", "Step 1", DateTime.UtcNow);
            var messageBody = GetJsonByteArray(processMessageStartFinish);

            // Act
            var actualProcessCompleteMessage = ProcessMessageMapper.MapToProcessCompletionMessage(messageType, messageBody);

            // Assert
            Assert.Null(actualProcessCompleteMessage);
        }

        [Theory]
        [InlineData("ProcessFinish")]
        [InlineData("ProcessException")]
        [Trait("Class Tests", "")]
        public void MapToCompletionMessage_WhenMessageBodyByteArrayIsNotJsonAndMessageTypeIsValid_ShouldThrow(string messageType)
        {
            // Arrange
            var messageBody = GetJsonByteArray("Some non-json string");

            // Act & Assert
            Assert.Throws<JsonSerializationException>(() => ProcessMessageMapper.MapToProcessCompletionMessage(messageType, messageBody));
        }

        private static byte[] GetJsonByteArray<T>(T message) where T : class
        {
            return Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(message));
        }
    }
}
