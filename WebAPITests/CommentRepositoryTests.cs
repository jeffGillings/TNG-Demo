namespace WebAPITests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;
    using WebAPI.Data;
    using WebAPI.Data.Domain;
    using WebAPI.Services;
    using Xunit;

    public class CommentRepositoryTests : TestBase<CommentRepository>, IClassFixture<DbContextSetup>
    {
        private DbContextSetup _dbContextSetup;
        private ApplicationDbContext _dbContext => _dbContextSetup.DbContext;
        private CommentRepository _sut;

        public CommentRepositoryTests(DbContextSetup dbContextSetup)
        {
            _dbContextSetup = dbContextSetup;
            _sut = new CommentRepository(_dbContext);
        }

        [Fact]
        public void AddAsyncShould_ThrowException_WhenCommentDataIsNull()
        {
            // Act and Assert
            Assert.ThrowsAsync<ArgumentNullException>( () => _sut.AddAsync(null));
        }

        [Fact]
        public async Task AddAsyncShould_AddComment_WhenValidDataGiven()
        {
            // Arrange
            var comment = Some<Comment>();
            var expectedResult = _dbContext.Comments.Count() + 1;

            // Act
            await _sut.AddAsync(comment);
            _dbContext.SaveChanges();

            // Assert
            Assert.Equal(expectedResult, _dbContext.Comments.Count());
        }
    }
}
