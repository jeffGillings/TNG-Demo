namespace ToneAnalyzerFunction.Services
{
    using System.Threading.Tasks;

    public interface ICommentClient
    {
        Task<bool> SetCommentTone(CommentTone toneUpdate);
    }
}
