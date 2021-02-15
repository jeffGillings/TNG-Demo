namespace WebAPI.Services
{
    using System;
    using System.Threading.Tasks;
    using WebAPI.Data;
    using WebAPI.Data.Domain;

    public class CommentRepository : ICommentRepository
    {
        private readonly ApplicationDbContext _dbContext;

        public CommentRepository(ApplicationDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task AddAsync(Comment comment)
        {
            if(comment == null)
            {
                throw new ArgumentNullException(nameof(comment));
            }

            return AddAsyncImpl(comment);
        }

        public Task<Comment> GetAsync(int id)
        {
            return GetAsyncImpl(id);
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }

        private async Task AddAsyncImpl(Comment comment)
        {
            await _dbContext.Comments.AddAsync(comment);
        }

        private async Task<Comment> GetAsyncImpl(int id)
        {
            return await _dbContext.Comments.FindAsync(id);
        }
    }
}
