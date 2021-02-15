namespace ToneAnalyzerFunction.Services
{
    using System.Threading.Tasks;

    public interface ICommentService
    {
        Task<bool> SetCommentTone(CommentTone commentTone);
    }
}
