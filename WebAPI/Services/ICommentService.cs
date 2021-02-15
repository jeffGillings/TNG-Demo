namespace WebAPI.Services
{
    using System.Threading.Tasks;
    using WebAPI.DTO;

    public interface ICommentService
    {
        Task<Outcome<CommentResponse>> GetCommentAsync(int commentId);
        Task<Outcome<int>> AddCommentAsync(CommentAddRequest request);
        Task<Outcome<CommentResponse>> UpdateCommentAsync(CommentUpdateRequest request);
    }
}
