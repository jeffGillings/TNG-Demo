namespace WebAPI.Messaging
{
    public class NewCommentEvent
    {
        public int commentId { get; set; }
        public string Comment { get; set; }
    }
}
