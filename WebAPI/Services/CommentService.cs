namespace WebAPI.Services
{
    using System.Threading.Tasks;
    using global::Messaging.ServiceBus;
    using WebAPI.DTO;
    using static Microsoft.AspNetCore.Http.StatusCodes;
    using static WebAPI.Constants.ErrorMessages;

    public class CommentService : ICommentService
    {
        private readonly ICommentRepository _repository;
        private readonly IQueueClient<SetCommentToneMessage> _queueClient;

        public CommentService(ICommentRepository repository, IQueueClient<SetCommentToneMessage> queueClient)
        {
            _repository = repository;
            _queueClient = queueClient;
        }

        public async Task<Outcome<int>> AddCommentAsync(CommentAddRequest request)
        {
            var comment = await _repository.GetAsync(request.Id);
            if(comment != null)
            {
                return Outcome.Fail<int>(Status409Conflict, DuplicateCommentId);
            }

            comment = CommentMappingHelper.MapToComment(request);
            await _repository.AddAsync(comment);
            await _repository.SaveAsync();

            // Publish message to ServiceBus to trigger  function to
            // get comment tone 
            await _queueClient.SendAsync( CreateCommentMessage(request) );

            return Outcome.Success(comment.Id);
        }

        public async Task<Outcome<CommentResponse>> GetCommentAsync(int commentId)
        {
            var comment = await _repository.GetAsync(commentId);
            if(comment == null)
            {
                return Outcome.Fail<CommentResponse>(Status400BadRequest, CommentNotFound);
            }

            return Outcome.Success( CommentMappingHelper.MapToCommentResponse(comment) );
        }

        public async Task<Outcome<CommentResponse>> UpdateCommentAsync(CommentUpdateRequest request)
        {
            var comment = await _repository.GetAsync(request.Id);
            if(comment == null)
            {
                return Outcome.Fail<CommentResponse>(Status400BadRequest, UpdateFailed);
            }

            comment.Tone = request.Tone;
            await _repository.SaveAsync();

            return Outcome.Success( CommentMappingHelper.MapToCommentResponse(comment) );
        }

        private SetCommentToneMessage CreateCommentMessage(CommentAddRequest request)
        {
            return new SetCommentToneMessage
            {
                Id = request.Id,
                Comment = request.comment
            };
        }
    }
}
