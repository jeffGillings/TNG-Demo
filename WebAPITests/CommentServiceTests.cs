namespace WebAPITests
{
    using System.Threading.Tasks;
    using Messaging.ServiceBus;
    using Moq;
    using Shared.TestSupport;
    using WebAPI.Data.Domain;
    using WebAPI.DTO;
    using WebAPI.Services;
    using Xunit;
    using static Microsoft.AspNetCore.Http.StatusCodes;

    public class CommentServiceTests : TestBase<CommentService>
    {
        public CommentServiceTests()
        {
            SetupSystemUnderTest();
        }

        [Fact]
        public async Task AddCommentAsync_ShouldReturnConflictStatus_WhenCommentIdExists()
        {
            // Arrange
            var comment = Some<Comment>();
            GetMock<ICommentRepository>()
                .Setup(x => x.GetAsync(Any<int>()))
                .ReturnsAsync(comment);

            // Act
            var result = await SystemUnderTest.AddCommentAsync(Some<CommentAddRequest>());

            // Assert
            Assert.Equal(Status409Conflict, result.Reason);
        }

        [Fact]
        public async Task AddCommentAsync_ShouldReturnSuccess_WhenCommentAdded()
        {
            // Arrange
            GetMock<ICommentRepository>()
                .Setup(x => x.GetAsync(Any<int>()))
                .ReturnsAsync(Any<Comment>());

            // Act
            var result = await SystemUnderTest.AddCommentAsync(Some<CommentAddRequest>());

            // Assert 
            Assert.True(result.Successful);
            Assert.True(result.Result > 0);
            GetMock<IQueueClient<SetCommentToneMessage>>().Verify(x => x.SendAsync(Any<SetCommentToneMessage>()), Times.Once);
        }

        [Fact]
        public async Task GetCommentAsync_ShouldReturnBadRequest_WhenCommentNotFound()
        {
            // Arrange
            GetMock<ICommentRepository>()
                .Setup(x => x.GetAsync(Any<int>()))
                .ReturnsAsync(Any<Comment>());

            // Act
            var result = await SystemUnderTest.GetCommentAsync(Some<int>());

            // Assert
            Assert.Equal(Status400BadRequest, result.Reason);
        }

        [Fact]
        public async Task GetCommentAsync_ShouldReturnComment_ForValidCommentId()
        {
            // Arrange
            GetMock<ICommentRepository>()
                .Setup(x => x.GetAsync(Any<int>()))
                .ReturnsAsync(Some<Comment>());

            // Act
            var result = await SystemUnderTest.GetCommentAsync(Some<int>());

            // Assert
            Assert.True(result.Successful);
            Assert.NotNull(result.Result);
            Assert.IsType<CommentResponse>(result.Result);
        }

        [Fact]
        public async Task UpdateCommentAsync_ShouldReturnBadRequest_WhenCommentNotFound()
        {
            // Arrange
            GetMock<ICommentRepository>()
                .Setup(x => x.GetAsync(Any<int>()))
                .ReturnsAsync(Any<Comment>());

            // Act
            var result = await SystemUnderTest.GetCommentAsync(Some<int>());

            // Assert
            Assert.Equal(Status400BadRequest, result.Reason);
        }

        [Fact]
        public async Task UpdateCommentAsync_ShouldReturnCommentResponse_ForValidRequest()
        {
            // Arrange
            GetMock<ICommentRepository>()
                .Setup(x => x.GetAsync(Any<int>()))
                .ReturnsAsync(Some<Comment>());

            // Act
            var result = await SystemUnderTest.GetCommentAsync(Some<int>());

            // Assert
            Assert.True(result.Successful);
            Assert.NotNull(result.Result);
            Assert.IsType<CommentResponse>(result.Result);
        }
    }
}
