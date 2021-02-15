namespace WebAPI.Services
{
    using System.Threading.Tasks;
    using WebAPI.Data.Domain;

    public interface ICommentRepository
    {
        Task AddAsync(Comment comment);
        Task<Comment> GetAsync(int id);
        Task SaveAsync();
    }
}
