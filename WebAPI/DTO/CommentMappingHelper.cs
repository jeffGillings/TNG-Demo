namespace WebAPI.DTO
{
    using WebAPI.Data.Domain;

    public static class CommentMappingHelper
    {
        public static Comment MapToComment(CommentAddRequest request)
        {
            return new Comment
            {
                Id = request.Id,
                Statement = request.comment
            };
        }

        public static CommentResponse MapToCommentResponse(Comment comment)
        {
            return new CommentResponse
            {
                Comment = comment.Statement,
                Tone = comment.Tone,
            };
        }
    }
}
