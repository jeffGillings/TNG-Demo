namespace ToneAnalyzerFunction
{
    using System;
    using System.Text;
    using System.Text.Json;
    using System.Threading.Tasks;
    using Messaging.ServiceBus;
    using Microsoft.Azure.ServiceBus;
    using Microsoft.Azure.WebJobs;
    using Microsoft.Extensions.Logging;
    using ToneAnalyzerFunction.Services;

    public class SetCommentToneFunction
    {
        private readonly IToneAnalyzer _analyzer;
        private readonly ICommentService _commentService;

        public SetCommentToneFunction(IToneAnalyzer analyzer, ICommentService commentService)
        {
            _analyzer = analyzer;
            _commentService = commentService;
        }

        [FunctionName("SetCommentToneFunction")]
        public async Task Run(
            [ServiceBusTrigger("comment-tone-queue", Connection = "CommentToneQueue:ConnectionString")]Message message,
            ILogger logger)
        {
            var (succeeded, payload) = Deserialise(message);
            if (!succeeded)
            {
                logger.LogError("Failed to deserialize message. {@message}", message);
                return;
            }

            string tone = _analyzer.GetTone(payload.Comment);
            if(string.IsNullOrWhiteSpace(tone))
            {
                logger.LogError("Failed to retrieve 'Tone' for comment with Id. {@payload}", payload);
                throw new Exception("Error retrieving comment tone");
            }

            var commentTone = new CommentTone { Id = payload.Id, Tone = tone};
            bool result = await _commentService.SetCommentTone(commentTone);
            if(!result)
            {
                logger.LogError("Failed to set 'Tone' for comment with Id. {@payload}", payload);
                throw new Exception("Error setting tone for comment");
            }

            logger.LogInformation("Comment Tone successfully updated. {@payload}; {@message}", payload, message);
        }

        private (bool, SetCommentToneMessage) Deserialise(Message message)
        {
            if (message.Body.Length == 0)
            {
                return (false, null);
            }

            var payload = JsonSerializer.Deserialize<SetCommentToneMessage>(Encoding.UTF8.GetString(message.Body));
            return payload != null
                ? (true, payload)
                : (false, null);
        }
    }
}
