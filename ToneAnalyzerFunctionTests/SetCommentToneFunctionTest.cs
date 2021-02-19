namespace ToneAnalyzerFunctionTests
{
    using System;
    using System.Threading.Tasks;
    using Messaging.ServiceBus;
    using Microsoft.Extensions.Logging;
    using Moq;
    using Shared.TestSupport;
    using ToneAnalyzerFunction;
    using ToneAnalyzerFunction.Services;
    using Xunit;

    public class SetCommentToneFunctionTest : TestBase<SetCommentToneFunction>
    {
        [Fact]
        public async Task RunShould_NotSetTone_WhenMessageIsEmpty()
        {
            // Arrange
            var message = Any<SetCommentToneMessage>().ToMessage();

            // Act
            await SystemUnderTest.Run(message,  Mock.Of<ILogger>());

            // Assert
            GetMock<IToneAnalyzer>().Verify(x => x.GetTone(Any<string>()), Times.Never);
            GetMock<ICommentService>().Verify(x => x.SetCommentTone(Any<CommentTone>()), Times.Never);
        }

        [Fact]
        public async Task RunShould_ThrowException_WhenToneAnalyzerReturnsEmpty()
        {
            // Arrange
            var message = Some<SetCommentToneMessage>().ToMessage();
            GetMock<IToneAnalyzer>()
                .Setup(x => x.GetTone(Any<string>()))
                .Returns(string.Empty);

            // Act & Assert
            var exception =  await Assert.ThrowsAsync<Exception>(async () => await SystemUnderTest.Run(message, Mock.Of<ILogger>()));
            Assert.Equal("Error retrieving comment tone", exception.Message);
        }


        [Fact]
        public async Task RunShould_ThrowException_WhenCommentServiceReturnsFalse()
        {
            // Arrange
            var message = Some<SetCommentToneMessage>().ToMessage();
            GetMock<IToneAnalyzer>()
                .Setup(x => x.GetTone(Any<string>()))
                .Returns(Some<string>());
            
            GetMock<ICommentService>()
                .Setup(x => x.SetCommentTone(Some<CommentTone>()))
                .ReturnsAsync(false);

            // Act & Assert
            var exception =  await Assert.ThrowsAsync<Exception>(async () => await SystemUnderTest.Run(message, Mock.Of<ILogger>()));
            Assert.Equal("Error setting tone for comment", exception.Message);
        }
    }
}
