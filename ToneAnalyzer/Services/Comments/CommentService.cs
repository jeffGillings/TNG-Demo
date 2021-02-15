namespace ToneAnalyzerFunction.Services
{
    using System.Threading.Tasks;

    public class CommentService : ICommentService
    {
        private readonly ICommentClient _client;

        public CommentService(ICommentClient client)
        {
            _client = client;
        }

        public async Task<bool> SetCommentTone(CommentTone commentTone)
        {
            return await _client.SetCommentTone(commentTone);
        }
    }
}
